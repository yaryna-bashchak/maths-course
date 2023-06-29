import { List, Typography } from "@mui/material";
import { Course, Lesson } from "../../app/models/lesson";
import LessonItem from "./LessonItem";
import { useEffect, useState } from "react";

export default function CourseDetails() {
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

    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };

    return (
        <>
            <Typography variant="h5">{course?.title}</Typography>
            <List>
                {lessons.map((lesson) =>
                    <LessonItem
                        key={lesson.id}
                        lesson={lesson}
                        isOpen={openIndex === lesson.id}
                        onItemClick={handleItemClick}
                    />
                )}
            </List>
        </>
    )
}