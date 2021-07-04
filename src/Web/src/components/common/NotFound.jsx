import React from 'react';
import { Typography, makeStyles } from "@material-ui/core";
import Lottie from 'react-lottie';

import animationData from "../../assets/lottie-files/not-found.json";

const useStyles = makeStyles({
	animation: {
		display: "flex",
		flexDirection: "column",
		alignItems: "center",
		justifyContent: "start",
		flexGrow: 1
	},
});

const ErrorPage = () => {
	const classes = useStyles();
	const defaultOptions = {
		loop: true,
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
			<Typography variant="h4" component="h1" >
				Page was not found!
			</Typography>
		</div>

	);
}

export default ErrorPage;