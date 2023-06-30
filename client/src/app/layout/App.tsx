import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import Header from "./Header";
import { Outlet } from "react-router-dom";

function App() {
    const theme = createTheme({
        palette: {
            background: {
                default: "#eaeaea"
            }
        }
    });

    return (
        <>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <Header />
                <Container sx={{
                    pt: "90px",
                    pb: "20px",
                    width: "100%",
                    backgroundColor: "white",
                    [theme.breakpoints.up('md')]: {
                        width: '70%',
                        // mawWidth: "1200px",
                    },
                }}>
                    <Outlet />
                </Container>
            </ThemeProvider >
        </>
    );
}

export default App;
