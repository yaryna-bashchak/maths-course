import { List, ListItem } from "@mui/material";
import { NavLink } from "react-router-dom";
import SignedInMenu from "./SignedInMenu";
import { useAppSelector } from "../store/configureStore";
import { navStyles } from "./Header";

const midLinks = [
    { title: 'Про⠀нас', path: '' },
    { title: 'Курси', path: 'course' },
    { title: 'Ціни', path: '/#price' },
]

const rightLinks = [
    { title: 'Увійти', path: '/login' },
    { title: 'Зареєструватися', path: '/register' },
]

export default function LaptopNavLinks() {
    const { user } = useAppSelector(state => state.account);

    return (
        <>
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
            {user ?
                <SignedInMenu /> :
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
        </>
    )
}