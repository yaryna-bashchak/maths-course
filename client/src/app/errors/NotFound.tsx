import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

type Props = {
    message?: string;
};

export default function NotFound({ message = 'Упс! Ми не змогли знайти такої сторінки :(' }: Props) {
    return (
        <Container component={Paper} sx={{ height: 400 }}>
            <Typography gutterBottom variant="h3">{message}</Typography>
            <Divider />
            <Button fullWidth component={Link} to="/course">Повернутись до курсів</Button>
        </Container>
    )
}