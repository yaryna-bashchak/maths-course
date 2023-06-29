import { Container, CssBaseline } from "@mui/material";
import Header from "./Header";
import { Outlet } from "react-router-dom";

function App() {
    return (
        <>
            <CssBaseline />
            <Header />
            <Container sx={{ pt: "90px" }}>
                <Outlet />
            </Container>
        </>
    );
}

export default App;
