import React from 'react';
import { Redirect } from 'react-router-dom';
import * as Yup from "yup";
import { InputAdornment, Dialog, makeStyles, Typography, } from "@material-ui/core";
import { PermIdentityOutlined as NameIcon } from "@material-ui/icons";

import Form from '../forms/Form';
import FormField from '../forms/FormField';
import SubmitButton from "../forms/SubmitButton";

import useApi from "../../hooks/useApi";
import profileApi from "../../api/profile";
import { useAuth0 } from '@auth0/auth0-react';


const useStyles = makeStyles({
	root: {
		display: "flex",
		flexDirection: "column",
		width: 300,
	},
	wrapper: {
		width: "100%",
	},
	header: {
		backgroundColor: "#ebebeb",
		padding: 11,

	},
	title: {
		width: "100%",
		textAlign: "center",
		marginBottom: 12,
	},
	content: {
		display: "flex",
		width: "100%",
		marginBottom: 10,
		marginTop: 10,
		padding: [[6, 16]],
	},
	submitBtn: {
		border: 0,
		padding: 14,
		display: "block",
		width: "100%",
		overflow: "hidden",
		borderRadius: [[0, 0, 5, 5,]],

	}
});


const CompleteProfile = ({ setHasName }) => {
	const classes = useStyles();
	const { user } = useAuth0();

	const { request: updateUserMetadata } = useApi(profileApi.updateUserMetadata);

	const validationSchema = Yup.object({
		firstname: Yup.string().required("Required*").min(2, "Must be at least 2 characters"),
		lastname: Yup.string().required("Required*").min(2, "Must be at least 2 characters"),
	});

	const initialValues = {
		firstname: "",
		lastname: "",
	}

	const handleSubmit = async (values) => {
		const response = await updateUserMetadata(values);

		if (response.ok) {
			setHasName(true);
			window.location.replace("/"); // using window object to navigate, cuz i need a reload on the page to retreive the name from auth0
		}
	};

	return (
		<>
			{user && user.name ? ( // redirect back to homepage if auth0 user object already has a name set *In case, component url was typed in manually*
				<Redirect to="/" />
			)
				:

				(<div className={classes.root}>
					<Dialog open={true}>
						<Form
							initialValues={initialValues}
							onSubmit={handleSubmit}
							validationSchema={validationSchema}
							enableReinitialize={true}
						>
							<div className={classes.header}>
								<Typography variant="h6" className={classes.title}>
									Peer Review Tool
								</Typography>
							</div>
							<div className={classes.bodyWrapper}>
								<div className={classes.content}>
									<FormField name="firstname" label="First Name"
										inputProps={{
											startAdornment: (
												<InputAdornment position="start">
													<NameIcon fontSize="small" />
												</InputAdornment>
											),
										}}
									/>
								</div>
								<div className={classes.content}>
									<FormField name="lastname" label="Last Name" inputProps={{
										startAdornment: (
											<InputAdornment position="start">
												<NameIcon fontSize="small" />
											</InputAdornment>
										),
									}} />
								</div>
							</div>

							<SubmitButton variant="contained" color="secondary" title="Save" styles={{
								width: "100%",
								borderRadius: [[0, 0, 5, 5]],
								padding: 14,
							}} />
						</Form>
					</Dialog>
				</div>
				)}
		</>
	);
}

export default CompleteProfile;