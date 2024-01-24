import { Edit, Delete } from "@mui/icons-material";
import { TableRow, TableCell, Button, useMediaQuery, useTheme } from "@mui/material";
import { Lesson } from "../../../app/models/lesson";

interface Props {
    lesson?: Lesson;
}

export default function LessonLine({ lesson }: Props) {
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    return (
        <>
            <TableRow
                key={lesson?.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
                <TableCell component="th" scope="row">
                    {lesson?.id}
                </TableCell>
                {/* <TableCell align="left">
                                    <Box display='flex' alignItems='center'>
                                        <img src={course.pictureUrl} alt={course.name} style={{ height: 50, marginRight: 20 }} />
                                        <span>{course.name}</span>
                                    </Box>
                                </TableCell> */}
                <TableCell align="left">{lesson?.title}</TableCell>
                {!isMobileOrTablet && <TableCell align="center">{lesson?.importance}</TableCell>}
                <TableCell align="right">
                    <Button startIcon={<Edit />} />
                    {/* <Button onClick={() => handleSelectCourse(section)} startIcon={<Edit />} /> */}
                    <Button startIcon={<Delete />} color='error' disabled />
                </TableCell>
            </TableRow>
        </>
    )
}