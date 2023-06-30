import { Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { Lesson } from "../../app/models/lesson";
import Tests from "./Tests";
import Videos from "./Videos";
// import { Course } from "../../app/models/course";

export default function LessonDetails() {
    // const [course, setCourse] = useState<Course>();
    
    const [lesson, setLesson] = useState<Lesson>();
    const [isTheoryCompleted, setIsTheoryCompleted] = useState<boolean>(false);
    const [isPracticeCompleted, setIsPracticeCompleted] = useState<boolean>(false);
    // let testScore = -1;
    const id = 1;

    useEffect(() => {
        fetch('http://localhost:5000/api/lessons/1')
            .then(response => response.json())
            .then(data => {
                // setCourse(data);
                // const foundLesson = data.lessons.find((lesson: Lesson) => lesson.id === id);
                setLesson(data);
                if (data) {
                    setIsTheoryCompleted(data.isTheoryCompleted);
                    setIsPracticeCompleted(data.isPracticeCompleted);
                    // testScore = data.testScore;
                } else {
                    console.log(`No lesson found with id ${id}`);
                }
            })
    }, [])

    useEffect(() => {
        let data = {};

        if (isTheoryCompleted !== lesson?.isTheoryCompleted) {
            data = { ...data, isTheoryCompleted: Number(isTheoryCompleted) };
            if (lesson) { lesson.isTheoryCompleted = isTheoryCompleted; }
        }

        if (isPracticeCompleted !== lesson?.isPracticeCompleted) {
            data = { ...data, isPracticeCompleted: Number(isPracticeCompleted) };
            if (lesson) { lesson.isPracticeCompleted = isPracticeCompleted; }
        }

        if (Object.keys(data).length > 0) {
            fetch(`http://localhost:5000/api/Lessons/${lesson?.id}`, {
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