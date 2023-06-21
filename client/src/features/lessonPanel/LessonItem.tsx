import { Collapse, ListItemButton, ListItemText, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { useState } from "react";

interface Props {
    index: number;
    lesson: Lesson;
}

export default function LessonItem({ index, lesson }: Props) {
    const [open, setOpen] = useState(false);

    const handleClick = () => {
        setOpen(!open);
    };

    return (
        <>
            <ListItemButton key={index} onClick={handleClick}>
                <ListItemText primary={lesson.title} />
                {open ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={open} timeout="auto" unmountOnExit>
                <Typography variant="body1">{lesson.description}</Typography>
            </Collapse>
        </>
    )
}