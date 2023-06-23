import { Collapse, ListItemButton, ListItemText, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import StarPurple500RoundedIcon from '@mui/icons-material/StarPurple500Rounded';
import { green, grey, yellow } from '@mui/material/colors';
import CalculateOutlinedIcon from '@mui/icons-material/CalculateOutlined';
import DoneOutlineIcon from '@mui/icons-material/DoneOutline';
import Videos from "./Videos";
import { useEffect, useState } from "react";

interface Props {
    lesson: Lesson;
    isOpen: boolean;
    onItemClick: (index: number) => void;
    updateLessonCompletion: (lessonId: number, isCompleted: boolean) => void;
}

export default function LessonItem({ lesson, isOpen, onItemClick, updateLessonCompletion }: Props) {
    const [completed, setCompleted] = useState(lesson.isCompleted ? [true, true, true] : [false, false, true]);

    const handleChangeTheory = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCompleted([event.target.checked, completed[1], completed[2]]);
    };

    const handleChangePractice = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCompleted([completed[0], event.target.checked, completed[2]]);
    };

    useEffect(() => {
        const isCompleted = completed.every(value => value === true);
        updateLessonCompletion(lesson.id, isCompleted);
    }, [completed]);

    const handleClick = () => {
        onItemClick(lesson.id);
    };

    return (
        <>
            <ListItemButton onClick={handleClick}>
                <ListItemText>
                    <span style={{ display: "flex", alignItems: "center" }}>
                        {lesson.isCompleted ? <DoneOutlineIcon sx={{ color: green[500], mr: "5px" }} /> : <CalculateOutlinedIcon sx={{ color: grey[500], mr: "5px" }} />}
                        {lesson.title}
                        {
                            Array.from({ length: lesson.importance }).map((_, i) => (
                                <StarPurple500RoundedIcon key={i} sx={{ color: yellow[800], ml: i === 0 ? "5px" : "" }} />
                            ))
                        }
                    </span>
                </ListItemText>
                {isOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <Typography variant="body1">{lesson.description}</Typography>
                <Videos lesson={lesson} completed={completed} onTheoryClick={handleChangeTheory} onPracticeClick={handleChangePractice} />
            </Collapse>
        </>
    )
}