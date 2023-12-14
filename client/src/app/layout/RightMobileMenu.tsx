import { List, IconButton, ListItem, ListItemText, Drawer } from "@mui/material";
import { useAppSelector } from "../store/configureStore";
import { AccountCircle } from "@mui/icons-material";
import { NavLink } from "react-router-dom";
import MenuIcon from '@mui/icons-material/Menu';
import { useState } from "react";

const nonAuthorizedLinks = [
    { title: 'Увійти', path: '/login' },
    { title: 'Зареєструватися', path: '/register' },
]
const authorizedLinks = [
    { title: 'Вийти', path: '/login', color: 'red' },
]

const baseLinks = [
    { title: 'Про⠀нас', path: '' },
    { title: 'Курси', path: 'course' },
    { title: 'Ціни', path: '/#price' },
]

export default function RightMobileMenu() {
    const { user } = useAppSelector(state => state.account);
    const [menuOpen, setMenuOpen] = useState(false);

    const sideList = () => (
        <List>
            {user && (
                <ListItem sx={{ borderBottom: '1px solid #ddd' }}>
                    <AccountCircle sx={{ mr: 1, fontSize: '25px', color: 'primary.main' }} />
                    <ListItemText primary={`${user.username.toUpperCase()}`} primaryTypographyProps={{ color: 'primary.main' }} />
                </ListItem>
            )}
            {(user ? authorizedLinks : nonAuthorizedLinks).concat(baseLinks).map(({ title, path }) => (
                <ListItem button component={NavLink} to={path} key={path} onClick={() => setMenuOpen(false)}>
                    {title}
                </ListItem>
            ))}
        </List>
    );

    return (<>
        <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={() => setMenuOpen(true)}
        >
            <MenuIcon />
        </IconButton>
        <Drawer
            anchor="right"
            open={menuOpen}
            onClose={() => setMenuOpen(false)}
            sx={{ width: '150px', flexShrink: 0, '& .MuiDrawer-paper': { width: '150px' } }}
        >
            {sideList()}
        </Drawer>
    </>)
}