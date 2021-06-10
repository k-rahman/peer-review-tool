import React, { useEffect } from 'react';
import { Typography } from "@material-ui/core";
import * as Yup from "yup";
import { useRouteMatch } from "react-router-dom";
import { useAuth0 } from '@auth0/auth0-react';

import Form from "./forms/Form";
import FormField from './forms/FormField';
import SubmittButton from './forms/SubmitButton';
import useApi from '../hooks/useApi';
import submissions from '../api/submissions';

const validationSchema = Yup.object({
	content: Yup.string().required("Required*").min(5),
});

const initialValues = {
	content: "",
};

const DuringSubmission = _ => {
	const match = useRouteMatch();
	const { user } = useAuth0();
	const { request: getSubmission, data: submission } = useApi(submissions.getSubmission)


	useEffect(_ => {
		getSubmission(match.params.uid, user.sub);
	}, []);

	const handleSubmit = _ => {

	}

	return (
		<>
			<Typography variant="h4">
				Your submission goes here
			</Typography>
			< Form
				initialValues={submission ? submission.content : initialValues}
				validationSchema={validationSchema}
				onSubmit={handleSubmit}
			>
				<FormField
					name="content"
					label="Content"
				/>

				<SubmittButton
					variant="contained"
					color="primary"
					title="Save"
				/>
			</Form>
		</>
	);
}

export default DuringSubmission;