import { Typography, Grid, Box, Button } from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { Lesson } from "../../../app/models/lesson";
import { useEffect, useRef } from "react";
import AppDropzone from "../../../app/components/AppDropzone";
import { yupResolver } from '@hookform/resolvers/yup';
import { lessonValidationSchema } from "../courseForm/validationSchemas";
import VideoPreview from "./VideoPreview";

interface Props {
    lesson?: Lesson;
    cancelEdit: () => void;
}

export default function LessonForm({ lesson, cancelEdit }: Props) {
    const { control, reset, handleSubmit, watch, formState: { isDirty } } = useForm({
        resolver: yupResolver<any>(lessonValidationSchema)
    });

    const watchTheoryFile = watch('theoryFile', null)
    const watchPracticeFile = watch('practiceFile', null)
    const theoryRef = useRef<HTMLVideoElement>(null);
    const practiceRef = useRef<HTMLVideoElement>(null);

    useEffect(() => {
        if (lesson && !watchTheoryFile && !watchPracticeFile && !isDirty) reset(lesson);
        return () => {
            if (watchTheoryFile) URL.revokeObjectURL(watchTheoryFile.preview);
            if (watchPracticeFile) URL.revokeObjectURL(watchPracticeFile.preview);
        }
    }, [isDirty, lesson, reset, watchPracticeFile, watchTheoryFile]);

    const handleSubmitData = (data: FieldValues) => {
        console.log(data);
    }

    return (
        <Box sx={{ p: 4 }}>
            <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
                Урок
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={8}>
                        <AppTextInput control={control} name='title' label='Назва уроку' />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <AppTextInput type="number" control={control} name='importance' label='Важливість' />
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput multiline={true} rows={4} control={control} name='description' label='Опис' />
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h5" gutterBottom>
                            Теорія
                        </Typography>
                        <Box display='flex' justifyContent='space-between' alignItems='center'>
                            <AppDropzone control={control} name='theoryFile' />
                            <VideoPreview watchFile={watchTheoryFile} ref={theoryRef} />
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h5" gutterBottom>
                            Практика
                        </Typography>
                        <Box display='flex' justifyContent='space-between' alignItems='center'>
                            <AppDropzone control={control} name='practiceFile' />
                            <VideoPreview watchFile={watchPracticeFile} ref={practiceRef} />
                        </Box>
                    </Grid>
                </Grid>
                <Box display='flex' justifyContent='space-between' sx={{ mt: 3 }}>
                    <Button onClick={cancelEdit} variant='contained' color='inherit'>Відмінити</Button>
                    <Button type="submit" variant='contained' color='success'>Зберегти</Button>
                </Box>
            </form>
        </Box>
    )
}