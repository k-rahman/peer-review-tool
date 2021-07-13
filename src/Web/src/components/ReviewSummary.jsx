import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Paper, List, ListItem, ListItemText, Button, makeStyles, Typography } from "@material-ui/core";
import { Add as AddIcon, Edit as EditIcon, Visibility as OpenIcon } from '@material-ui/icons';
import {
	GridComponent,
	ColumnDirective,
	ColumnsDirective,
	ColumnChooser,
	ColumnMenu,
	Toolbar,
	Page,
	Filter,
	Reorder,
	Resize,
	Sort,
	Inject
} from "@syncfusion/ej2-react-grids";

import Dialog from "./common/Dialog";
import Submission from "./AfterSubmission";
import Review from "./ReviewView";
import TeacherReview from "./ReviewAddEdit";
import SubmitButton from './forms/SubmitButton';
import AppBar from './common/AppBar';

import "../assets/styles/workshops-table.css";

const useStyles = makeStyles({
	root: {
		padding: [[8, 8]],
	},
	wrapper: {
		width: "100%",
		padding: [[6, 16]],
	},
	title: {
		fontWeight: [500, "!important"],
		marginBottom: 12,
	},
	content: {
		padding: [[6, 16]],
	},
	contentWrapper: {
		width: "100%",
		padding: [[0, 8]],
	},
	contentPaper: {
		marginBottom: 12,
	},
	editColumn: {
		padding: [0, '!important'],
		borderRight: [[1, "solid", "#e0e0e0"], "!important"]
	},
	openBtnText: {
		height: "100%",
		textTransform: "none",
		minWidth: 55,
		"&:disabled": {
			color: "#ff1744"
		},
		"& span": {
			justifyContent: "flex-end"

		},
	},
	list: {
		width: "100%",
		paddingTop: 0,
		paddingBottom: 0
	},
	listItem: {
		padding: 0
	},
	listItemText: {
		width: "100%",
		height: "100%",
		textTransform: "none",
		display: "flex",
		alignItems: "center",
		justifyContent: "space-between",
	},
})

