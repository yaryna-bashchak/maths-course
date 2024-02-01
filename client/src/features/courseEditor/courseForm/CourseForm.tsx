import { Typography, Grid, Paper, Box, Button, Table, TableBody, TableContainer } from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { Course } from "../../../app/models/course";
import { useEffect } from "react";
import { yupResolver } from '@hookform/resolvers/yup';
import { courseValidationSchema } from "./validationSchemas";
import SectionForm from "./SectionForm";
import { Lesson } from "../../../app/models/lesson";
import useCourse from "../../../app/hooks/useCourse";
import agent from "../../../app/api/agent";
import { useAppDispatch } from "../../../app/store/configureStore";
import { setCourse } from "../../courses/coursesSlice";
import { LoadingButton } from "@mui/lab";

interface Props {
    course?: Course;
    cancelEdit: () => void;
    handleSelectLesson: (lesson: Lesson | undefined) => void;
}

export default function CourseForm({ course: givenCourse, cancelEdit, handleSelectLesson }: Props) {
    const { course: fullCourse } = useCourse(givenCourse?.id);
    const course = fullCourse ?? givenCourse;
    const dispatch = useAppDispatch();

    const { control, reset, handleSubmit, formState: { isSubmitting } } = useForm({
        resolver: yupResolver<any>(courseValidationSchema)
    });

    useEffect(() => {
        if (course) reset(course);
    }, [course, reset]);

    const handleSubmitData = async (data: FieldValues) => {
        console.log(data);
        try {
            let response: Course;
            if (course) {
                response = await agent.Course.update(course.id, data);
                cancelEdit();
            } else {
                response = await agent.Course.create(data);
            }

            dispatch(setCourse(response));
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <Box sx={{ p: 4 }}>
            <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
                Курс
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput control={control} name='title' label='Назва курсу' />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput type="number" control={control} name='priceFull' label='Повна ціна' />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput type="number" control={control} name='priceMonthly' label='Щомісячна ціна' />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput type="number" control={control} name='duration' label='Тривалість' />
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput multiline={true} rows={2} control={control} name='description' label='Опис' />
                    </Grid>
                </Grid>
                <Box display='flex' justifyContent='space-between' sx={{ mt: 3 }}>
                    <Button onClick={cancelEdit} variant='contained' color='inherit'>Відмінити</Button>
                    <LoadingButton loading={isSubmitting} type="submit" variant='contained' color='success'>Зберегти</LoadingButton>
                </Box>
            </form>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{ pt: 6 }} variant='h4'>Секції</Typography>
                {/* <Button onClick={() => setEditMode('course')} sx={{ m: 2 }} size='large' variant='contained'>Створити</Button> */}
            </Box>
            <TableContainer component={Paper} sx={{ mt: 2 }}>
                <Table>
                    <TableBody>
                        {course?.sections.map((section, index) =>
                            <SectionForm section={section} courseId={course?.id} key={index} handleSelectLesson={handleSelectLesson} />)}
                        {course && <SectionForm
                            handleSelectLesson={handleSelectLesson}
                            courseId={course?.id}
                            numberOfNewSection={
                                course?.sections ?
                                    course.sections.reduce((max, section) => section.number > max ? section.number : max, 0) + 1
                                    : 1} />}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    )
}