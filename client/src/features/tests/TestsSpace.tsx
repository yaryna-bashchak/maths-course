import { Box, Button, Step, StepButton, Stepper, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import TestControl from "./TestControl";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchTestsAsync, testSelectors } from "./testsSlice";
import { courseSelectors, fetchCourseAsync, initializeCourseStatus } from "../courses/coursesSlice";
import { Test } from "../../app/models/test";
import { LessonParams } from "../../app/models/course";
import { isAvailable } from "../lesson/isAvailable";
import { findLessonById } from "../lesson/findLessonById";
import Results from "./Results";

function isLoading(lessonParams: LessonParams, status: string, courseStatus: string) {
    return !lessonParams || status.includes('pending') || courseStatus.includes('pending');
}

function isNotFound(status: string, courseStatus: string, previousLessonId: number, lessonId: number, tests: Test[]) {
    return !status.includes('pending') && !courseStatus.includes('pending') && (previousLessonId !== lessonId || tests.length === 0);
}

export default function TestsSpace() {
    const dispatch = useAppDispatch();
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;
    const tests = useAppSelector(testSelectors.selectAll);
    const { lessonId: previousLessonId, status } = useAppSelector(state => state.tests);
    const { status: courseStatus } = useAppSelector(state => state.courses);
    const [currentTestScore, setCurrentTestScore] = useState(0);

    const [activeStep, setActiveStep] = useState(0);
    const [completed, setCompleted] = useState<{
        [k: number]: number;
    }>({});
    const [isFinished, setIsFinished] = useState(false);
    const [elapsedTime, setElapsedTime] = useState(0);

    const { lessonParams } = useAppSelector(state => state.courses.individualCourseStatus[parseInt(courseId!)]) || {};

    useEffect(() => {
        if (!lessonParams)
            dispatch(initializeCourseStatus({ courseId: parseInt(courseId!) }));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [lessonParams]);

    useEffect(() => {
        if (previousLessonId !== parseInt(lessonId!)) dispatch(fetchTestsAsync(parseInt(lessonId!)));
        if (!course || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    useEffect(() => {
        if (isFinished) return;

        const timer: NodeJS.Timeout = setInterval(() => {
            setElapsedTime(prev => prev + 1);
        }, 1000);

        return () => clearInterval(timer);
    }, [isFinished]);

    const handleStep = (step: number) => () => {
        setActiveStep(step);
    };

    const formatTime = (seconds: number) => {
        const minutes = Math.floor(seconds / 60);
        const secondsOfNewMinute = String(elapsedTime % 60).padStart(2, '0');
        return `${minutes}:${secondsOfNewMinute}`;
    }

    if (isLoading(lessonParams, status, courseStatus)) return <LoadingComponent />

    if (isNotFound(status, courseStatus, previousLessonId, parseInt(lessonId!), tests) || !isAvailable(course, lessonId!)) {
        if (!lesson) return <NotFound />
        return <NotFound message="Ох, тестів до цього уроку ще немає" />
    }

    return (
        <>
            <Box sx={{ display: 'flex', justifyContent: 'start' }}>
                <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course/${courseId}/lesson/${lessonId}`}>Назад до уроку</Button>
            </Box>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', m: '10px 0px' }}>
                <Typography variant="h3">Тести</Typography>
                <Typography variant="h6" sx={{ mr: '10px' }}>
                    {!isFinished && formatTime(elapsedTime)}
                </Typography>
            </Box>
            <Box sx={{ width: '100%', m: '10px 0px' }}>
                <Stepper nonLinear activeStep={activeStep}>
                    {tests.map((_label, index) => (
                        <Step key={index} completed={completed[index] >= 0}>
                            <StepButton color="inherit" onClick={handleStep(index)} />
                        </Step>
                    ))}
                </Stepper>
            </Box>
            {
                isFinished ? (
                    <>
                        <Results
                            formatedTime={formatTime(elapsedTime)}
                            lessonId={lesson!.id}
                            testScore={lesson?.testScore!}
                        />
                        <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                            <Box sx={{ flex: '1 1 auto' }} />
                            <Button endIcon={<ArrowForwardIcon />} variant="outlined" component={Link} to={`/course/${courseId}/lesson/${lessonId ? parseInt(lessonId) + 1 : 0}`}>До наступного уроку</Button>
                        </Box>
                    </>
                ) : (
                    tests.map((test, index) => (
                        <TestControl
                            key={index}
                            test={test}
                            completed={completed}
                            activeStep={activeStep}
                            index={index}
                            currentTestScore={currentTestScore}
                            setCurrentTestScore={setCurrentTestScore}
                            setActiveStep={setActiveStep}
                            setCompleted={setCompleted}
                            setIsFinished={setIsFinished}
                        />
                    ))
                )
            }
        </>
    )
}