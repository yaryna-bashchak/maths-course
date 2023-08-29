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
import LoadingButton from '@mui/lab/LoadingButton';
import { useState } from 'react';
import agent from '../../app/api/agent';
import { FieldValues, useForm } from 'react-hook-form';

export default function Login() {
    const { register, handleSubmit, formState: { isSubmitting } } = useForm();

    const submitForm = async (data: FieldValues) => {
        await agent.Account.login(data);
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
            <Box component="form" onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Нік"
                    autoComplete="username"
                    autoFocus
                    {...register('username')}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Пароль"
                    type="password"
                    autoComplete="current-password"
                    {...register('password')}
                />
                <LoadingButton
                    loading={isSubmitting}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Увійти
                </LoadingButton>
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