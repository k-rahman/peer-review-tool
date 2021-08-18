import React from 'react';
import { makeStyles } from "@material-ui/core";
import * as Yup from "yup";

import Form from "./forms/Form";
import FormField from './forms/FormField';
import SubmittButton from './forms/SubmitButton';

const withStyles = makeStyles({
	wrapper: {
		width: "100%",
		padding: [[6, 16]],
	},
	content: {
		marginBottom: 24,
	},
	submitBtn: {
		width: "100%",
		padding: [[6, 0]],
		textAlign: "right"
	},
});

const DuringSubmission = ({ handleSubmit, data }) => {
	const classes = withStyles();

	const validationSchema = Yup.object({
		content: Yup.string().required("Required*").min(5),
	});

	const initialValues = {
		content: "",
	};

	return (
		<>
			< Form
				initialValues={{ content: data?.content } || initialValues}
				validationSchema={validationSchema}
				onSubmit={handleSubmit}
				enableReinitialize={true}
			>

				<div className={classes.wrapper}>
					<div className={classes.content}>
						<FormField
							name="content"
							label="Content"
							multiline
						/>
					</div>

					<div className={classes.submitBtn}>
						<SubmittButton
							variant="contained"
							color="primary"
							title="Save"
						/>
					</div>
				</div>
			</Form>
		</>
	);
}

export default DuringSubmission;