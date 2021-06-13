import React, { useEffect } from 'react';
import { Typography } from "@material-ui/core";
import { useRouteMatch } from "react-router-dom";
import { useAuth0 } from '@auth0/auth0-react';
import useApi from '../hooks/useApi';
import submissions from '../api/submissions';

const AfterSubmission = () => {
	const match = useRouteMatch();
	const { user } = useAuth0();
	const { request: getSubmission, data: submission } = useApi(submissions.getSubmission)


	useEffect(_ => {
		getSubmission(match.params.uid, user.sub);
	}, []);

	return (
		<>
			<Typography variant="h4">Your submitted submission</Typography>
			<Typography variant="body">
				{submission?.content ? submission.content : "You have not submitted your work! please contact your teacher, thank you."}
			</Typography>
		</>
	);
}

export default AfterSubmission;