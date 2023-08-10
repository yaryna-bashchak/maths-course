import { Backdrop, Box, CircularProgress, Typography } from "@mui/material";

type Props = {
    message?: string;
};

export default function LoadingComponent({ message = 'Завантаження...' }: Props) {
    return (
        <Backdrop open={true} invisible={true}>
            <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
                <CircularProgress size={80} color="secondary" />
                <Typography variant="h5" sx={{ justifyContent: "center", position: "fixed", top: "60%" }}>{message}</Typography>
            </Box>
        </Backdrop>
    )
}