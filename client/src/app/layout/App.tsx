import { useEffect, useState } from "react";
import { Course, Lesson } from "../models/lesson";
import LessonList from "../../features/lessonPanel/LessonList";
import { Container, CssBaseline, Typography } from "@mui/material";
import Header from "./Header";

function App() {
    const [course, setCourse] = useState<Course>();
    const [lessons, setLessons] = useState<Lesson[]>([]);

    const updateLessonCompletion = (lessonId: number, isCompleted: boolean) => {
        setLessons(lessons =>
            lessons.map(lesson =>
                lesson.id === lessonId ? { ...lesson, isCompleted } : lesson
            )
        );
    };

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
            isCompleted: false,
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
                <LessonList lessons={lessons} addLesson={addLesson} updateLessonCompletion={updateLessonCompletion}/>
            </Container>
        </>
    );
}

export default App;
