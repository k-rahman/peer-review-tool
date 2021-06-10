import React from 'react';
import { formatDistanceToNow } from "date-fns";
import { Typography } from "@material-ui/core";

const BeforeSubmission = ({ startDate }) => {
	return (
		<Typography variant="h3">
			You can submit {formatDistanceToNow(new Date(startDate), { addSuffix: true })}
		</Typography>
	);
}

export default BeforeSubmission;