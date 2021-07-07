import React, { useState } from 'react';
import { Grid, Paper, Button } from "@material-ui/core";

import ReviewAddEdit from "./ReviewAddEdit";
import useApi from "../hooks/useApi";
import reviewsService from "../api/reviews";

const Review = ({ data: review, title }) => {
	const { request: getReviewGrades, data: grades } = useApi(reviewsService.getGrades)
	const { request: updateReview } = useApi(reviewsService.updateReview);

	const [open, setOpen] = useState(false);

	const handleReviewClicked = async _ => {
		await getReviewGrades(review?.id);
		setOpen(true);
	};

	const handleClose = _ => {
		setOpen(false);
	};

	const handleSubmit = (values, { setSubmitting }) => {
		updateReview(review?.id, values);
		console.log("Submit");
		setTimeout(() => {
			alert(JSON.stringify(values, null, 2));
			setSubmitting(false);
		}, 400);
	};

	return (
		<>
			<Grid item sm={12}>
				<Paper variant="elevation"
					style={{ height: "100%", width: "100", overflow: "auto", maxHeight: 450 }}
				>
					<div>{title}</div>
					<div>{review?.content}</div>
					<Button onClick={() => handleReviewClicked()}>Review</Button>
				</Paper>
			</Grid>

			<ReviewAddEdit
				open={open}
				data={grades}
				handleClose={handleClose}
				handleSubmit={handleSubmit}
			/>

		</>
	);
}

export default Review;