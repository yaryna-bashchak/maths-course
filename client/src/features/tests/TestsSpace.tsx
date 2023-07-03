import { Box, Button, Step, StepButton, Stepper, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Test } from "../../app/models/test";
import TestControl from "./TestControl";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

export default function TestsSpace() {
    const [tests, setTests] = useState<Test[] | null>(null);
    const [activeStep, setActiveStep] = useState(0);
    const [completed, setCompleted] = useState<{
        [k: number]: number;
    }>({});
    const [testScore, setTestScore] = useState(0);
    const [isFinished, setIsFinished] = useState(false);

    const [loading, setLoading] = useState(true);
    const { id } = useParams<{ id: string }>();

    useEffect(() => {
        axios.get(`http://localhost:5000/api/tests/lessonId/${id}`)
            .then(response => {
                setTests(response.data);
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id])

    const totalSteps = () => {
        return tests ? tests.length : 0;
    };

    const completedSteps = () => {
        return Object.keys(completed).length;
    };

    const handleNext = () => {
        if (tests) {
            let newActiveStep = tests.findIndex((test, i) => !(i in completed) && i > activeStep);

            if (newActiveStep === -1) {
                newActiveStep = tests.findIndex((test, i) => !(i in completed));
            }

            setActiveStep((newActiveStep >= 0) ? newActiveStep : activeStep + 1);
        }
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleStep = (step: number) => () => {
        setActiveStep(step);
    };

    const handleComplete = (score: number) => {
        const newCompleted = { ...completed };
        setTestScore((prev) => prev + score);
        newCompleted[activeStep] = score;
        setCompleted(newCompleted);
    };

    const handleFinish = () => {
        setIsFinished(true);
        putResult();
    }

    const handleReset = () => {
        setActiveStep(0);
        setCompleted({});
        setTestScore(0);
        setIsFinished(false);
    };

    const putResult = () => {
        const data = {
            testScore: (100 * testScore / totalSteps()),
        };

        axios.put(`http://localhost:5000/api/Lessons/${id}`, data)
            .then(response => {
                console.log('Success:', response.data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    if (loading) return <h3>Loading...</h3>

    if (!tests) return <h3>Tests not found</h3>

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start' }}>
                <Button startIcon={<ArrowBackIcon/>} variant="outlined" component={Link} to={`/lesson/${id}`}>Назад до уроку</Button>
            </Box>
            <Typography variant="h3">Тести</Typography>
            <Box sx={{ width: '100%' }}>
                <Stepper nonLinear activeStep={activeStep}>
                    {tests.map((label, index) => (
                        <Step key={index} completed={completed[index] >= 0}>
                            <StepButton color="inherit" onClick={handleStep(index)} />
                        </Step>
                    ))}
                </Stepper>
            </Box>
            {
                tests.map((test, index) => (
                    <TestControl
                        test={test}
                        handleNext={handleNext}
                        handleBack={handleBack}
                        handleComplete={handleComplete}
                        handleFinish={handleFinish}
                        handleReset={handleReset}
                        testScore={testScore}
                        totalSteps={totalSteps}
                        completedSteps={completedSteps}
                        completed={completed}
                        activeStep={activeStep}
                        index={index}
                        isFinished={isFinished}
                    />
                ))
            }
        </>
    )
}