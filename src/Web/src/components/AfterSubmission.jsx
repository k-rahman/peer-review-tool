import React from 'react';
import { makeStyles, Typography } from "@material-ui/core";

const useStyles = makeStyles({
	content: {
		padding: [[6, 16]],
	}
});


const AfterSubmission = ({ data: submission }) => {
	const classes = useStyles();

	return (
		<>
			<Typography variant="body1" className={classes.content}>
				{submission ? submission : "You have not submitted your work! please contact your teacher, thank you."}
			</Typography>
		</>
	);
}

export default AfterSubmission;