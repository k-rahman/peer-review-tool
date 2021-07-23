import React from "react";
import { useFormikContext } from "formik";
import { TextField } from "@material-ui/core";
import _ from "lodash";
import { makeStyles } from "@material-ui/core";

const withStyles = makeStyles({
	root: props => ({
		flexGrow: 1,
		width: "100%",
		marginBottom: props.marginBottom
	}),
});

const FormField = ({ name, label, isDisabled, multiline = false, marginBottom, inputProps }) => {
	const classes = withStyles({ marginBottom });
	const {
		errors,
		handleChange,
		handleBlur,
		touched,
		values,
	} = useFormikContext();

	return (
		<TextField
			className={classes.root}
			disabled={isDisabled}
			error={_.get(errors, name) && _.get(touched, name)}
			helperText={_.get(touched, name) && _.get(errors, name)}
			InputProps={{ ...inputProps }}
			InputLabelProps={{ shrink: _.get(values, name)?.name, }}
			label={label}
			multiline={multiline}
			name={name}
			onBlur={handleBlur}
			onChange={handleChange}
			value={_.get(values, name)?.name || _.get(values, name) || ""}
			variant="outlined"
			required
			rows={10}
		/>
	);
};

export default FormField;