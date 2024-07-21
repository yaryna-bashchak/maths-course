import { Box, Button, Paper, Step, StepLabel, Stepper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import PurchasePlanSelection from "./PurchasePlanSelection.tsx";
import PaymentForm from "./PaymentForm.tsx";
import Review from './Review.tsx';
import useCourse from "../../app/hooks/useCourse.tsx";
import { FieldValues, FormProvider, useForm } from "react-hook-form";
import { Link, useLocation } from "react-router-dom";
import NotFound from "../../app/errors/NotFound.tsx";
import LoadingComponent from "../../app/layout/LoadingComponent.tsx";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { StripeElementType } from "@stripe/stripe-js";
import { createOrUpdatePayment } from "./paymentSlice.ts";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore.ts";
import { CardNumberElement } from "@stripe/react-stripe-js";
import { useElements, useStripe } from "@stripe/react-stripe-js";
import { LoadingButton } from "@mui/lab";

const steps = ['Оберіть план', 'Перегляньте своє замовлення', 'Оплатіть замовлення'];

function useQuery() {
    return new URLSearchParams(useLocation().search);
}

export default function CheckoutPage() {
    const { course, status, courseLoaded, lessonParams } = useCourse();
    const dispatch = useAppDispatch();
    const query = useQuery();
    const methods = useForm();
    const [activeStep, setActiveStep] = useState(0);
    const [sectionExists, setSectionExists] = useState<boolean | null>(null);
    const isSectionIdInQuery = !!query.get('sectionId');
    const [sectionId, setSectionId] = useState(isSectionIdInQuery ? parseInt(query.get('sectionId')!, 10) : null);
    const [cardState, setCardState] = useState<{ elementError: { [key in StripeElementType]?: string } }>({ elementError: {} });
    const [cardComplete, setCardComplete] = useState<any>({ cardNumber: false, cardExpiry: false, cardCvc: false });
    const [loading, setLoading] = useState(false);
    const [paymentMessage, setPaymentMessage] = useState('');
    const [paymentSucceed, setPaymentSucceed] = useState(false);
    const { payments } = useAppSelector(state => state.payments);
    const stripe = useStripe();
    const elements = useElements();

    function onCardInputChange(event: any) {
        setCardState({
            ...cardState,
            elementError: {
                ...cardState.elementError,
                [event.elementType]: event.error?.message,
            }
        });
        setCardComplete({
            ...cardComplete,
            [event.elementType]: event.complete,
        })
    }

    const selectedPlan = methods.watch('selectedPlan');
    const nameOnCard = methods.watch('nameOnCard');

    useEffect(() => {
        if (isSectionIdInQuery && activeStep === 0) {
            const sectionFound = course?.sections?.some(section => section.id === sectionId);

            if (sectionFound) {
                methods.setValue('selectedPlan', 'Section');
                setActiveStep(1);
                setSectionExists(true);
            } else {
                setSectionExists(false);
            }
        }
    }, [sectionId, course, methods, activeStep, isSectionIdInQuery]);

    useEffect(() => {
        if (course) {
            if (selectedPlan === 'Section' && !isSectionIdInQuery) {
                setSectionId(course.sections[0].id);
            } else if (selectedPlan === 'Course') {
                setSectionId(null);
            }
        }
    }, [course, isSectionIdInQuery, sectionId, selectedPlan]);

    const getStepContent = (step: number) => {
        if (!course) return;

        switch (step) {
            case 0:
                return <PurchasePlanSelection name='selectedPlan' control={methods.control} />;
            case 1:
                return <Review name='total' control={methods.control} sectionId={sectionId} />;
            case 2:
                return <PaymentForm cardState={cardState} onCardInputChange={onCardInputChange} />;
            default:
                throw new Error('Unknown step');
        }
    }

    if ((!course || course.sections.length === 0) && !courseLoaded) {
        if (!lessonParams || status.includes('pending')) return <LoadingComponent />;
        return <NotFound />;
    }

    if (sectionExists !== null && sectionExists === false) return <NotFound />;

    const getCurrentPayment = () => {
        return payments.find(payment =>
            payment.purchaseType === selectedPlan
                && payment.purchaseId === (selectedPlan === 'Course' ? course?.id : sectionId));
    }

    async function submitPayment() {
        setLoading(true);
        if (!stripe || !elements) return;
        try {
            const cardElement = elements.getElement(CardNumberElement);
            const paymentResult = await stripe.confirmCardPayment(getCurrentPayment()!.clientSecret, {
                payment_method: {
                    card: cardElement!,
                    billing_details: {
                        name: nameOnCard
                    }
                }
            })
            console.log(paymentResult);
            if (paymentResult.paymentIntent?.status === 'succeeded') {
                // dispatch(fetchPayments()); ще приходить status 0, не встигає оновитись
                setPaymentSucceed(true);
                setPaymentMessage('Дякуємо - ми отримали Вашу оплату!');
                setActiveStep(activeStep + 1);
                // dispatch(fetchCourseAsync(course!.id)); ще приходить що курс не available
                setLoading(false);
            } else {
                setPaymentMessage(paymentResult.error?.message || 'Збій оплати');
                setPaymentSucceed(false);
                setLoading(false);
                setActiveStep(activeStep + 1);
            }
        } catch (error) {
            console.log(error);
            setLoading(false);
        }

    }
    const handleNext = async (data: FieldValues) => {
        console.log(data);

        if (activeStep === steps.length - 1) {
            await submitPayment();
        } else {
            if (activeStep === steps.length - 2) {
                dispatch(createOrUpdatePayment({
                    body: {
                        purchaseType: selectedPlan,
                        purchaseId: selectedPlan === 'Course' ? course?.id : sectionId
                    }
                }));
            }

            setActiveStep(activeStep + 1);
        }
    }

    const handleBack = () => {
        setActiveStep(activeStep - 1);
    };

    const submitDisabled = () => {
        if (activeStep === 0) {
            return selectedPlan === null
        }
        else if (activeStep === steps.length - 1) {
            return !cardComplete.cardNumber
                || !cardComplete.cardExpiry
                || !cardComplete.cardCvc
                || !nameOnCard
        }
    }

    return (
        <FormProvider {...methods}>
            <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course`}>Назад до курсів</Button>
            </Box>
            <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
                <Typography component="h1" variant="h4" align="center">
                    Оформлення покупки
                </Typography>
                <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
                    {steps.map((label) => (
                        <Step key={label}>
                            <StepLabel>{label}</StepLabel>
                        </Step>
                    ))}
                </Stepper>
                <>
                    {activeStep === steps.length ? (
                        <>
                            <Typography variant="h5" gutterBottom>
                                {paymentMessage}
                            </Typography>
                            {paymentSucceed ? (
                                <Typography variant="subtitle1">
                                    Вітаємо з придбанням курсу! Ми вже надали Вам доступ до уроків - успіхів! 
                                </Typography>
                            ) : (
                                <Button variant="contained" onClick={handleBack}>Спробувати ще раз</Button>
                            )}

                        </>
                    ) : (
                        <form onSubmit={methods.handleSubmit(handleNext)}>
                            {getStepContent(activeStep)}
                            <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                                {activeStep !== 0 && (
                                    <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }} disabled={activeStep === 1 && isSectionIdInQuery}>
                                        Назад
                                    </Button>
                                )}
                                <LoadingButton
                                    loading={loading}
                                    variant="contained"
                                    disabled={submitDisabled()}
                                    type='submit'
                                    sx={{ mt: 3, ml: 1 }}
                                >
                                    {activeStep === steps.length - 1 ? 'Оплатити' : 'Далі'}
                                </LoadingButton>
                            </Box>
                        </form>
                    )}
                </>
            </Paper>
        </FormProvider>
    );
}
