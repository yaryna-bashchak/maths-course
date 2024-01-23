import { Typography, Button, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Box } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import useCourses from "../../app/hooks/useCourses";

export default function CourseEditor() {
    const { courses } = useCourses();

    return (
        <>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{ p: 2 }} variant='h4'>Редактор Курсів</Typography>
                <Button sx={{ m: 2 }} size='large' variant='contained' disabled>Створити</Button>
            </Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>№</TableCell>
                            <TableCell align="left">Назва курсу</TableCell>
                            <TableCell align="right">Тривалість</TableCell>
                            <TableCell align="right">Повна ціна</TableCell>
                            <TableCell align="right">Щомісячна ціна</TableCell>
                            <TableCell align="right">Кількість секцій</TableCell>
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {courses.map((course) => (
                            <TableRow
                                key={course.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {course.id}
                                </TableCell>
                                {/* <TableCell align="left">
                                    <Box display='flex' alignItems='center'>
                                        <img src={course.pictureUrl} alt={course.name} style={{ height: 50, marginRight: 20 }} />
                                        <span>{course.name}</span>
                                    </Box>
                                </TableCell> */}
                                <TableCell align="left">{course.title}</TableCell>
                                <TableCell align="right">{course.duration}</TableCell>
                                <TableCell align="right">{course.priceFull}</TableCell>
                                <TableCell align="right">{course.priceMonthly}</TableCell>
                                <TableCell align="right">{course.sections.length}</TableCell>
                                <TableCell align="right">
                                    <Button startIcon={<Edit />} />
                                    <Button startIcon={<Delete />} color='error' disabled />
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}