import React from "react";
import { Button, makeStyles } from "@material-ui/core";
import { useAuth0 } from "@auth0/auth0-react";

const withStyles = makeStyles({
	root: {
		width: "50%",
		margin: 16,
		padding: [[10, 24]],
	},
});

const LogoutButton = _ => {
	const classes = withStyles();
	const { logout } = useAuth0();

	return (
		<Button
			className={classes.root}
			color="secondary"
			variant="contained"
			onClick={() => logout({
				returnTo: window.location.origin,
			})
			}
		>
			Log Out
		</Button>
	);
};

export default LogoutButton;