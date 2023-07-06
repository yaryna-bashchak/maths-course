import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link, useLocation } from "react-router-dom";

export default function ServerError() {
    const { state } = useLocation();

    return (
        <Container component={Paper}>
            {
                state?.error ? (
                    <>
                        <Typography gutterBottom variant="h3" color="secondary">
                            {state.error.title}
                        </Typography>
                        <Divider />
                        <Typography variant="body1">
                            {state.error.detail || "Помилка сервера :("}
                        </Typography>
                    </>
                ) : (
                    <Typography gutterBottom variant="h5">Упс! Помилка сервера :{'('}</Typography>
                )
            }
            <Divider />
            <Button fullWidth component={Link} to="/course/1">Повернутись до курсів</Button>
        </Container>
    )
}