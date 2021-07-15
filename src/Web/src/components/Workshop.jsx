// packages
import React, { useEffect, useState } from 'react';
import { useAuth0 } from '@auth0/auth0-react';
import { makeStyles, Grid, Paper, Typography } from "@material-ui/core";

// custom components
import ErrorPage from './common/ErrorPage';
import TimeLine from "./common/TimeLine";
import WorkshopDetails from './WorkshopDetails';
import WorkshopSummary from './WorkshopSummary';
import Submission from './Submission';
import Review from './Review';

// api
import useApi from "../hooks/useApi";
import workshopService from "../api/workshops";
import submissionService from "../api/submissions";
import reviewService from "../api/reviews";

// lottie
import badRequest from "../assets/lottie-files/bad-request.json";

const useStyles = makeStyles({
	container: {
		minHeight: "calc(100vh - 64px - 48px - 61px - 18px)",
		height: "100%",
	},
	timeLinePaper: {
		width: "100%",
		height: "100%",
		borderRight: "none",
		borderTopRightRadius: 0,
		borderBottomRightRadius: 0,
		// minHeight: 450
	},
	workshopDetailsPaper: {
		height: "100%",
		width: "100%",
		maxHeight: 422,
		borderLeft: "none",
		borderTopLeftRadius: 0,
		borderBottomLeftRadius: 0,
	},
	workshopSummaryPaper: {
		// minHeight: "calc(100vh - 64px - 436px - 61px - 48px - 18px )",
		marginTop: 18,
	},
	submissionPaper: {
		marginTop: 18,
	},
});

const Workshop = ({ match, history }) => {
	const classes = useStyles();
	const { user } = useAuth0();

	const [role, setRole] = useState([]);

	const {
		request: getWorkshopByUid,
		response: workshopResponse,
		data: workshop,
		error: workshopError
	} = useApi(workshopService.getWorkshopByUid);

	const {
		request: getSubmissionDeadlines,
		response: submissionResponse,
		data: submissionDeadlines,
		error: submissionError
	} = useApi(submissionService.getSubmissionDeadlines);

	const {
		request: getReviewDeadlines,
		response: reviewResponse,
		data: reviewDeadlines,
		error: reviewError
	} = useApi(reviewService.getReviewDeadlines);


	const events = [
		{ id: "published", date: workshop?.published, description: "Public" },
		{ id: "start", date: workshop?.submissionStart, description: "Submission" },
		{ id: "deadline", date: workshop?.submissionEnd, description: "Submission Deadline" },
		{ id: "start", date: workshop?.reviewStart, description: "Review" },
		{ id: "deadline", date: workshop?.reviewEnd, description: "Review Deadline" },
	];

	useEffect(() => {
		setRole(user['https://schemas.peer-review-tool/roles']);
	}, [user]);

	useEffect(() => {
		getWorkshopByUid(match.params.uid);

		const getWorkshopIntervalHandle = setInterval(_ => getWorkshopByUid(match.params.uid), 20000);
		return _ => clearInterval(getWorkshopIntervalHandle);
	}, [match.params.uid]);

	useEffect(() => {
		getSubmissionDeadlines(match.params.uid);

		const getSubmissionDatesIntervalHandle = setInterval(_ => getSubmissionDeadlines(match.params.uid), 20000);
		return _ => clearInterval(getSubmissionDatesIntervalHandle);
	}, [match.params.uid]);

	useEffect(() => {
		getReviewDeadlines(match.params.uid);

		const getReviewDatesIntervalHandle = setInterval(_ => getReviewDeadlines(match.params.uid), 20000);
		return _ => clearInterval(getReviewDatesIntervalHandle);
	}, [match.params.uid]);

	if (workshopError && workshopResponse.status === 400) {
		return <ErrorPage
			animationData={badRequest}
			label="You have used wrong workshop link, please contact your teacher!" />
	}

	if (workshopError && workshopResponse.status === 404) {
		history.replace("/NotFound");
	}

	return (
		<div className={classes.container}>
			<Grid container spacing={0} >

				{workshopError && !workshopResponse.status ?
					< Grid item sm={12} md={12} >
						<Paper variant="outlined" >
							<Typography variant="subtitle2" color="secondary" align="center" component="div">
								Workshop service is down at the moment.
							</Typography>
						</Paper>
					</Grid>
					:
					<>
						{/*TimeLine*/}
						< Grid item sm={12} md={5} >
							<Paper variant="outlined" className={classes.timeLinePaper}>
								{<TimeLine events={events} />}
							</Paper>
						</Grid>

						{/*Workshop details*/}
						<Grid item sm={12} md={7} zeroMinWidth >
							<Paper variant="outlined" className={classes.workshopDetailsPaper}>
								<WorkshopDetails data={workshop} />
							</Paper>
						</Grid>
					</>
				}


				{/*teacher workshop results summary*/}
				{role.indexOf("Instructor") !== -1 &&
					<Grid item sm={12}>
						<Paper variant="outlined" className={classes.workshopSummaryPaper}>
							<WorkshopSummary
								data={workshop}
								startDate={new Date(reviewDeadlines?.reviewStart)}
								endDate={new Date(reviewDeadlines?.reviewEnd)}
							/>
						</Paper>
					</Grid>
				}

				{/* submission */}
				{role.indexOf("Participant") !== -1 &&
					<Grid item sm={12}>
						<Paper variant="outlined" className={classes.submissionPaper}>
							{submissionError && !submissionResponse.status ?
								<Typography variant="subtitle2" color="secondary" align="center" component="div">
									Submission service is down at the moment.
								</Typography>
								:
								<Submission
									startDate={new Date(submissionDeadlines?.submissionStart)}
									endDate={new Date(submissionDeadlines?.submissionEnd)}
								/>
							}
						</Paper>
					</Grid>
				}

				{/* reviews */}
				{role.indexOf("Participant") !== -1 &&
					<Grid item sm={12}>
						<Paper variant="outlined" className={classes.submissionPaper}>
							{reviewError && !reviewResponse.status ?
								<>
									<Typography variant="subtitle2" color="secondary" align="center" component="div">
										Review service is down at the moment.
									</Typography>
								</>
								:
								<Review
									startDate={new Date(reviewDeadlines?.reviewStart)}
									endDate={new Date(reviewDeadlines?.reviewEnd)}
								/>
							}
						</Paper>
					</Grid>
				}

			</Grid >
		</div >
	);
}

export default Workshop;