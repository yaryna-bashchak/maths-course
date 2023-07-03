import { Button, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { Link, useParams } from "react-router-dom";

interface Props {
    lesson: Lesson;
}

export default function Tests({ lesson }: Props) {
    const {id} = useParams<{id: string}>();

    return (
        <>
            <Typography variant="h5" sx={{mt: "5px", mb: "10px"}}>
                Результат тестування: {(lesson.testScore !== -1) ? `${lesson.testScore.toFixed(2)}%` : "ще не складено"}
            </Typography>
            <Button component={Link} to={`/lesson/${id}/test`} variant="contained" disableElevation>
                Почати тест
            </Button>
        </>
    )
}