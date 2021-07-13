import React from 'react';
import { formatDistanceToNow } from "date-fns";
import { Typography } from "@material-ui/core";

const BeforeReview = ({ startDate }) => {
	return (
		<Typography variant="h5">
			You can review your work and your peers works {formatDistanceToNow(new Date(startDate), { addSuffix: true })}
		</Typography>
	);
}

export default BeforeReview;