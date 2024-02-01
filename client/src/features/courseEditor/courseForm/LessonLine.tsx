import { Edit, Delete, Add } from "@mui/icons-material";
import { TableRow, TableCell, Button, useMediaQuery, useTheme } from "@mui/material";
import { Lesson } from "../../../app/models/lesson";

interface Props {
    lesson?: Lesson;
    handleSelectLesson?: (lesson: Lesson | undefined) => void;
    handleDeleteLesson?: (id: number) => void;
}

export default function LessonLine({ lesson, handleSelectLesson, handleDeleteLesson }: Props) {
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    return (
        <>
            <TableRow
                key={lesson?.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
                {handleSelectLesson && handleDeleteLesson && (lesson ? <>
                    <TableCell component="th" scope="row">
                        {lesson?.id}
                    </TableCell>
                    <TableCell align="left">{lesson?.title}</TableCell>
                    {!isMobileOrTablet && <TableCell align="center">{lesson?.importance}</TableCell>}
                    <TableCell align="right">
                        <Button onClick={() => handleSelectLesson(lesson)} startIcon={<Edit />} />
                        <Button onClick={() => handleDeleteLesson(lesson.id)} startIcon={<Delete />} color='error' />
                    </TableCell>
                </> : <>
                    <TableCell align="center" colSpan={4}>
                        <Button onClick={() => handleSelectLesson(undefined)} startIcon={<Add />}>додати урок</Button>
                    </TableCell>
                </>)
                }
            </TableRow>
        </>
    )
}