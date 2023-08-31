import { Container, CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import Header from "./Header";
import { Outlet, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css"
import { useEffect } from "react";
import { useAppDispatch } from "../store/configureStore";
import { fetchCurrentUser } from "../../features/account/accountSlice";

function App() {
    const dispatch = useAppDispatch();
    const location = useLocation();
    const whiteBackgroundNotNeeded = ["/login", "/register"]
    const isWhiteBackgroundNeeded = !whiteBackgroundNotNeeded.includes(location.pathname);
    
    useEffect(() => {
        dispatch(fetchCurrentUser());
    }, [dispatch])

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
                <ToastContainer position="bottom-right" theme="colored" />
                <CssBaseline />
                <Header />
                <Container sx={{
                    pt: "90px",
                    pb: "20px",
                    width: "100%",
                    backgroundColor: isWhiteBackgroundNeeded ? "white" : "none",
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
