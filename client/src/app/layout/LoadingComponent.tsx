import { Backdrop, Box, CircularProgress, Typography } from "@mui/material";

type Props = {
    message?: string;
    fullScreen?: boolean;
};

export default function LoadingComponent({
    message = 'Завантаження...',
    fullScreen = true,
}: Props) {
    const loadingContent = (
        <Box display="flex" flexDirection="column" justifyContent="center" alignItems="center" height={fullScreen ? '100vh' : '100%'} >
            <CircularProgress size={80} color="secondary" />
            <Typography variant="h5" sx={{ justifyContent: 'center', marginTop: 2 }}>
                {message}
            </Typography>
        </Box>
    );

    return fullScreen ? <Backdrop open={true} invisible={true}>{loadingContent}</Backdrop> : loadingContent;
}