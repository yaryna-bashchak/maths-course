import { Typography, Button, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Box } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import useCourses from "../../app/hooks/useCourses";
import { useState } from "react";
import { Course } from "../../app/models/course";
// import { Lesson } from "../../app/models/lesson";
import CourseForm from "./courseForm/CourseForm";

type EditMode = 'false' | 'course' | 'lesson';

export default function CourseEditor() {
    const { courses } = useCourses();
    const [editMode, setEditMode] = useState<EditMode>('false');
    const [selectedCourse, setSelectedCourse] = useState<Course | undefined>(undefined);
    // const [selectedLesson, setSelectedLesson] = useState<Lesson | undefined>(undefined);

    const handleSelectCourse = (course: Course) => {
        setSelectedCourse(course);
        setEditMode('course');
    }

    const cancelEdit = () => {
        if (selectedCourse) setSelectedCourse(undefined);
        setEditMode('false');
    }

    if (editMode === 'course') return <CourseForm course={selectedCourse} cancelEdit={cancelEdit} />
    // if (editMode === 'lesson') return <LessonForm />

    return (
        <>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{ p: 2 }} variant='h4'>Редактор Курсів</Typography>
                <Button onClick={() => setEditMode('course')} sx={{ m: 2 }} size='large' variant='contained'>Створити</Button>
            </Box>
            <TableContainer component={Paper}>
                <Table>
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
                                    <Button onClick={() => handleSelectCourse(course)} startIcon={<Edit />} />
                                    <Button startIcon={<Delete />} color='error' />
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}