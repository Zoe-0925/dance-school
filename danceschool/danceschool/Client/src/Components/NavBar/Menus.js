import React, { Fragment } from 'react';
import {
  MenuItem, Divider, IconButton,
} from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles';
/*******/
import { useDotIconMenu } from "../Hooks/useDotIconMenu"
import { DropDownMenu } from "../Buttons/IconButtons"
import AccountCircle from '@material-ui/icons/AccountCircle';

export const useStyles = makeStyles((theme) => ({
    menuButton: {
        marginRight: theme.spacing(2),
    },
    sectionMobile: {
        display: 'flex',
        [theme.breakpoints.up('md')]: {
            display: 'none',
        },
    },
}));


export const AccountMenu = ({ handleSignOut }) => {
    const classes = useStyles();
    const { anchorEl, isOpen, handleMenuClose, handleMenuOpen } = useDotIconMenu()

    return (
        <Fragment>
            <IconButton
                edge="start"
                className={classes.menuButton}
                color="inherit"
                aria-label="open drawer"
                onClick={handleMenuOpen}
            >
                <AccountCircle style={{ "cursor": "pointer" }} />
            </IconButton>
            <DropDownMenu anchorEl={anchorEl} isOpen={isOpen} handleMenuClose={handleMenuClose} >
                <div className="menu-container">
                    <p className="menu-title">ADMIN ACCOUNT</p>
                    <Divider />
                    <MenuItem onClick={handleSignOut}>Log Out</MenuItem>
                </div>
            </DropDownMenu>
        </Fragment>
    )
}
