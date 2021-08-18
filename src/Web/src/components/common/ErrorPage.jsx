import React from 'react';
import { Typography, makeStyles } from "@material-ui/core";
import Lottie from 'react-lottie';

const useStyles = makeStyles({
	animation: {
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		justifyContent: "start",
		flexGrow: 1,
		minHeight: "calc(100vh - 64px - 48px - 61px - 18px)",
	},
});

const ErrorPage = ({ label, animationData }) => {
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
		<div className={classes.animation}>
			<Lottie
				options={defaultOptions}
				height={600}
				width={600}
			/>
			<Typography variant="h4" component="h2" >
				{label}
			</Typography>
		</div>

	);
}

export default ErrorPage;