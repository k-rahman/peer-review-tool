import React, { useEffect } from "react";
import { useFormikContext } from "formik";
import { TextField } from "@material-ui/core";
import _ from "lodash";
import styles from "../../assets/styles/form-field.module.css";

const FormField = ({ name, label, variant, isDisabled, cssClass }) => {
	const {
		errors,
		values,
		touched,
		handleChange,
		handleBlur,
	} = useFormikContext();

	return (
		<TextField
			className={`${styles["form-field"]} ${styles[cssClass]}`}
			error={_.get(errors, name) && _.get(touched, name)}
			disabled={isDisabled}
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