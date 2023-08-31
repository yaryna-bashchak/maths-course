import Avatar from '@mui/material/Avatar';
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
import { useForm } from 'react-hook-form';
import agent from '../../app/api/agent';

export default function Register() {
    const { register, handleSubmit, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'onTouched',
    });

    return (
        <Container component={Paper} maxWidth="xs" sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4 }}>
            <CssBaseline />
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Реєстрація
            </Typography>
            <Box component="form"
                onSubmit={handleSubmit(data => agent.Account.register(data))}
                noValidate sx={{ mt: 1 }}
            >
                <TextField
                    margin="normal"
                    fullWidth
                    label="Нікнейм"
                    autoComplete="username"
                    autoFocus
                    {...register('username', { required: 'Будь ласка, придумайте свій нікнейм, який ви будете викоритовувати для входу' })}
                    error={!!errors.username}
                    helperText={errors?.username?.message as string}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Електронна пошта"
                    autoComplete="email"
                    {...register('email', { required: 'Будь ласка, вкажіть вашу електронну пошту' })}
                    error={!!errors.email}
                    helperText={errors?.email?.message as string}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Пароль"
                    type="password"
                    autoComplete="current-password"
                    {...register('password', { required: 'Будь ласка, придумайте пароль' })}
                    error={!!errors.password}
                    helperText={errors?.password?.message as string}
                />
                <LoadingButton
                    loading={isSubmitting}
                    disabled={!isValid}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Зареєструватися
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to="/login">
                            {"Вже маєте акаунт? Увійти"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}