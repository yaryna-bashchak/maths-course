import { Typography, Grid, Box, Button } from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { Lesson } from "../../../app/models/lesson";
import { useEffect, useRef } from "react";
import AppDropzone from "../../../app/components/AppDropzone";
import { yupResolver } from '@hookform/resolvers/yup';
import { lessonValidationSchema } from "../courseForm/validationSchemas";

interface Props {
    lesson?: Lesson;
    cancelEdit: () => void;
}

export default function LessonForm({ lesson, cancelEdit }: Props) {
    const { control, reset, handleSubmit, watch } = useForm({
        resolver: yupResolver<any>(lessonValidationSchema)
    });
    const watchTheoryFile = watch('theory', null)
    const watchPracticeFile = watch('practice', null)
    const theoryRef = useRef<HTMLVideoElement>(null);
    const practiceRef = useRef<HTMLVideoElement>(null);

    useEffect(() => {
        if (lesson) reset(lesson);
    }, [lesson, reset]);

    useEffect(() => {
        if (watchTheoryFile && watchTheoryFile.preview) {
            theoryRef.current?.load();
        }
    }, [watchTheoryFile]);

    useEffect(() => {
        if (watchPracticeFile && watchPracticeFile.preview) {
            practiceRef.current?.load();
        }
    }, [watchPracticeFile]);

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
                            <AppDropzone control={control} name='theory' />
                            {watchTheoryFile && (
                                <video ref={theoryRef} style={{ maxHeight: 200 }} controls >
                                    <source src={watchTheoryFile.preview} type={watchTheoryFile.type} />
                                    Your browser does not support the video tag.
                                </video>
                            )}
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h5" gutterBottom>
                            Практика
                        </Typography>
                        <Box display='flex' justifyContent='space-between' alignItems='center'>
                            <AppDropzone control={control} name='practice' />
                            {watchPracticeFile &&
                                <video ref={practiceRef} style={{ maxHeight: 200 }} controls >
                                    <source src={watchPracticeFile.preview} type={watchPracticeFile.type} />
                                    Your browser does not support the video tag.
                                </video>
                            }
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