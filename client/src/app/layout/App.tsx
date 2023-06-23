import { useEffect, useState } from "react";
import { Course, Lesson } from "../models/lesson";
import LessonList from "../../features/lessonPanel/LessonList";
import { Container, CssBaseline, Typography } from "@mui/material";
import Header from "./Header";

function App() {
    const [course, setCourse] = useState<Course>();
    const [lessons, setLessons] = useState<Lesson[]>([]);

    useEffect(() => {
        fetch('http://localhost:5000/api/courses/1')
            .then(response => response.json())
            .then(data => {
                setCourse(data);
                setLessons(data.lessons)
            })
    }, [])

    function addLesson() {
        setLessons(prevState => [...prevState,
        {
            id: prevState.length + 1,
            title: 'Тема ' + (prevState.length + 1),
            description: 'На уроці ви дізнаєтеся про те і те.',
            urlTheory: "",
            urlPractice: "string",
            number: prevState.length + 1,
            importance: 1,
            testScore: -1,
            isTheoryCompleted: false,
            isPracticeCompleted: false,
            keywords: [
                {
                    id: 6,
                    word: "кут"
                },
            ],
        }])
    }

    return (
        <>
            <CssBaseline />
            <Header />
            <Container sx={{ pt: "90px" }}>
                <Typography variant="h5">{course?.title}</Typography>
                <LessonList lessons={lessons} addLesson={addLesson} />
            </Container>
        </>
    );
}

export default App;
