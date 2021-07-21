import React from 'react';
import { useFormikContext } from 'formik';
import Dialog from "./common/Dialog";
import { DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from "@material-ui/core";

const Confirmation = ({ open, handleClose }) => {
	const { handleSubmit } = useFormikContext();

	return (
		<Dialog open={open}>
			<DialogTitle id="alert-dialog-title">{"You have unsaved changes!"}</DialogTitle>
			<DialogContent>
				<DialogContentText id="alert-dialog-description">
					You have not saved your changes. Do you want to save now?
				</DialogContentText>
			</DialogContent>
			<DialogActions>
				<Button onClick={() => {
					handleClose();
				}} color="secondary">
					Discard
				</Button>
				<Button onClick={() => {
					handleSubmit();
					handleClose();
				}} color="primary" autoFocus>
					Save
				</Button>
			</DialogActions>
		</Dialog>
	);
}

export default Confirmation;