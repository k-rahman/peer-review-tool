import React, { useEffect } from 'react';
import * as Yup from "yup";
import _ from "lodash";
import Dialog from '@material-ui/core/Dialog';
import { Button, IconButton, Typography } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import CloseIcon from '@material-ui/icons/Close';

import Form from "./forms/Form";
import FormField from "./forms/FormField";
import FormSelect from "./forms/FormSelect";
import SubmitButton from "./forms/SubmitButton";

const useStyles = makeStyles((theme) => ({
	appBar: {
		position: 'relative',
	},
	title: {
		marginLeft: theme.spacing(2),
		flex: 1,
	},
}));

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
			maxPoints: ''
		}
	]
};

const ReviewAddEdit = ({ open, handleClose, transition, handleSubmit, data }) => {
	const classes = useStyles();

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
								Review
							</Typography>
							<SubmitButton variant="contained" color="primary" title="Save" />
						</Toolbar>
					</AppBar>

					{data?.grades?.map((r, i) => (
						<>
							<div>{r.description}</div>
							<div>{r.maxPoints}</div>
							<FormField name={`grades.${i}.feedback`} label="Feedback" />
							<FormSelect menuItems={[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20]} name={`grades.${i}.points`} label="Points" />
						</>
					))}

				</Form>

			</Dialog>
		</div>
	);
}

export default ReviewAddEdit;