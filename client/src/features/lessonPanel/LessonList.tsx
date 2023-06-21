import { List } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import LessonItem from "./LessonItem";
import { useState } from "react";

interface Props {
    lessons: Lesson[];
    addLesson: () => void;
}

export default function LessonList({ lessons, addLesson }: Props) {
    const [openIndex, setOpenIndex] = useState(-1);

    const handleItemClick = (index: number) => {
        setOpenIndex(openIndex !== index ? index : -1);
    };
    
    return (
        <>
            <List>
                {lessons.map((lesson, index) =>
                    <LessonItem 
                        key={lesson.id}
                        index={index}
                        lesson={lesson}
                        isOpen={openIndex === index} 
                        onItemClick={handleItemClick}
                    />
                )}
            </List>
            <button onClick={addLesson}>Add lesson</button>
        </>
    )
}