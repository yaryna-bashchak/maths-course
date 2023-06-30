import { ListItemButton, ListItemText } from "@mui/material";
import StarPurple500RoundedIcon from '@mui/icons-material/StarPurple500Rounded';
import { yellow } from '@mui/material/colors';
import { Lesson } from "../../app/models/lesson";
import { Link } from "react-router-dom";

interface Props {
    icon: JSX.Element;
    lesson: Lesson;
}

export default function LessonItemShort({ icon, lesson }: Props) {
    return (
        <>
            <Link to={`/lesson/${lesson.id}`} style={{ textDecoration: 'none', color: 'rgba(0, 0, 0, 0.87)' }}>
                <ListItemButton sx={{ p: "4px 16px" }}>
                    <ListItemText>
                        <span style={{ display: "flex", alignItems: "center" }}>
                            {icon}
                            {lesson.title}
                            {
                                Array.from({ length: lesson.importance }).map((_, i) => (
                                    <StarPurple500RoundedIcon key={i} sx={{ color: yellow[800], ml: i === 0 ? "5px" : "" }} />
                                ))
                            }
                        </span>
                    </ListItemText>
                </ListItemButton>
            </Link>
        </>
    )
}