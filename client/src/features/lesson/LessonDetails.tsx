import { Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import Videos from "./Videos";
import { useParams } from "react-router-dom";
import axios from "axios";

export default function LessonDetails() {
    const [lesson, setLesson] = useState<Lesson | null>(null);
    const [isTheoryCompleted, setIsTheoryCompleted] = useState<boolean>(false);
    const [isPracticeCompleted, setIsPracticeCompleted] = useState<boolean>(false);
    const [loading, setLoading] = useState(true);
    // let testScore = -1;
    const {id} = useParams<{id: string}>();

    
    useEffect(() => {
        axios.get(`http://localhost:5000/api/lessons/${id}`)
            .then(response => {
                setLesson(response.data);
                setIsTheoryCompleted(response.data.isTheoryCompleted);
                setIsPracticeCompleted(response.data.isPracticeCompleted);
            })
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id])
    
    useEffect(() => {
        if (lesson) {
            const data = {
                isTheoryCompleted: Number(isTheoryCompleted),
                isPracticeCompleted: Number(isPracticeCompleted)
            };
            
            axios.put(`http://localhost:5000/api/Lessons/${lesson.id}`, data)
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
    
    if (loading) return <h3>Loading...</h3>
    
    if (!lesson) return <h3>Lesson not found</h3>

    return (
        lesson ? (
            <>
                <Typography variant="h5">{lesson.title}</Typography>
                <Typography variant="body1">{lesson.description}</Typography>
                <Videos isTheoryCompleted={isTheoryCompleted} isPracticeCompleted={isPracticeCompleted} onTheoryClick={handleChangeTheory} onPracticeClick={handleChangePractice} />
                <Tests lesson={lesson} />
            </>
        ) : null
    )
}