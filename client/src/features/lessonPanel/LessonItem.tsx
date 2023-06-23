import { Collapse, ListItemButton, ListItemText, Typography } from "@mui/material";
import { Lesson } from "../../app/models/lesson";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import StarPurple500RoundedIcon from '@mui/icons-material/StarPurple500Rounded';
import { green, grey, yellow } from '@mui/material/colors';
import CalculateOutlinedIcon from '@mui/icons-material/CalculateOutlined';
import DoneOutlineIcon from '@mui/icons-material/DoneOutline';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import Videos from "./Videos";
import { useEffect, useState } from "react";

interface Props {
    lesson: Lesson;
    isOpen: boolean;
    onItemClick: (index: number) => void;
}

export default function LessonItem({ lesson, isOpen, onItemClick }: Props) {
    const [completed, setCompleted] = useState([lesson.isTheoryCompleted, lesson.isPracticeCompleted, (lesson.testScore >= 0)]);

    const handleChangeTheory = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCompleted([event.target.checked, completed[1], completed[2]]);
    };

    const handleChangePractice = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCompleted([completed[0], event.target.checked, completed[2]]);
    };

    type Stage = "completed" | "inProcess" | "notStarted";

    const stages = {
        completed: <DoneOutlineIcon sx={{ color: green[500], mr: "5px" }} />,
        inProcess: <AccessTimeIcon sx={{ color: yellow[800], mr: "5px" }} />,
        notStarted: <CalculateOutlinedIcon sx={{ color: grey[500], mr: "5px" }} />,
    };

    const stageOfCompletion = (): Stage => {
        if (completed.every(value => value === true)) {
            return "completed";
        } else if (completed.some(value => value === true)) {
            return "inProcess";
        } else {
            return "notStarted";
        }
    }

    const [stage, setStage] = useState(stageOfCompletion());

    useEffect(() => {
        setStage(stageOfCompletion());
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [completed]);

    useEffect(() => {
        if (!isOpen) {
            let data = {};

            if (completed[0] !== lesson.isTheoryCompleted) {
                data = { ...data, isTheoryCompleted: Number(completed[0]) };
            }

            if (completed[1] !== lesson.isPracticeCompleted) {
                data = { ...data, isPracticeCompleted: Number(completed[1]) };
            }

            if (Object.keys(data).length > 0) {
                fetch(`http://localhost:5000/api/Lessons/${lesson.id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(data),
                })
                    .then(response => response.json())
                    .then(data => {
                        console.log('Success:', data);
                    })
                    .catch((error) => {
                        console.error('Error:', error);
                    });
            }
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isOpen]);

    const handleClick = () => {
        onItemClick(lesson.id);
    };

    return (
        <>
            <ListItemButton onClick={handleClick}>
                <ListItemText>
                    <span style={{ display: "flex", alignItems: "center" }}>
                        {stages[stage]}
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
                <Videos completed={completed} onTheoryClick={handleChangeTheory} onPracticeClick={handleChangePractice} />
            </Collapse>
        </>
    )
}