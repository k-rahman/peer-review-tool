import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom"
import { isAfter } from "date-fns";
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails } from "@material-ui/core";
import { ExpandMore as ExpandMoreIcon } from "@material-ui/icons";

import useApi from "../hooks/useApi";
import reviewService from "../api/reviews";
import HorizontalTabs from './common/HorizontalTabs';
import TabPanel from "./common/TabPanel";
import Participants from "./Participants";
import ReviewSummary from './ReviewSummary';


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

const WorkshopSummary = ({
	data: workshop,
	submissionStartDate,
	submissionEndDate,
	reviewStartDate,
	reviewEndDate,
	reviewsSummary,
	submissions
}) => {

	const { participants } = workshop;

	const classes = useStyles();

	// const {
	// 	request: getSubmissions,
	// 	data: submissions
	// } = useApi(submissionService.getSubmissions);

	const {
		request: getReviewsSummary,
		// data: reviewsSummary
	} = useApi(reviewService.getReviewsSummary);

	const [tabValue, setTabValue] = useState(0);
	const [expanded, setExpanded] = useState("workshop summary");
	const [participantsWithoutSubmission, setParticipantsWithoutSubmission] = useState([]);

	// useEffect(_ => {
	// 	if (isBefore(submissionEndDate, new Date())) // call it if submission phase ended
	// 		getSubmissions(params.uid);
	// }, [submissionEndDate]); // call it on submission phase end

	useEffect(_ => {
		const withoutSubmissions = getParticipantsWithoutSubmissions();
		setParticipantsWithoutSubmission(withoutSubmissions);
	}, [submissions])

	// useEffect(_ => {
	// 	if (isBefore(addMinutes(reviewStartDate, 1), new Date())) // call it after 1 min from review start
	// 		getReviewsSummary(params.uid);
	// }, [reviewStartDate]);

	const handleChange = (panel) => (e, isExpanded) => {
		setExpanded(isExpanded ? panel : false);
	};

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
	};

	const getParticipantsWithoutSubmissions = _ => {
		const withSubmissions = submissions?.map(r => r.authorId);
		const withoutSubmissions = participants?.filter(p => withSubmissions?.indexOf(p.auth0Id) === -1);

		return withoutSubmissions;
	}

	return (
		<Accordion
			variant="outlined"
			expanded={expanded === 'workshop summary'}
			onChange={handleChange('workshop summary')}
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
					Workshop Summary
				</Typography>
			</AccordionSummary>

			<AccordionDetails classes={{ root: classes.details }}>
				<HorizontalTabs
					handleChange={handleTabChange}
					value={tabValue}
					tabs={[
						{ name: "Participants", disabled: false },
						{ name: "No Submissions", disabled: isAfter(submissionEndDate, new Date()) },
						{ name: "Review Summary", disabled: isAfter(reviewStartDate, new Date()) }
					]}
				/>

				<TabPanel value={tabValue} index={0}>
					<Participants data={participants} />
				</TabPanel>

				<TabPanel value={tabValue} index={1}>
					<Participants data={participantsWithoutSubmission} />
				</TabPanel>

				<TabPanel value={tabValue} index={2}>
					<ReviewSummary
						data={reviewsSummary}
						handleRefreshForm={getReviewsSummary}
						reviewStartDate={reviewStartDate}
					/>
				</TabPanel>

				<Typography variant="caption" color="textSecondary" style={{ marginTop: "10px" }} component="div">
					*Participants tab will show "(no name)" for participants, who have not recalim their account yet
				</Typography>
				<Typography variant="caption" color="textSecondary" component="div">
					*No Submissions tab will show students, who did not submit their work at the end of submission phase
				</Typography>
				<Typography variant="caption" color="textSecondary" component="div">
					*Review Summary tab will be available at the start of review phase
				</Typography>
			</AccordionDetails>
		</Accordion>
	);
}

export default WorkshopSummary;