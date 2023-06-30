import { AccountCircle } from "@mui/icons-material";
import { AppBar, Avatar, Box, IconButton, InputBase, List, ListItem, Menu, MenuItem, Toolbar, Typography, alpha, styled } from "@mui/material";
import { useState } from "react";
import SearchIcon from '@mui/icons-material/Search';
import { NavLink } from "react-router-dom";

const midLinks = [
    { title: 'Про⠀нас', path: '' },
    { title: 'Курси', path: 'course/1' },
    { title: 'Ціни', path: 'lesson/1' },
]

const rightLinks = [
    { title: 'Увійти', path: '/login' },
    { title: 'Зареєструватися', path: '/register' },
]

const isAuthorized = false;

const navStyles = {
    color: 'inherit',
    textDecoration: 'none',
    fontSize: '14px',
    lineHeight: 1.5,
    '&:hover': {
        color: 'grey.300',
    }
}

// const Search = styled('div')(({ theme }) => ({
//     position: 'relative',
//     borderRadius: theme.shape.borderRadius,
//     backgroundColor: alpha(theme.palette.common.white, 0.15),
//     '&:hover': {
//         backgroundColor: alpha(theme.palette.common.white, 0.25),
//     },
//     marginRight: theme.spacing(2),
//     marginLeft: 0,
//     width: '100%',
//     [theme.breakpoints.up('sm')]: {
//         marginLeft: theme.spacing(3),
//         width: 'auto',
//     },
// }));

// const SearchIconWrapper = styled('div')(({ theme }) => ({
//     padding: theme.spacing(0, 2),
//     height: '100%',
//     position: 'absolute',
//     pointerEvents: 'none',
//     display: 'flex',
//     alignItems: 'center',
//     justifyContent: 'center',
// }));

// const StyledInputBase = styled(InputBase)(({ theme }) => ({
//     color: 'inherit',
//     '& .MuiInputBase-input': {
//         padding: theme.spacing(1, 1, 1, 0),
//         // vertical padding + font size from searchIcon
//         paddingLeft: `calc(1em + ${theme.spacing(4)})`,
//         transition: theme.transitions.create('width'),
//         width: '100%',
//         [theme.breakpoints.up('md')]: {
//             width: '20ch',
//         },
//     },
// }));

export default function Header() {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <AppBar position="fixed" sx={{ top: 0, bottom: "auto" }}>
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-around', alignItems: 'center' }}>
                <Box sx={{display: 'flex', alignItems: 'center'}}>
                    <Avatar alt="Logo" src="/images/header/logo.jpg" sx={{ mr: "10px" }} />
                    <Typography variant="h6" component={NavLink} to='/'
                        sx={navStyles}>
                        План ZNO-шника
                    </Typography>
                    {/* <Search>
                        <SearchIconWrapper>
                            <SearchIcon />
                        </SearchIconWrapper>
                        <StyledInputBase
                            placeholder="Знайти..."
                            inputProps={{ 'aria-label': 'search' }}
                        />
                    </Search> */}
                </Box>

                <List sx={{ display: 'flex' }}>
                    {midLinks.map(({ title, path }) => (
                        <ListItem
                            component={NavLink}
                            to={path}
                            key={path}
                            sx={navStyles}
                        >
                            {title.toUpperCase()}
                        </ListItem>
                    ))}
                </List>

                {
                    isAuthorized ?
                        <Box>
                            <IconButton
                                size="large"
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="inherit"
                            >
                                <AccountCircle />
                                <Typography variant="body1" sx={{ ml: "5px" }}>
                                    Яринка
                                </Typography>
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={Boolean(anchorEl)}
                                onClose={handleClose}
                            >
                                <MenuItem onClick={handleClose}>Профіль</MenuItem>
                                <MenuItem onClick={handleClose}>Мої Курси</MenuItem>
                                <MenuItem onClick={handleClose}>Всі Курси</MenuItem>
                                <MenuItem onClick={handleClose}>Вийти</MenuItem>
                            </Menu>
                        </Box> :
                        <List sx={{ display: 'flex' }}>
                            {rightLinks.map(({ title, path }) => (
                                <ListItem
                                    component={NavLink}
                                    to={path}
                                    key={path}
                                    sx={navStyles}
                                >
                                    {title}
                                </ListItem>
                            ))}
                        </List>
                }
            </Toolbar>
        </AppBar>
    )
}