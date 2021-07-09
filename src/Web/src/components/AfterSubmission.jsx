import React from 'react';
import { Typography } from "@material-ui/core";

const AfterSubmission = ({ data: submission }) => {

	return (
		<>
			<Typography variant="body1">
				{submission ? submission : "You have not submitted your work! please contact your teacher, thank you."}
			</Typography>
		</>
	);
}

export default AfterSubmission;