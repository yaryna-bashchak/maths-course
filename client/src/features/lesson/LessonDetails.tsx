import { Box, Button, Typography } from "@mui/material";
import { useEffect } from "react";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import Videos from "./Videos";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { courseSelectors, fetchCourseAsync, initializeCourseStatus } from "../courses/coursesSlice";
import { Course, LessonParams } from "../../app/models/course";
import { isAvailable } from "./isAvailable";
import { findLessonById } from "./findLessonById";

function isLoading(course: Course | undefined, courseLoaded: boolean, lessonParams: LessonParams, status: string) {
    return (!course || !course.sections?.length) && !courseLoaded && (!lessonParams || status.includes('pending'));
}

function isNotFound(course: Course | undefined, lesson: Lesson | null, status: string) {
    return (!course || !course.sections?.length || !lesson) && !status.includes('pending');
}

export default function LessonDetails() {
    const dispatch = useAppDispatch();
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;
    const { status } = useAppSelector(state => state.courses);
    const courseStatus = useAppSelector(state => state.courses.individualCourseStatus[parseInt(courseId!)]);
    const { courseLoaded, lessonParams } = courseStatus || {};

    useEffect(() => {
        if (!lessonParams)
            dispatch(initializeCourseStatus({ courseId: parseInt(courseId!) }));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [courseStatus]);

    useEffect(() => {
        if (!course || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    if (isLoading(course, courseLoaded, lessonParams, status)) return <LoadingComponent />;

    if (isNotFound(course, lesson, status) || !isAvailable(course, lessonId!)) return <NotFound />;

    return (
        lesson ? (
            <>
                <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                    <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course/${courseId}`}>Назад до курсу</Button>
                </Box>
                <Typography variant="h5">{lesson.number}. {lesson.title}</Typography>
                <Typography variant="body1">{lesson.description}</Typography>
                <Videos />
                <Tests />
            </>
        ) : null
    )
}