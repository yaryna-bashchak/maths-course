import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Container, CssBaseline } from "@mui/material";
import Header from "./Header";
import { Outlet, useLocation } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css"
import { useEffect, useState, useCallback } from "react";
import { useAppDispatch } from "../store/configureStore";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import LoadingComponent from "./LoadingComponent";

function App() {
    const dispatch = useAppDispatch();
    const location = useLocation();
    const whiteBackgroundNotNeeded = ["/login", "/register"]
    const isWhiteBackgroundNeeded = !whiteBackgroundNotNeeded.includes(location.pathname);
    const [loading, setLoading] = useState(true);

    const initApp = useCallback(async () => {
        try {
            await dispatch(fetchCurrentUser());
        } catch (error) {
            console.log(error);
        }
    }, [dispatch]);

    useEffect(() => {
        initApp().then(() => setLoading(false));
    }, [initApp])

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
                {loading ? <LoadingComponent />
                    : <Container sx={{
                        pt: "90px",
                        pb: "30px",
                        width: "100%",
                        backgroundColor: isWhiteBackgroundNeeded ? "white" : "none",
                        [theme.breakpoints.up('sm')]: {
                            width: "100%",
                        },
                        [theme.breakpoints.up('md')]: {
                            width: "80%",
                            maxWidth: "980px"
                        },
                    }}>
                        <Outlet />
                    </Container>
                }
            </ThemeProvider >
        </>
    );
}

export default App;
