import { Typography, Button, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Box } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import useCourses from "../../app/hooks/useCourses";
import { useEffect, useState } from "react";
import { Course } from "../../app/models/course";
import { Lesson } from "../../app/models/lesson";
import CourseForm from "./courseForm/CourseForm";
import LessonForm from "./lessonForm/LessonForm";
import LoadingComponent from "../../app/layout/LoadingComponent";
import agent from "../../app/api/agent";
import { removeCourse } from "../courses/coursesSlice";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch } from "../../app/store/configureStore";

type EditMode = 'false' | 'course' | 'lesson';

export default function CourseEditor() {
    const { courses, status, coursesLoaded } = useCourses();
    const [isCoursesRequestMade, setIsCoursesRequestMade] = useState(coursesLoaded);
    const [editMode, setEditMode] = useState<EditMode>('false');
    const [selectedCourse, setSelectedCourse] = useState<Course | undefined>(undefined);
    // const [selectedSection, setSelectedSection] = useState<Section | undefined>(undefined);
    const [selectedLesson, setSelectedLesson] = useState<Lesson | undefined>(undefined);
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(false);
    const [target, setTarget] = useState(0);

    useEffect(() => {
        if (status === 'pendingFetchCourses') {
            setIsCoursesRequestMade(true);
        }
    }, [status]);

    const handleSelectCourse = (course: Course) => {
        setSelectedCourse(course);
        setEditMode('course');
    }

    const handleDeleteCourse = (id: number) => {
        setLoading(true);
        setTarget(id);
        agent.Course.delete(id)
            .then(() => dispatch(removeCourse(id)))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }

    const handleSelectLesson = (lesson: Lesson | undefined) => {
        setSelectedLesson(lesson);
        setEditMode('lesson');
    }

    const cancelEdit = () => {
        if (editMode === 'lesson') {
            setSelectedLesson(undefined);
            setEditMode('course');
            return;
        }
        if (editMode === 'course') {
            setSelectedCourse(undefined);
            setEditMode('false');
        }
    }

    if (isCoursesRequestMade === false || status.includes('pending')) return <LoadingComponent />
    if (editMode === 'course') return <CourseForm course={selectedCourse} cancelEdit={cancelEdit} handleSelectLesson={handleSelectLesson} />
    if (editMode === 'lesson') return <LessonForm lesson={selectedLesson} cancelEdit={cancelEdit} />

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
                                <TableCell component="th" scope="row">{course.id} </TableCell>
                                <TableCell align="left">{course.title}</TableCell>
                                <TableCell align="right">{course.duration}</TableCell>
                                <TableCell align="right">{course.priceFull}</TableCell>
                                <TableCell align="right">{course.priceMonthly}</TableCell>
                                <TableCell align="right">{course.sections.length}</TableCell>
                                <TableCell align="right">
                                    <Button onClick={() => handleSelectCourse(course)} startIcon={<Edit />} />
                                    <LoadingButton loading={loading && target === course.id} onClick={() => handleDeleteCourse(course.id)} startIcon={<Delete />} color='error' />
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}