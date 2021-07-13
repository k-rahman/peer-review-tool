import React from 'react';
import { Typography } from "@material-ui/core";

const AfterSubmission = ({ data: reviews }) => {
	const { selfReview } = reviews;

	return (
		<>
			<Typography variant="body1">
				{!selfReview ? "You have not submitted your work! please contact your teacher, thank you." : null} {/*to be decided*/}
			</Typography>
		</>
	);
}

export default AfterSubmission;