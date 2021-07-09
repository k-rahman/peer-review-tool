import React from 'react';
import { makeStyles, AppBar as MaterialAppBar, Toolbar, IconButton, Typography } from '@material-ui/core';
import { Close as CloseIcon } from "@material-ui/icons";

const useStyles = makeStyles(theme => ({
	appBar: {
		position: 'relative',
	},
	title: {
		marginLeft: theme.spacing(2),
		flex: 1,
	},
}));

const AppBar = ({ position, close, title, button }) => {
	const classes = useStyles();

	return (
		<MaterialAppBar position={position} className={classes.appBar}>
			<Toolbar>
				<IconButton edge="start" color="inherit" onClick={close} aria-label="close">
					<CloseIcon />
				</IconButton>
				<Typography variant="h6" className={classes.title}>
					{title}
				</Typography>
				{button}
			</Toolbar>
		</MaterialAppBar>
	);
}

export default AppBar;