const ReviewSummary = ({ data: summary, handleRefreshForm }) => {
	const classes = useStyles();
	const params = useParams();

	const [openDialog, setOpenDialog] = useState(false);
	const [content, setContent] = useState();


	const handleOpen = _ => {
		setOpenDialog(true);
	}

	const handleClose = _ => {
		setOpenDialog(false);
	}

	const teacherReviewColTemplate = props => {
		if (props.teacherReview == null)
			return <Typography variant="caption"> no value</Typography>;

		const { teacherReview: { grades }, maxPointsSum } = props;
		const total = grades?.reduce((acc, g) => { return acc + g.points; }, 0);
		const noReview = grades?.every(g => g.points === null);

		return (
			<Paper>
				<Button
					fullWidth
					size="small"
					color="primary"
					onClick={() => {
						handleOpen();
						renderTeacherReview(props.teacherReview);
					}}
				>
					{noReview ? <AddIcon /> : <EditIcon />}
					<Typography variant="body2" className={classes.openBtnText}>
						{noReview ? "Review" : `${total}/${maxPointsSum}`}
					</Typography>
				</Button>
			</Paper>
		);
	};

	const peerReviewsColTemplate = props => {
		if (props.peerReviews == null)
			return <Typography variant="caption"> no value</Typography>;

		const { peerReviews, participant, maxPointsSum } = props;

		return (
			<List dense className={classes.list}>
				{peerReviews.map((review, i) => {
					const { grades, reviewer } = review;
					const total = grades?.reduce((acc, g) => { return acc + g.points; }, 0);
					const noReview = grades.every(g => g.points === null);

					return (
						<ListItem key={i} className={classes.listItem}>
							<div className={classes.listItemText}>
								<ListItemText>
									{review.reviewer}
								</ListItemText>
								<ListItemText>
									<Button
										fullWidth
										size="small"
										onClick={() => {
											handleOpen();
											renderParticipantReview(participant, reviewer, grades);
										}}
										disabled={noReview}
										color={noReview ? "secondary" : "primary"}
										className={classes.openBtnText}
									>
										{!noReview && <OpenIcon />}
										<Typography variant="body2" className={classes.openBtnText}>
											{noReview ? "No review" : `${total}/${maxPointsSum}`}
										</Typography>
									</Button>
								</ListItemText>
							</div>
						</ListItem>
					);
				})}
			</List>
		);
	};

	const selfReviewColTemplate = props => {
		if (props.selfReview === null)
			return <Typography variant="caption"> no value</Typography>;

		const { selfReview: { grades, reviewer }, maxPointsSum, participant } = props;
		const total = grades?.reduce((acc, g) => { return acc + g.points; }, 0);

		if (grades?.every(g => g.points === null))
			return (
				<Typography color="secondary" variant="body2" >
					No review
				</Typography >
			);


		return (
			<Button
				fullWidth
				size="small"
				color="primary"
				onClick={() => {
					handleOpen();
					renderParticipantReview(participant, reviewer, grades);
				}}>
				{/* <ReviewIcon /> */}
				<OpenIcon />
				<Typography variant="body2" className={classes.openBtnText}>
					{`${total}/${maxPointsSum}`}
				</Typography>
			</Button >
		);
	};

	const submissionColTemplate = props => {
		const { participant, submissionContent } = props;

		return (
			<Button
				fullWidth
				size="small"
				color="primary"
				onClick={() => {
					handleOpen();
					renderSubmission(participant, submissionContent)
				}}>
				<OpenIcon />
				<Typography variant="body2" className={classes.openBtnText}>Check out</Typography>
			</Button>
		);
	};

	const renderSubmission = (participant, content) => {
		setContent(
			<>
				<AppBar title="Submission" close={handleClose} />
				<div className={classes.root}>
					<div className={classes.wrapper}>
						<Typography variant="h6" className={classes.title}>
							Author
						</Typography>
						<div className={classes.contentWrapper}>
							<Paper variant="outlined" className={classes.contentPaper}>
								<Typography className={classes.content}>
									{participant}
								</Typography>
							</Paper>
						</div>
						<Typography variant="h6" className={classes.title}>
							Content
						</Typography>
						<div className={classes.contentWrapper}>
							<Paper variant="outlined" className={classes.contentPaper}>
								<div className={classes.content}>
									<Submission data={content} />
								</div>
							</Paper>
						</div>
					</div>
				</div>
			</>
		);
	}

	const renderParticipantReview = (participant, reviewer, grades) => {
		setContent(
			<>
				<AppBar title={participant === reviewer ? "Self Review" : "Peer Review"} close={handleClose} />
				<Review data={{ reviewer, grades }} />
			</>
		);
	}

	const renderTeacherReview = review => {
		setContent(
			<TeacherReview
				data={review}
				refreshForm={handleRefreshForm}
				AppBar={
					<AppBar
						position="relative"
						button={<SubmitButton variant="contained" color="secondary" title="Save" />}
						close={handleClose}
						title="Teacher Review"
					/>
				}
				showSubmit={false}
			/>
		);
	}

	const filterSettings = { type: "CheckBox" };
	const pageOptions = { pageSizes: ["10", "20", "30", "40"], pageSize: "20" };

	return (
		<>
			<Dialog open={openDialog} close={handleClose}>
				{content}
			</Dialog>
			<div className={classes.root}>
				<GridComponent
					dataSource={summary?.reviewsSummary}
					allowFiltering={true}
					allowPaging={true}
					allowReordering={true}
					allowResizing={true}
					allowSorting={true}
					filterSettings={filterSettings}
					pageSettings={pageOptions}
					showColumnChooser={true}
					showColumnMenu={true}
				>
					<ColumnsDirective>
						<ColumnDirective field="participant" headerText='Participants' headerTextAlign="center" showInColumnChooser={false} minWidth="200" width="220" />
						<ColumnDirective headerText='Submission' headerTextAlign="center" template={submissionColTemplate} minWidth="180" width="180" />
						<ColumnDirective headerText='Self Review' textAlign="center" headerTextAlign="center" template={selfReviewColTemplate} minWidth="151" width="151" />
						<ColumnDirective headerText='Peer Reviews' textAlign="center" headerTextAlign="center" template={peerReviewsColTemplate} minWidth="300" maxWidth="340" width="300" />
						<ColumnDirective field="average" headerText="Average" textAlign="center" headerTextAlign="center" minWidth="140" width="140" />
						<ColumnDirective headerText='Teacher Review' textAlign="center" headerTextAlign="center" template={teacherReviewColTemplate} />
					</ColumnsDirective>
					<Inject services={[ColumnChooser, ColumnMenu, Filter, Reorder, Resize, Page, Sort, Toolbar]} />
				</GridComponent>
			</div>
		</>
	);
}

export default ReviewSummary;