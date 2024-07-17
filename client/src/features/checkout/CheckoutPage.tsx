import { Box, Button, Paper, Step, StepLabel, Stepper, Typography } from "@mui/material";
import { useState } from "react";
import PurchasePlanSelection from "./PurchasePlanSelection.tsx";
import PaymentForm from "./PaymentForm.tsx";
import Review from "./Review.tsx";
import useCourse from "../../app/hooks/useCourse.tsx";
import { FieldValues, FormProvider, useForm } from "react-hook-form";

const steps = ['Оберіть план', 'Перегляньте своє замовлення', 'Оплатіть замовлення'];

export default function CheckoutPage() {
    const { course } = useCourse();
    const [activeStep, setActiveStep] = useState(0);
    const methods = useForm();

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return <PurchasePlanSelection name='selectedPlan' control={methods.control} title={course?.title} duration={course?.duration} priceFull={course?.priceFull} priceMonthly={course?.priceMonthly} />;
            case 1:
                return <Review />;
            case 2:
                return <PaymentForm />;
            default:
                throw new Error('Unknown step');
        }
    }
    const handleNext = (data: FieldValues) => {
        if (activeStep === 0) console.log(data);
        setActiveStep(activeStep + 1);
    };

    const handleBack = () => {
        setActiveStep(activeStep - 1);
    };

    return (
        <FormProvider {...methods}>
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
                                    <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
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
