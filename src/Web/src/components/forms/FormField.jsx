import React from "react";
import { useFormikContext } from "formik";
import { TextField } from "@material-ui/core";
import _ from "lodash";
import { makeStyles } from "@material-ui/core";

const withStyles = makeStyles({
	root: props => ({
		width: "100%",
		marginTop: props.marginTop
	}),
});

const FormField = ({ name, label, isDisabled, isReadOnly = false, multiline = false, marginTop }) => {
	const classes = withStyles({ marginTop });
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
			error={_.get(errors, name) && !isReadOnly && _.get(touched, name)}
			helperText={_.get(touched, name) && !isReadOnly && _.get(errors, name)}
			InputProps={{ readOnly: isReadOnly, }}
			InputLabelProps={{ shrink: _.get(values, name)?.name, }}
			label={label}
			multiline={multiline}
			name={name}
			onBlur={handleBlur}
			onChange={handleChange}
			value={_.get(values, name)?.name || _.get(values, name)}
			variant="outlined"
			required
			rows={5}
		/>
	);
};

export default FormField;