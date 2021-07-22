import React from 'react';
import { useParams } from 'react-router';
import { isBefore, isAfter } from "date-fns";
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails } from "@material-ui/core";
import { ExpandMore as ExpandMoreIcon } from "@material-ui/icons";
import { useAuth0 } from '@auth0/auth0-react';
import { toast } from 'react-toastify';

import useApi from '../hooks/useApi';
import submissions from '../api/submissions';
import BeforeSubmission from './BeforeSubmission';
import DuringSubmission from './DuringSubmission';
import AfterSubmission from './AfterSubmission';

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


const Submission = ({ submission, startDate, endDate }) => {
	const classes = useStyles();
	const params = useParams();
	const { user } = useAuth0();

	// const { request: getSubmission, error, data: submission } = useApi(submissions.getSubmission)
	const { request: createSubmission } = useApi(submissions.createSubmission)
	const { request: updateSubmission } = useApi(submissions.updateSubmission)

	const [expanded, setExpanded] = React.useState("submission");

	// useEffect(_ => {
	// 	getSubmission(params.uid);
	// }, [isBefore(startDate, new Date()) && isAfter(endDate, new Date())]);

	const handleChange = (panel) => (e, isExpanded) => {
		setExpanded(isExpanded ? panel : false);
	};

	const handleSubmit = async values => {
		let response;
		if (submission === null || submission?.length === 0) {
			response = await createSubmission(params.uid, { ...values, author: user.name });
		}
		else {
			response = await updateSubmission(submission.id, { ...values, author: user.name });
		}

		if (response.ok) {
			toast.success("Saved Successfully.",
				{ style: { background: "#005577" } }
			);
		}
	}

	const submissionComponent = _ => {

		// before submission start
		if (isAfter(startDate, new Date()))
			return <BeforeSubmission startDate={startDate} />

		// submission started
		if (isBefore(startDate, new Date()) && isAfter(endDate, new Date()))
			return <DuringSubmission handleSubmit={handleSubmit} data={submission} />

		// submission ended
		if (isBefore(endDate, new Date()))
			return <AfterSubmission data={submission.content} />
	}

	return (

		<Accordion
			variant="outlined"
			expanded={expanded === 'submission'}
			onChange={handleChange('submission')}
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
					Your Submission
				</Typography>
			</AccordionSummary>

			<AccordionDetails>
				{submissionComponent()}
			</AccordionDetails>
		</Accordion>
	);
}

export default Submission;