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

const steps = ['Оберіть план', 'Перегляньте своє замовлення', 'Оплатіть замовлення'];

function useQuery() {
    return new URLSearchParams(useLocation().search);
}

export default function CheckoutPage() {
    const { course, status, courseLoaded, lessonParams } = useCourse();
    const query = useQuery();
    const methods = useForm();
    const [activeStep, setActiveStep] = useState(0);
    const [sectionExists, setSectionExists] = useState<boolean | null>(null);
    const isSectionIdInQuery = !!query.get('sectionId');
    const [sectionId, setSectionId] = useState(isSectionIdInQuery ? parseInt(query.get('sectionId')!, 10) : null);

    const selectedPlan = methods.watch('selectedPlan');

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
                return <PaymentForm />;
            default:
                throw new Error('Unknown step');
        }
    }

    if ((!course || course.sections.length === 0) && !courseLoaded) {
        if (!lessonParams || status.includes('pending')) return <LoadingComponent />;
        return <NotFound />;
    }

    if (sectionExists !== null && sectionExists === false) return <NotFound />;

    const handleNext = (data: FieldValues) => {
        console.log(data);
        setActiveStep(activeStep + 1);
    };

    const handleBack = () => {
        setActiveStep(activeStep - 1);
    };

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
                                Thank you for your order.
                            </Typography>
                            <Typography variant="subtitle1">
                                Your order number is #2001539. We have emailed your order
                                confirmation, and will send you an update when your order has
                                shipped.
                            </Typography>
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
                                <Button
                                    variant="contained"
                                    type='submit'
                                    sx={{ mt: 3, ml: 1 }}
                                >
                                    {activeStep === steps.length - 1 ? 'Оплатити' : 'Далі'}
                                </Button>
                            </Box>
                        </form>
                    )}
                </>
            </Paper>
        </FormProvider>
    );
}
