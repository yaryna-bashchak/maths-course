import { Typography, Grid, Paper, Box, Button, Table, TableBody, TableContainer } from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { Course } from "../../../app/models/course";
import { useEffect } from "react";
import AppDropzone from "../../../app/components/AppDropzone";
import { yupResolver } from '@hookform/resolvers/yup';
import { courseValidationSchema } from "./validationSchemas";
import SectionForm from "./SectionForm";

interface Props {
    course?: Course;
    cancelEdit: () => void;
}

export default function CourseForm({ course, cancelEdit }: Props) {
    const { control, reset, handleSubmit, watch } = useForm({
        resolver: yupResolver<any>(courseValidationSchema)
    });
    const watchFile = watch('file', null)

    useEffect(() => {
        if (course) reset(course);
    }, [course, reset]);

    const handleSubmitData = (data: FieldValues) => {
        console.log(data);
    }

    return (
        <Box sx={{ p: 4 }}>
            <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
                Редагування курсу
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
                    <Grid item xs={12}>
                        <Box display='flex' justifyContent='space-between' alignItems='center'>
                            <AppDropzone control={control} name='file' />
                            {watchFile &&
                                <img src={watchFile.preview} alt="preview" style={{ maxHeight: 200 }} />
                                // ) : (
                                //     <img src={} alt="preview" style={{ maxHeight: 200 }} />
                                // )
                            }
                        </Box>
                    </Grid>
                </Grid>
                <Box display='flex' justifyContent='space-between' sx={{ mt: 3 }}>
                    <Button onClick={cancelEdit} variant='contained' color='inherit'>Відмінити</Button>
                    <Button type="submit" variant='contained' color='success'>Зберегти</Button>
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
                            <SectionForm section={section} key={index} />)}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    )
}