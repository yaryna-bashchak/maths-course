import { Box, Button, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import VideoWithCheckbox from "./VideoWithCheckbox";
import { Link, useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { Course, LessonParams } from "../../app/models/course";
import { isAvailable } from "./isAvailable";
import { findLessonById } from "./findLessonById";
import useCourse from "../../app/hooks/useCourse";

function isLoading(course: Course | undefined, courseLoaded: boolean, lessonParams: LessonParams, status: string) {
    return (!course || !course.sections?.length) && !courseLoaded && (!lessonParams || status.includes('pending'));
}

function isNotFound(course: Course | undefined, lesson: Lesson | null, status: string) {
    return (!course || !course.sections?.length || !lesson) && !status.includes('pending');
}

export default function LessonDetails() {
    const { course, status, courseLoaded, lessonParams } = useCourse();

    const { lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;

    if (isLoading(course, courseLoaded, lessonParams, status)) return <LoadingComponent />;

    if (isNotFound(course, lesson, status) || !isAvailable(course, lessonId!)) return <NotFound />;

    return (
        lesson ? (
            <>
                <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                    <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course/${course?.id}`}>Назад до курсу</Button>
                </Box>
                <Typography variant="h5">{lesson.number}. {lesson.title}</Typography>
                <Typography variant="body1">{lesson.description}</Typography>
                <Box className="box-container">
                    <VideoWithCheckbox videoNumber={0} />
                    <VideoWithCheckbox videoNumber={1} />
                </Box>
                <Tests />
            </>
        ) : null
    )
}