import React, { useState, useEffect } from 'react';
import { useParams } from "react-router-dom"
import _ from "lodash";
import { isAfter, isBefore, isValid } from "date-fns";
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


const WorkshopSummary = ({ data: workshop, startDate, EndDate }) => {
	const { participants } = workshop;

	const classes = useStyles();
	const params = useParams();

	const {
		request: getReviewsSummary,
		data: reviewsSummary
	} = useApi(reviewService.getReviews);

	const [tabValue, setTabValue] = useState(0);
	const [expanded, setExpanded] = useState("workshop summary");
	const [participantsWithoutSubmission, setParticipantsWithoutSubmission] = useState([]);

	useEffect(_ => {
		if (isValid(startDate) && isBefore(startDate, new Date()))
			getReviewsSummary(params.uid);
	}, [params.uid, startDate]);

	useEffect(_ => {
		const withoutSubmissions = getParticipantsWithoutSubmissions();

		setParticipantsWithoutSubmission(withoutSubmissions);
	}, [reviewsSummary])

	const handleChange = (panel) => (e, isExpanded) => {
		setExpanded(isExpanded ? panel : false);
	};

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
	};

	const getParticipantsWithoutSubmissions = _ => {
		const withSubmissions = reviewsSummary?.reviewsSummary?.map(r => r.participantAuth0Id);
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
						{ name: "non-submitters", disabled: isAfter(new Date(workshop?.submissionStart), new Date()) },
						{ name: "Review Summary", disabled: isAfter(new Date(workshop?.reviewStart), new Date()) }
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
						handleRefreshForm={getReviewsSummary} />
				</TabPanel>

				<Typography variant="body2" color="textSecondary" style={{ marginTop: "10px" }}>
					*Review Summary will open when review phase starts
				</Typography>
			</AccordionDetails>
		</Accordion>
	);
}

export default WorkshopSummary;