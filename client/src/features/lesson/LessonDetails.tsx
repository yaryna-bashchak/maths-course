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
import { courseSelectors, fetchCourseAsync } from "../courses/coursesSlice";
import { Course } from "../../app/models/course";

export function findLessonById(course: Course, lessonId: number): Lesson | null {
    for (const section of course.sections) {
        for (const lesson of section.lessons) {
            if (lesson.id === lessonId) {
                return lesson;
            }
        }
    }
    return null;
}

export default function LessonDetails() {
    const dispatch = useAppDispatch();
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;
    const { status } = useAppSelector(state => state.courses);

    useEffect(() => {
        if(!course || course?.sections.length === 0) dispatch(fetchCourseAsync(parseInt(courseId!)));
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    if (status === 'pendingFetchCourse') return <LoadingComponent />

    if (!lesson) return <NotFound />

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