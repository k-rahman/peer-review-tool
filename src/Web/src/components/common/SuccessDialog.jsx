import React from 'react';
import { IconButton, AppBar, Toolbar, Typography, makeStyles, Slide, Dialog } from "@material-ui/core";
import { Close as CloseIcon } from "@material-ui/icons";
import Lottie from 'react-lottie';

import animationData from "../../assets/lottie-files/success-lottie.json";

const useStyles = makeStyles({
	successDialog: {
		minHeight: 600,
		minWidth: 400,
		display: "flex",
		flexWrap: "wrap",
		justifyContent: "center",
	},
	appBar: {
		position: 'relative',
	},
	animation: {
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		justifyContent: "center"
	},
});

const Transition = React.forwardRef((props, ref) => {
	return <Slide direction="up" ref={ref} {...props} />;
});

const SuccessDialog = ({ children, open, close, label }) => {
	const classes = useStyles();
	const defaultOptions = {
		loop: false,
		autoplay: true,
		animationData,
		rendererSettings: {
			preserveAspectRatio: "xMidYMid slice"
		},
	};

	return (
		<Dialog
			closeAfterTransition
			fullWidth={true}
			maxWidth='md'
			open={open}
			onClose={close}
			TransitionComponent={Transition}
		>
			<AppBar className={classes.appBar}>
				<Toolbar>
					<IconButton edge="start" color="inherit" onClick={close} aria-label="close">
						<CloseIcon />
					</IconButton>
				</Toolbar>
			</AppBar>
			<div className={classes.successDialog}>
				<div className={classes.animation}>
					<Lottie
						options={defaultOptions}
						height={300}
						width={300}
					/>
					<Typography variant="h6" component="h2" >
						{label}
					</Typography>
				</div>
				{children}
			</div>
		</Dialog >
	);
}

export default SuccessDialog;