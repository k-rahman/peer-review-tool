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
	},
});

const Wait = ({ label, animationData, width, height }) => {
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
				height={height}
				width={width}
			/>
			<Typography variant="body1" component="h2" >
				{label}
			</Typography>
		</div>
	);
}

export default Wait;