import { Collapse, ListItemButton, ListItemText, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import StarPurple500RoundedIcon from '@mui/icons-material/StarPurple500Rounded';
import { green, grey, yellow } from '@mui/material/colors';
import CalculateOutlinedIcon from '@mui/icons-material/CalculateOutlined';
import DoneOutlineIcon from '@mui/icons-material/DoneOutline';

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
                <ListItemText >
                    <span style={{ display: "flex", alignItems: "center" }}>
                        {lesson.isCompleted ? <DoneOutlineIcon sx={{ color: green[500], mr: "5px" }}/> : <CalculateOutlinedIcon sx={{ color: grey[500], mr: "5px" }}/>}
                        {lesson.title}
                        {
                            Array.from({ length: lesson.importance }).map((_, i) => (
                                <StarPurple500RoundedIcon sx={{ color: yellow[800], ml: i === 0 ? "5px" : "" }} />
                            ))
                        }
                    </span>
                </ListItemText>
                {isOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <Typography variant="body1">{lesson.description}</Typography>
            </Collapse>
        </>
    )
}