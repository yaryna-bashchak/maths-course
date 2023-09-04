import { Collapse, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { green, grey, yellow } from '@mui/material/colors';
import CalculateOutlinedIcon from '@mui/icons-material/CalculateOutlined';
import DoneOutlineIcon from '@mui/icons-material/DoneOutline';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import { Section } from "../../app/models/course";
import { Lesson } from "../../app/models/lesson";
import LessonItemShort from "./LessonItemShort";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

interface Props {
    section: Section;
    isOpen: boolean;
    onItemClick: (index: number) => void;
}

export default function SectionItem({ section, isOpen, onItemClick }: Props) { 
    type Stage = "unavailable" | "completed" | "inProcess" | "notStarted";

    const stages = {
        unavailable: <LockOutlinedIcon sx={{ color: grey[400], mr: "5px" }} />,
        completed: <DoneOutlineIcon sx={{ color: green[500], mr: "5px" }} />,
        inProcess: <AccessTimeIcon sx={{ color: yellow[800], mr: "5px" }} />,
        notStarted: <CalculateOutlinedIcon sx={{ color: grey[500], mr: "5px" }} />,
    };

    const stageOfLessonCompletion = (lesson: Lesson, isAvailable: boolean): Stage => {
        if(!isAvailable) return "unavailable";

        const completed = [lesson.isTheoryCompleted, lesson.isPracticeCompleted, Boolean(lesson.testScore)]

        if (completed.every(value => value === true)) {
            return "completed";
        } else if (completed.some(value => value === true)) {
            return "inProcess";
        } else {
            return "notStarted";
        }
    }

    const stageOfSectionCompletion = (completed: Stage[], isAvailable: boolean): Stage => {
        if(!isAvailable) return "unavailable";

        if (completed.every(value => value === "completed")) {
            return "completed";
        } else if (completed.some(value => value !== "notStarted")) {
            return "inProcess";
        } else {
            return "notStarted";
        }
    }

    const handleClick = () => {
        onItemClick(section.id);
    };

    const sectionCompleted = section.lessons.map(lesson => stageOfLessonCompletion(lesson, section.isAvailable));
    const stage = stageOfSectionCompletion(sectionCompleted, section.isAvailable);
    const countOfCompleted = sectionCompleted.filter(value => value === "completed").length;

    return (
        <>
            <ListItemButton
                className='item-border'
                onClick={handleClick}
                sx={{ opacity: section.isAvailable ? 1 : 0.5 }}>
                <ListItemIcon
                    sx={{
                        minWidth: "0",
                        mr: "10px",
                        "& svg": {
                            fontSize: "25px",
                        }
                    }}>
                    {stages[stage]}
                </ListItemIcon>
                <ListItemText
                    primary={section.title}
                    secondary={`Завершено: ${countOfCompleted}/${section.lessons.length}`}
                    sx={{
                        ".MuiListItemText-primary": {
                            fontSize: "16px",
                        }
                    }} />
                {/* <ListItemText sx={{ "& svg": { fontSize: "20px", mr: "10px" } }}>
                    <span style={{ display: "flex", alignItems: "center", fontSize: "16px" }}>
                        {stages[stage]}
                        {section.title}
                    </span>
                </ListItemText> */}
                {isOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>
            <Collapse className="item-border" in={isOpen} timeout="auto" unmountOnExit sx={{ pl: "16px" }}>
                {section.lessons.map((lesson, index) =>
                    <LessonItemShort
                        key={lesson.id}
                        icon={stages[sectionCompleted[index]]}
                        lesson={lesson}
                        isAvailable={section.isAvailable}
                    />
                )}
            </Collapse>
        </>
    )
}