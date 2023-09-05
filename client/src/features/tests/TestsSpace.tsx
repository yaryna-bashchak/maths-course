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
import { findLessonById, isAvailable } from "../lesson/LessonDetails";
import { courseSelectors, fetchCourseAsync, initializeCourseStatus } from "../courses/coursesSlice";
import { Test } from "../../app/models/test";
import { LessonParams } from "../../app/models/course";

export function totalSteps(tests: Test[]): number {
    return tests ? tests.length : 0;
};

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

    const handleStep = (step: number) => () => {
        setActiveStep(step);
    };

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
                            Вітаю! Твій результат: {lesson?.testScore && (lesson.testScore).toFixed(2)}%
                        </Typography>
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