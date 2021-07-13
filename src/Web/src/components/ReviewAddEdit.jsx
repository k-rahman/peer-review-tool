import React from 'react';
import { useParams } from 'react-router-dom';
import * as Yup from "yup";
import _ from "lodash";
import { toast } from "react-toastify";
import { List, ListItem, ListItemText, makeStyles, Paper, Typography } from '@material-ui/core';
import { LabelImportantTwoTone as ListIcon } from "@material-ui/icons";

import Form from "./forms/Form";
import FormField from "./forms/FormField";
import FormSelect from "./forms/FormSelect";
import Submission from "./AfterSubmission";

import useApi from "../hooks/useApi";
import reviewService from "../api/reviews";
import SubmitButton from './forms/SubmitButton';
import Confirmation from './Confirmation';

const useStyles = makeStyles((theme) => ({
	root: {
		padding: 18,
	},
	wrapper: {
		width: "100%",
		padding: [[6, 16]],
	},
	title: {
		width: "100%",
		fontWeight: [500, "!important"],
		marginBottom: 12,
	},
	subtitle: {
		width: "100%",
		fontWeight: [500, "!important"],
		padding: [[6, 0]],
	},
	content: {
		display: "flex",
		marginBottom: 12,
		marginTop: 12,
		padding: [[6, 16]],
	},
	contentWrapper: {
		padding: [[0, 8]],
	},
	list: {
		width: "100%",
		paddingTop: 0,
	},
	listItem: {
		padding: 0,
		flexWrap: "wrap",
	},
	listItemText: {
		marginTop: 0,
	},
	criteriaWrapper: {
		display: "flex",
		width: "100%",
	},
	maxPointsWrapper: {
		display: "flex",
		flexDirection: "column",
		justifyContent: "center",
	},
	icon: {
		marginRight: 5,
	},
	submitBtn: {
		width: "100%",
		padding: [[6, 28]],
		textAlign: "right"
	},
}));


const ReviewAddEdit = ({
	data: review,
	AppBar,
	refreshForm,
	handleDiscard,
	checkFormChanged,
	checkChanges,
	showSubmit = true
}) => {

	const { request: updateReview } = useApi(reviewService.updateReview);

	const classes = useStyles();
	const params = useParams();

	const validationSchema = Yup.object({
		grades: Yup.array().min(1).of(
			Yup.object({
				feedback: Yup.string().required("Required*").nullable(),
				points: Yup.number().required("Required*").nullable()
			})
		),
	});

	const initialValues = {
		grades: [
			{
				feedback: "",
				points: ""
			}
		]
	};

	const handleSubmit = async (values) => {
		const response = await updateReview(review?.id, values.grades);
		refreshForm(params.uid);

		if (response.ok)
			toast.success("Saved Successfully.", { style: { backgroundColor: "#005577" } });
	}

	return (
		<Form
			initialValues={review || initialValues}
			onSubmit={handleSubmit}
			validationSchema={validationSchema}
			enableReinitialize={true}
			checkChanges={checkChanges}
		>
			{AppBar} {/* optional AppBar like in case of dialog*/}

			<Confirmation open={checkFormChanged} handleClose={handleDiscard} />
			<div className={classes.root}>
				<div className={classes.wrapper}>
					<Typography variant="h6" className={classes.title}>
						Content To Review
					</Typography>
					<div className={classes.contentWrapper}>
						<Paper variant="outlined" className={classes.content}>
							<Submission data={review?.content} />
						</Paper>
					</div>
					<Typography variant="h6" className={classes.title}>
						Review
					</Typography>
					<List className={classes.list}>
						{review?.grades?.map((g, i) => (
							<ListItem key={i} className={classes.listItem}>
								<div className={classes.criteriaWrapper}>
									<ListIcon color="primary" className={classes.icon} />
									<ListItemText className={classes.listItemText}>{g.description}</ListItemText>
								</div>
								<div className={classes.wrapper}>
									<div className={classes.content}>
										<FormSelect
											label="Points"
											menuItems={_.range(0, g.maxPoints + 1)}
											name={`grades.${i}.points`}
											styles={{ margin: [[0, 12, 0, 0]] }}
										/>
										<div className={classes.maxPointsWrapper}>
											<Typography align="center" variant="body1">/{g.maxPoints}</Typography>
										</div>
									</div>
									<div className={classes.content}>
										<FormField name={`grades.${i}.feedback`} label="Feedback" multiline />
									</div>
								</div>
							</ListItem>
						))}
					</List>
					{showSubmit &&
						<div className={classes.submitBtn}>
							<SubmitButton variant="contained" color="primary" title="Save" />
						</div>
					}
				</div>
			</div>
		</Form>
	);
}

export default ReviewAddEdit;