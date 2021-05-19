import React from "react";
import { useFormikContext } from "formik";
import { TextField } from "@material-ui/core";
import _ from "lodash";

const FormField = ({ name, label, variant }) => {
	const {
		errors,
		values,
		touched,
		handleChange,
		handleBlur
	} = useFormikContext();

	return (
		<TextField
			error={_.get(errors, name) && _.get(touched, name)}
			helperText={_.get(touched, name) && _.get(errors, name)}
			label={label}
			onChange={handleChange}
			onBlur={handleBlur}
			name={name}
			value={_.get(values, name)}
			variant={variant}
		/>
	);
};

export default FormField;