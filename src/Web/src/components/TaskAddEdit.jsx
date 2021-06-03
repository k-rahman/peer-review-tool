import React from "react";
import * as Yup from "yup";
import Dialog from '@material-ui/core/Dialog';
import { Button, IconButton, Typography } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import CloseIcon from '@material-ui/icons/Close';

import useApi from "../hooks/useApi";
import tasksApi from "../api/tasks";
import Form from "./forms/Form";
import FormField from "./forms/FormField";
import FormFieldArray from "./forms/FormFieldArray";
import SubmitButton from "./forms/SubmitButton";
import FormDatePicker from "./forms/FormDatePicker";
import FormUpload from "./forms/FormUpload";

const useStyles = makeStyles((theme) => ({
	appBar: {
		position: 'relative',
	},
	title: {
		marginLeft: theme.spacing(2),
		flex: 1,
	},
}));

const TaskAddEdit = ({ open, handleClose, transition, data }) => {
	const classes = useStyles();
	const { error: addError, request: addItem } = useApi(tasksApi.addTask);

	const validationSchema = Yup.object({
		name: Yup.string().required("Required*").min(5),
		description: Yup.string().required("Required*").min(5),
		submissionStart: Yup.date().required("Required*").nullable(),
		submissionEnd: Yup.date().required("Required*").nullable(),
		reviewStart: Yup.date().required("Required*").nullable(),
		reviewEnd: Yup.date().required("Required*").nullable(),
		published: Yup.date().required("Required*").nullable(),
		criteria: Yup.array().min(1).of(
			Yup.object({
				description: Yup.string().required("Required*"),
				maxPoints: Yup.number().required("Required*")
			})
		),
	});

	const initialValues = {
		name: "",
		description: "",
		participantsEmails: null,
		submissionStart: new Date(),
		submissionEnd: new Date(),
		reviewStart: new Date(),
		reviewEnd: new Date(),
		published: new Date(),
		criteria: [
			{
				description: "",
				maxPoints: 0
			}
		]
	}

	const handleSubmit = (values, { setSubmitting }) => {
		addItem(values);
		setTimeout(() => {
			if (!addError) {
				alert(JSON.stringify(values, null, 2));
				setSubmitting(false);
			} else {
				alert("Something went wrong!");
			}
		}, 400);
	};

	console.log(data);

	return (
		<div>
			<Dialog fullWidth={true} maxWidth='md' closeAfterTransition open={open} onClose={handleClose} TransitionComponent={transition}>
				<Form
					initialValues={data ? data : initialValues}
					onSubmit={handleSubmit}
					validationSchema={validationSchema}
				>
					<AppBar className={classes.appBar}>
						<Toolbar>
							<IconButton edge="start" color="inherit" onClick={handleClose} aria-label="close">
								<CloseIcon />
							</IconButton>
							<Typography variant="h6" className={classes.title}>
								Create a new assignment
							</Typography>
							<SubmitButton variant="contained" color="primary" title="Create new task" />
						</Toolbar>
					</AppBar>

					<FormField name="name" label="Name" />
					<FormField name="description" label="Description" />

					<FormUpload variant="contained" color="primary" title="CSV file" name="participantsEmails" />

					<FormDatePicker name="submissionStart" label="Submission Start" />
					<FormDatePicker name="submissionEnd" label="Submission End" />
					<FormDatePicker name="reviewStart" label="Review Start" />
					<FormDatePicker name="reviewEnd" label="Review End" />
					<FormDatePicker name="published" label="Published" />

					<FormFieldArray name="criteria" value={{ description: "", maxPoints: 0 }}>
						{(index) => (
							<>
								<FormField
									name={`criteria.${index}.description`}
									label="criterion"
									cssClass="criteria-field"
								/>
								<FormField
									name={`criteria.${index}.maxPoints`}
									label={'Max Points'}
									cssClass="max-points-field"
								/>
							</>
						)}
					</FormFieldArray>
				</Form>

			</Dialog>
		</div>
	);
}

export default TaskAddEdit;