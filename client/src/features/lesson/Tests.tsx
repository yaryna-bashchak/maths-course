import { Button, Typography } from "@mui/material";
import { Link, useParams } from "react-router-dom";
import { useAppSelector } from "../../app/store/configureStore";
import { courseSelectors } from "../courses/coursesSlice";
import { findLessonById } from "./LessonDetails";

export default function Tests() {
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();
    const course = useAppSelector(state => courseSelectors.selectById(state, courseId!));
    const lesson = course ? findLessonById(course, parseInt(lessonId!)) : null;

    return (
        <>
            <Typography variant="h5" sx={{ mt: "5px", mb: "10px" }}>
                Результат тестування: {(lesson?.testScore !== -1) ? `${lesson?.testScore.toFixed(2)}%` : "ще не складено"}
            </Typography>
            <Button component={Link} to={`/course/${courseId}/lesson/${lessonId}/test`} variant="contained" disableElevation>
                Почати тест
            </Button>
        </>
    )
}