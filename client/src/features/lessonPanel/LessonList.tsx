import { List } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import LessonItem from "./LessonItem";

interface Props {
    lessons: Lesson[];
    addLesson: () => void;
}

export default function LessonList({ lessons, addLesson }: Props) {
    return (
        <>
            <List>
                {lessons.map((lesson, index) =>
                    <LessonItem key={lesson.id} index={index} lesson={lesson} />
                )}
            </List>
            <button onClick={addLesson}>Add lesson</button>
        </>
    )
}