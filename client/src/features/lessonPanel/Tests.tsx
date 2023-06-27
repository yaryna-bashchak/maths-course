import { Button, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";

interface Props {
    lesson: Lesson;
}

export default function Tests({ lesson }: Props) {
    return (
        <>
            <Typography variant="h5" sx={{mt: "5px", mb: "10px"}}>
                Результат тестування: {(lesson.testScore !== -1) ? `${lesson.testScore}%` : "ще не складено"}
            </Typography>
            <Button variant="contained" disableElevation>
                Почати тест
            </Button>
        </>
    )
}