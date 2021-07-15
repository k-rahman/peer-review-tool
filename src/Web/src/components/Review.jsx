import React, { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { isBefore, isAfter, isValid } from "date-fns";
import { makeStyles, Accordion, AccordionDetails, AccordionSummary, Typography } from "@material-ui/core";
import { ExpandMore as ExpandMoreIcon } from "@material-ui/icons";

import useApi from "../hooks/useApi";
import reviewService from "../api/reviews";
import BeforeReview from './BeforeReview';
import DuringReview from "./DuringReview";
import AfterReview from "./AfterReview";

const useStyles = makeStyles({
	root: {
		padding: [[12, 18]],
		border: 0,
	},
	wrapper: {
		padding: [[6, 16]],
	},
	summary: {
		padding: [[6, 16]],
		margin: [0, "!important"],
	},
	details: {
		display: "block",
	},
	title: {
		fontWeight: [500, "!important"],
	},
	content: {
		padding: [[6, 16]]
	},
	summaryContent: {
		margin: [0, "!important"],
	}
});


const Review = ({ startDate, endDate }) => {
	const classes = useStyles();
	const params = useParams();

	const { request: getReviews, data: reviews } = useApi(reviewService.getReviews);
	const { request: getReviewsSummary, data: reviewsSummary } = useApi(reviewService.getReviewsSummary);

	const [tabValue, setTabValue] = useState(0);
	const [formValueChanged, setFormValueChanged] = useState();
	const [tabChanged, setTabChanged] = useState(false);
	const [newValue, setNewValue] = useState(0);
	const [expanded, setExpanded] = React.useState("review");

	useEffect(() => {
		// // no need to try and fetch reviews during review phase
		// if (isValid(startDate) && isBefore(startDate, new Date()) && isAfter(endDate, new Date())) {
		getReviews(params.uid);
	}, [params.uid]);

	useEffect(() => {
		// // fetch reviews summary when review phase is over
		// if (isValid(endDate) && isBefore(endDate, new Date())) {
		getReviewsSummary(params.uid);
	}, [params.uid]);

	const handleChange = (panel) => (e, isExpanded) => {
		setExpanded(isExpanded ? panel : false);
	};

	const handleTabChange = (event, newValue) => {
		setNewValue(newValue);

		if (formValueChanged) {
			setTabChanged(true);
		}
		else {
			setTabValue(newValue);
			setTabChanged(false);
		}
	};

	const handleFormValueChanged = isChanged => {
		setFormValueChanged(isChanged)
	}

	const handleDiscard = _ => {
		setFormValueChanged(false);
		setTabChanged(false);
		setTabValue(newValue);
	}

	const reviewComponent = _ => {

		// before review start
		if (isAfter(startDate, new Date()))
			return <BeforeReview startDate={startDate} />

		// review started
		if (isBefore(startDate, new Date()) && isAfter(endDate, new Date()))
			return <DuringReview
				data={reviews}
				tabValue={tabValue}
				tabChanged={tabChanged}
				refreshForm={getReviews}
				handleTabChange={handleTabChange}
				handleFormValueChanged={handleFormValueChanged}
				handleDiscard={handleDiscard}
			/>;


		// review ended
		if (isBefore(endDate, new Date()))
			return <AfterReview data={reviewsSummary} />

	}

	return (

		<Accordion
			variant="outlined"
			expanded={expanded === 'review'}
			onChange={handleChange('review')}
			className={classes.root}
		>
			<AccordionSummary
				className={classes.summary}
				classes={{ content: classes.summaryContent }}
				expandIcon={<ExpandMoreIcon />}
				aria-controls="panel1bh-content"
				id="panel1bh-header"
			>
				<Typography variant="h5" className={classes.title}>
					Your Reviews
				</Typography>
			</AccordionSummary>

			<AccordionDetails classes={{ root: classes.details }}>
				{reviewComponent()}
			</AccordionDetails>
		</Accordion>
	);
}

export default Review;
