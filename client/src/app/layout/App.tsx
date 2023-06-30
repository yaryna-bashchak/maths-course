import { Container, CssBaseline, useTheme } from "@mui/material";
import Header from "./Header";
import { Outlet } from "react-router-dom";

function App() {
    const theme = useTheme();

    return (
        <>
            <CssBaseline />
            <Header />
            <Container sx={{
                pt: "90px",
                width: "100%",
                [theme.breakpoints.up('md')]: {
                    width: '70%',
                    // mawWidth: "1200px",
                },
            }}>
                <Outlet />
            </Container>
        </>
    );
}

export default App;
