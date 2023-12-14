import { Button, Menu, MenuItem, useMediaQuery, useTheme } from "@mui/material";
import React from "react";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { signOut } from "../../features/account/accountSlice";
import { AccountCircle } from "@mui/icons-material";
import { clearCourses } from "../../features/courses/coursesSlice";
import { clearTests } from "../../features/tests/testsSlice";

export default function SignedInMenu() {
    const dispatch = useAppDispatch();
    const { user } = useAppSelector(state => state.account);
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const theme = useTheme();
    const isMobileOrTablet = useMediaQuery(theme.breakpoints.down('md'));

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <div>
            <Button
                color='inherit'
                onClick={handleClick}
                sx={{ typography: 'h6', textTransform: 'none' }}
            >
                <AccountCircle sx={{ mr: 1, fontSize: '25px' }} />
                {isMobileOrTablet ? null : user?.username.toUpperCase()}
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
            >
                {/* <MenuItem onClick={handleClose}>Профіль</MenuItem>
                <MenuItem onClick={handleClose}>Мої курси</MenuItem> */}
                <MenuItem onClick={() => {
                    dispatch(signOut());
                    dispatch(clearCourses());
                    dispatch(clearTests());
                }}>Вийти</MenuItem>
            </Menu>
        </div>
    );
}