import { Lesson } from "../../app/models/lesson";

interface Props {
    lessons: Lesson[];
    addLesson: () => void;
}

export default function ListOfLessons({lessons, addLesson}: Props) {
    return (
        <>
            <ul>
                {lessons.map((item, index) => (
                <li key={index}>{item.title} - {item.description}</li>
                ))}
            </ul>
            <button onClick={addLesson}>Add lesson</button>
        </>
    )
}