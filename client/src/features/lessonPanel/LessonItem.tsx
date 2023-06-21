import { Collapse, ListItemButton, ListItemText, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { ExpandLess, ExpandMore } from "@mui/icons-material";

interface Props {
    index: number;
    lesson: Lesson;
    isOpen: boolean;
    onItemClick: (index: number) => void;
}

export default function LessonItem({ index, lesson, isOpen, onItemClick }: Props) {
    const handleClick = () => {
        onItemClick(index);
    };

    return (
        <>
            <ListItemButton key={index} onClick={handleClick}>
                <ListItemText primary={lesson.title} />
                {isOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <Typography variant="body1">{lesson.description}</Typography>
            </Collapse>
        </>
    )
}