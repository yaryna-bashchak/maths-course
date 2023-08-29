import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Link } from 'react-router-dom';
import { Paper } from '@mui/material';
import { useState } from 'react';

export default function Login() {
    const [values, setValues] = useState({
        username: '',
        password: '',
    })

    const handleSubmit = () => {
        console.log(values);
    };

    const handleInputChange = (event: any) => {
        const {name, value} = event.target;
        setValues({...values, [name]: value})
    }

    return (
        <Container component={Paper} maxWidth="xs" sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4 }}>
            <CssBaseline />
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Вхід
            </Typography>
            <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Нік"
                    name="username"
                    autoComplete="username"
                    autoFocus
                    onChange={handleInputChange}
                    value={values.username}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    name="password"
                    label="Пароль"
                    type="password"
                    autoComplete="current-password"
                    onChange={handleInputChange}
                    value={values.password}
                />
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Увійти
                </Button>
                <Grid container>
                    <Grid item>
                        <Link to="/register">
                            {"Ще не маєш акаунта? Зареєструватися"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}