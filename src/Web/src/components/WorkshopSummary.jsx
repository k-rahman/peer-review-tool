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

import useApi from "../hooks/useApi";
import reviewService from "../api/reviews";

import Dialog from "./common/Dialog";
import Submission from "./AfterSubmission";
import Review from "./ReviewView";
import TeacherReview from "./Review";

import "../assets/styles/workshops-table.css";

const useStyles = makeStyles({
	root: {
		padding: 20,
	},
	wrapper: {
		width: "100%",
		padding: [[6, 16]],
	},
	header: {
		fontWeight: [500, "!important"],
		padding: [[6, 16]],
	},
	content: {
		padding: [[6, 16]],
	},
	editColumn: {
		padding: [0, '!important'],
		borderRight: [[1, "solid", "#e0e0e0"], "!important"]
	},
	openBtnText: {
		height: "100%",
		textTransform: "none",
		paddingLeft: 5,
		"&:disabled": {
			color: "#ff1744"
		}
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
		justifyContent: "space-between",
	},
})

const WorkshopSummary = () => {
	const classes = useStyles();
	const params = useParams();

	const [openDialog, setOpenDialog] = useState(false);
	const [title, setTitle] = useState();
	const [content, setContent] = useState();
	const [dialogData, setDialogData] = useState([]);

	const { request: getReviewsSummary, data: summary } = useApi(reviewService.getReviews);

	useEffect(_ => {
		getReviewsSummary(params.uid);
	}, [params.uid]);


	const handleOpen = (participant, content) => {
		setOpenDialog(true);
		// setDialogData({ participant, content });
	}

	const handleClose = _ => {
		setOpenDialog(false);
	}

	const teacherReviewColTemplate = props => {
		const { teacherReview: { grades }, maxPointsSum } = props;
		const total = grades?.reduce((acc, g) => { return acc + g.points; }, 0);

		// return (
		// 	<Button
		// 		fullWidth
		// 		size="small"
		// 		color="primary"
		// 		onClick={() => {
		// 			handleOpen()
		// 			renderReviewAddEdit(props.teacherReview);
		// 		}}>
		// 		{grades ? <EditIcon /> : <AddIcon />}
		// 		<Typography variant="body2" className={classes.openBtnText}>
		// 			{grades ? `${total}/${maxPointsSum}` : "Review"}
		// 		</Typography>
		// 	</Button >
		// );


		return <TeacherReview
			data={props?.teacherReview}
			title="Teacher Review"
			btnContent={<>{grades ? <EditIcon /> : <AddIcon />}
				<Typography variant="body2" className={classes.openBtnText}>
					{grades ? `${total}/${maxPointsSum}` : "Review"}
				</Typography></>
			} />
	};

	const peerReviewsColTemplate = props => {
		const { peerReviews, participant, maxPointsSum } = props;

		return (
			<List dense className={classes.list}>
				{peerReviews.map((review, i) => {
					const { grades, reviewer } = review;
					const total = grades?.reduce((acc, g) => { return acc + g.points; }, 0);

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
											renderReview(participant, reviewer, grades);
										}}
										disabled={!grades}
										color={!grades ? "secondary" : "primary"}
										className={classes.openBtnText}
									>
										{grades && <OpenIcon />}
										<Typography variant="body2" className={classes.openBtnText}>
											{!grades ? "No review" : `${total}/${maxPointsSum}`}
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
		const { selfReview: { grades, reviewer }, maxPointsSum, participant } = props;

		if (grades === null)
			return (
				<Typography color="secondary" variant="body2" >
					No review
				</Typography >
			);

		const total = grades.reduce((acc, g) => { return acc + g.points; }, 0);

		return (
			<Button
				fullWidth
				size="small"
				color="primary"
				onClick={() => {
					handleOpen();
					renderReview(participant, reviewer, grades);
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
		setTitle("Submission");
		setContent(
			<>
				<div className={classes.wrapper}>
					<Typography variant="h6" className={classes.header}>
						Author
					</Typography>
					<div className={classes.wrapper}>
						<Paper variant="outlined" className={classes.content}>
							<Typography>
								{participant}
							</Typography>
						</Paper>
					</div>
					<Typography variant="h6" className={classes.header}>
						Content
					</Typography>
					<div className={classes.wrapper}>
						<Paper variant="outlined" className={classes.content}>
							<Submission data={content} />
						</Paper>
					</div>
				</div>
			</>
		);
	}

	const renderReview = (participant, reviewer, grades) => {
		if (participant === reviewer)
			setTitle("Self Review")
		else
			setTitle("Peer Review");

		setContent(<Review data={{ reviewer, grades }} />);
	}

	// const renderReviewAddEdit = review => {
	// 	setTitle("Teacher Review");
	// 	setContent()

	// };

	// const toolbarOptions = ['ColumnChooser'];
	const filterSettings = { type: "CheckBox" };
	const pageOptions = { pageSizes: ["10", "20", "30", "40"], pageSize: "20" };

	return (
		<>
			{/* <SubmissionSummary open={openDialog} close={handleCloseDialog} data={dialogData} /> */}
			<Dialog open={openDialog} close={handleClose} title={title} styles={{ display: "flex" }}>
				{/* <Submission /> */}
				{content}
			</Dialog>
			<div className={classes.root}>
				<div className={classes.wrapper}>
					<Typography variant="h5" className={classes.header}>Workshop Summary</Typography>
				</div>
				<div className={classes.wrapper}>
					<GridComponent
						dataSource={summary?.reviewsSummary}
						allowFiltering={true}
						allowPaging={true}
						allowReordering={true}
						allowResizing={true}
						allowSorting={true}
						// allowTextWrap={true}
						filterSettings={filterSettings}
						pageSettings={pageOptions}
						showColumnChooser={true}
						showColumnMenu={true}
					// toolbar={toolbarOptions}
					>
						<ColumnsDirective>
							<ColumnDirective field="participant" headerText='Participants' headerTextAlign="center" showInColumnChooser={false} minWidth="245" width="245" />
							<ColumnDirective headerText='Submission' headerTextAlign="center" template={submissionColTemplate} minWidth="135" width="135" />
							<ColumnDirective headerText='Self Review' textAlign="center" headerTextAlign="center" template={selfReviewColTemplate} minWidth="151" width="151" />
							<ColumnDirective headerText='Peer Reviews' textAlign="center" headerTextAlign="center" template={peerReviewsColTemplate} minWidth="405" width="405" />
							<ColumnDirective field="average" headerText="Average" textAlign="center" headerTextAlign="center" minWidth="124" width="124" />
							<ColumnDirective headerText='Teacher Review' textAlign="center" headerTextAlign="center" template={teacherReviewColTemplate} minWidth="145" width="145" />
						</ColumnsDirective>
						<Inject services={[ColumnChooser, ColumnMenu, Filter, Reorder, Resize, Page, Sort, Toolbar]} />
					</GridComponent>
				</div>
			</div>
		</>
	);
}

export default WorkshopSummary;