import { Typography, Grid, Paper, Box, Button } from "@mui/material";
import { useForm } from "react-hook-form";
import AppTextInput from "../../app/components/AppTextInput";
import { Course } from "../../app/models/course";
import { useEffect } from "react";

interface Props {
    course?: Course;
    cancelEdit: () => void;
}

export default function CourseForm({course, cancelEdit}: Props) {
    const { control, reset } = useForm();

    useEffect(() => {
        if (course) reset(course);
    }, [course, reset]);

    return (
        <Box component={Paper} sx={{p: 4}}>
            <Typography variant="h4" gutterBottom sx={{mb: 4}}>
                Редагування курсу
            </Typography>
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
                    <AppTextInput multiline={true} rows={2} control={control} name='description' label='Description' />
                </Grid>
                <Grid item xs={12}>
                    <AppTextInput control={control} name='pictureUrl' label='Image' />
                </Grid>
            </Grid>
            <Box display='flex' justifyContent='space-between' sx={{mt: 3}}>
                <Button onClick={cancelEdit} variant='contained' color='inherit'>Відмінити</Button>
                <Button variant='contained' color='success'>Зберегти</Button>
            </Box>
        </Box>
    )
}