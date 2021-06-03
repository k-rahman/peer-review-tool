import React, { useState } from 'react';
import { useFormikContext } from "formik";
import { Button, TextField } from "@material-ui/core";
import _ from "lodash";
import styles from "../../assets/styles/form-field.module.css";

const FormUpload = ({ name, title, color, variant }) => {
	const { values, setFieldValue, errors, touched } = useFormikContext();
	const [textFieldValue, setTextFieldValue] = useState("");

	const handleChange = e => {
		const file = e.currentTarget.files[0];
		console.log(file);
		setFieldValue(name, file);
		setTextFieldValue(file.name);

		// const reader = new FileReader();

		// reader.readAsDataURL(file);
		// let base64String;

		// reader.onload = await function (event) {
		// 	base64String = event.target?.result
		// 	setFieldValue(name, base64String);
		// }
	}

	console.log(values)

	return (
		<>
			<input id="participants-upload" type="file" accept=".csv" onChange={handleChange} name={name} />

			<div style={{ display: 'flex' }}>
				<TextField
					className={`${styles["form-field"]} ${styles["upload-field"]}`}
					error={_.get(errors, name) && _.get(touched, name)}
					disabled
					helperText={_.get(touched, name) && _.get(errors, name)}
					label="csv file"
					value={textFieldValue}
					variant="standard"
				/>

				<label htmlFor="participants-upload" style={{ alignSelf: "flex-end" }}>
					<Button variant={variant} color={color} component="span">
						{title}
					</Button>
				</label>
			</div>
		</>
	);
}

export default FormUpload;