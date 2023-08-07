import { Box, Button, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import Videos from "./Videos";
import { Link, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import NotFound from "../../app/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

export default function LessonDetails() {
    const [lesson, setLesson] = useState<Lesson | null>(null);
    const [isTheoryCompleted, setIsTheoryCompleted] = useState<boolean>(false);
    const [isPracticeCompleted, setIsPracticeCompleted] = useState<boolean>(false);
    const [loading, setLoading] = useState(true);
    // let testScore = -1;
    const { courseId, lessonId } = useParams<{ courseId: string, lessonId: string }>();


    useEffect(() => {
        lessonId && agent.Lesson.details(parseInt(lessonId))
            .then(lesson => {
                setLesson(lesson);
                setIsTheoryCompleted(lesson.isTheoryCompleted);
                setIsPracticeCompleted(lesson.isPracticeCompleted);
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [lessonId])

    useEffect(() => {
        if (lesson) {
            const data = {
                isTheoryCompleted: Number(isTheoryCompleted),
                isPracticeCompleted: Number(isPracticeCompleted)
            };

            agent.Lesson.update(lesson.id, data)
                .then(response => {
                    console.log('Success:', response.data);
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isTheoryCompleted, isPracticeCompleted]);

    const handleChangeTheory = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsTheoryCompleted(event.target.checked);
    };

    const handleChangePractice = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsPracticeCompleted(event.target.checked);
    };

    if (loading) return <LoadingComponent />

    if (!lesson) return <NotFound />

    return (
        lesson ? (
            <>
                <Box sx={{ display: 'flex', justifyContent: 'start', mb: '10px' }}>
                    <Button startIcon={<ArrowBackIcon />} variant="outlined" component={Link} to={`/course/${courseId}`}>Назад до курсу</Button>
                </Box>
                <Typography variant="h5">{lesson.number}. {lesson.title}</Typography>
                <Typography variant="body1">{lesson.description}</Typography>
                <Videos isTheoryCompleted={isTheoryCompleted} isPracticeCompleted={isPracticeCompleted} onTheoryClick={handleChangeTheory} onPracticeClick={handleChangePractice} />
                <Tests lesson={lesson} />
            </>
        ) : null
    )
}