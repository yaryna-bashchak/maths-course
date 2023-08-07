import { Button, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { Link, useParams } from "react-router-dom";

interface Props {
    lesson: Lesson;
}

export default function Tests({ lesson }: Props) {
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();

    return (
        <>
            <Typography variant="h5" sx={{mt: "5px", mb: "10px"}}>
                Результат тестування: {(lesson.testScore !== -1) ? `${lesson.testScore.toFixed(2)}%` : "ще не складено"}
            </Typography>
            <Button component={Link} to={`/course/${courseId}/lesson/${lessonId}/test`} variant="contained" disableElevation>
                Почати тест
            </Button>
        </>
    )
}