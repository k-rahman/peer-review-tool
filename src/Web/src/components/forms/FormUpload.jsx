import React from 'react';
import { useFormikContext } from "formik";
import { Button, makeStyles } from "@material-ui/core";

import FormField from './FormField';

const withStyles = makeStyles({
	input: {
		display: "none"
	},
	uploadBtn: {
		width: "100%",
		padding: [[14, 0]],
		textAlign: "right"
	}

});

const FormUpload = ({ name, title }) => {
	const classes = withStyles();
	const { setFieldValue } = useFormikContext();

	const handleChange = e => {
		const file = e.currentTarget.files[0];
		setFieldValue(name, file);
	}

	return (
		<>
			<input
				id="participants-upload"
				type="file"
				accept=".csv"
				onChange={handleChange}
				name={name}
				className={classes.input}
			/>

			<FormField
				isDisabled={true}
				label="csv file"
				name={name}
			/>

			<div className={classes.uploadBtn}>
				<label htmlFor="participants-upload">
					<Button variant="contained" color="primary" component="span">
						{title}
					</Button>
				</label>
			</div>
		</>
	);
}

export default FormUpload;