import { Box, Button, Step, StepButton, Stepper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import TestControl from "./TestControl";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import agent from "../../app/api/agent";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchTestsAsync, testSelectors } from "./testsSlice";

export default function TestsSpace() {
    const dispatch = useAppDispatch();
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const tests = useAppSelector(testSelectors.selectAll);
    const { lessonId: previousLessonId, status } = useAppSelector(staet => staet.tests);

    const [activeStep, setActiveStep] = useState(0);
    const [completed, setCompleted] = useState<{
        [k: number]: number;
    }>({});
    const [testScore, setTestScore] = useState(0);
    const [isFinished, setIsFinished] = useState(false);

    useEffect(() => {
        if (previousLessonId !== parseInt(lessonId ?? "")) dispatch(fetchTestsAsync(parseInt(lessonId ?? "")));
    }, [dispatch, lessonId, previousLessonId])

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

        lessonId && agent.Test.update(parseInt(lessonId), data)
            .then(response => {
                console.log('Success:', response.data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    if (status === "pendingFetchTests") return <LoadingComponent />

    if (tests.length === 0) return <NotFound />

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course/${courseId}/lesson/${lessonId}`}>Назад до уроку</Button>
            </Box>
            <Typography variant="h3" sx={{ m: '10px 0px' }}>Тести</Typography>
            <Box sx={{ width: '100%', m: '10px 0px' }}>
                <Stepper nonLinear activeStep={activeStep}>
                    {tests.map((label, index) => (
                        <Step key={index} completed={completed[index] >= 0}>
                            <StepButton color="inherit" onClick={handleStep(index)} />
                        </Step>
                    ))}
                </Stepper>
            </Box>
            {
                isFinished ? (
                    <>
                        <Typography sx={{ mt: 2, mb: 1 }}>
                            Вітаю! Твій результат: {(100 * testScore / totalSteps()).toFixed(2)}%
                        </Typography>
                        <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                            <Box sx={{ flex: '1 1 auto' }} />
                            <Button endIcon={<ArrowForwardIcon />} variant="outlined" component={Link} to={`/course/${courseId}/lesson/${lessonId ? parseInt(lessonId) + 1 : 0}`}>До наступного уроку</Button>
                        </Box>
                    </>
                ) : (
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
                )
            }
        </>
    )
}