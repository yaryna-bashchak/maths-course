import Avatar from '@mui/material/Avatar';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Link, useNavigate } from 'react-router-dom';
import { Paper } from '@mui/material';
import LoadingButton from '@mui/lab/LoadingButton';
import { FieldValues, useForm } from 'react-hook-form';
import { useAppDispatch } from '../../app/store/configureStore';
import { singInUser } from './accountSlice';

export default function Login() {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const { register, handleSubmit, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'onTouched',
    });

    const submitForm = async (data: FieldValues) => {
        await dispatch(singInUser(data));
        navigate('/course');
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
                    fullWidth
                    label="Нік"
                    autoComplete="username"
                    autoFocus
                    {...register('username', { required: 'Будь ласка, вкажіть нік' })}
                    error={!!errors.username}
                    helperText={errors?.username?.message as string}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Пароль"
                    type="password"
                    autoComplete="current-password"
                    {...register('password', { required: 'Будь ласка, вкажіть пароль' })}
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
                    Увійти
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to="/register">
                            {"Ще не маєте акаунта? Зареєструватися"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}