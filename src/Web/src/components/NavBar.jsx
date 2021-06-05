import React from "react";
import { AppBar, Toolbar, Typography, makeStyles } from "@material-ui/core";
import AuthenticationButton from "./AuthenticationButton";

const NavBar = () => {
	const classes = useStyles();

	return (
		<AppBar position="static">
			<Toolbar>
				<Typography variant="h6" className={classes.title}>
					Peer Review Tool
          </Typography>
				<AuthenticationButton />
			</Toolbar>
		</AppBar>
	);
}


const useStyles = makeStyles(theme => ({
	root: {
		flexGrow: 1,
	},
	menuButton: {
		marginRight: theme.spacing(2),
	},
	title: {
		flexGrow: 1,
	},
}));

export default NavBar;