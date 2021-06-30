import React from "react";
import { AppBar, Toolbar, Typography, makeStyles } from "@material-ui/core";
import AuthenticationButton from "../AuthenticationButton";

const useStyles = makeStyles(theme => ({
	title: {
		flexGrow: 1,
	},
}));

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



export default NavBar;