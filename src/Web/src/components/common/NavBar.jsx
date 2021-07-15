import React from "react";
import { AppBar, Popover, Toolbar, Typography, makeStyles, IconButton } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import AuthenticationButton from "../AuthenticationButton";
import { useAuth0 } from "@auth0/auth0-react";

const useStyles = makeStyles(theme => ({
	title: {
		flexGrow: 1,
	},
	container: {
		display: "flex",
		flexDirection: "column",
		justifyContent: "space-between",
		alignItems: "center",
		width: 300,
		height: 350,
	},
	body: {
		display: "flex",
		flexDirection: "column",
		justifyContent: "center",
		alignItems: "center",
		margin: [[20, 33]],
		flexGrow: 1,
		width: "100%",
		overflow: "hidden"
	},
	image: {
		borderRadius: 25,
		width: 100,
		height: 100,
		marginBottom: 16,
		marginTop: 16,
	},
	name: {
		letterSpacing: "0.29px",
		margin: 0,
		textOverflow: "ellipsis",
		overflow: "hidden",
	},
	email: {
		color: "#5f6368",
		textOverflow: "ellipsis",
		overflow: "hidden"
	},
	footer: {
		width: "100%",
		textAlign: "center",
		borderBottom: [[1, "solid", "#e8eaed"]],
		borderTop: [[1, "solid", "#e8eaed"]],
		padding: [[0, 17]],
		marginTop: 16,
		marginBottom: 16,
	},
}));

const NavBar = () => {
	const classes = useStyles();
	const { user } = useAuth0();

	const [anchorEl, setAnchorEl] = React.useState(null);

	const handleClick = (event) => {
		setAnchorEl(event.currentTarget);
	};

	const handleClose = () => {
		setAnchorEl(null);
	};

	const open = Boolean(anchorEl);
	const id = open ? 'simple-popover' : undefined;

	return (
		<AppBar position="static">
			<Toolbar>
				<Typography variant="h6" className={classes.title}>
					Peer Review Tool
				</Typography>
				<div>
					<IconButton
						aria-label="account of current user"
						aria-controls="menu-appbar"
						aria-haspopup="true"
						onClick={handleClick}
						color="inherit"
					>
						<AccountCircle fontSize="large" />
					</IconButton>

					<Popover
						id={id}
						open={open}
						anchorEl={anchorEl}
						classes={{
							paper: classes.container
						}}
						onClose={handleClose}
						anchorOrigin={{
							vertical: 'bottom',
							horizontal: 'center',
						}}
						transformOrigin={{
							vertical: 'top',
							horizontal: 'center',
						}}
					>
						<div className={classes.body}>
							<img src={user?.picture} className={classes.image} alt="user avatar" />
							<Typography variant="h5" className={classes.name}>{user?.name}</Typography>
							<Typography variant="subtitle2" className={classes.email}>{user?.email}</Typography>
						</div>
						<div className={classes.footer}>
							<AuthenticationButton />
						</div>
					</Popover>
				</div>
			</Toolbar>
		</AppBar >
	);
}



export default NavBar;