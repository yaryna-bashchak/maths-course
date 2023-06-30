import { Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import Videos from "./Videos";

export default function LessonDetails() {
    const [lesson, setLesson] = useState<Lesson>();
    const [isTheoryCompleted, setIsTheoryCompleted] = useState<boolean>(false);
    const [isPracticeCompleted, setIsPracticeCompleted] = useState<boolean>(false);
    // let testScore = -1;
    const id = 1;

    useEffect(() => {
        fetch('http://localhost:5000/api/lessons/1')
            .then(response => response.json())
            .then(data => {
                setLesson(data);
                if (data) {
                    setIsTheoryCompleted(data.isTheoryCompleted);
                    setIsPracticeCompleted(data.isPracticeCompleted);
                } else {
                    console.log(`No lesson found with id ${id}`);
                }
            })
    }, [])

    useEffect(() => {
        if (lesson) {
            const data = {
                isTheoryCompleted: Number(isTheoryCompleted),
                isPracticeCompleted: Number(isPracticeCompleted)
            };

            fetch(`http://localhost:5000/api/Lessons/${lesson.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data),
            })
                .then(response => response.json())
                .then(data => {
                    console.log('Success:', data);
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