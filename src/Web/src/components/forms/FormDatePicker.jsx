import React from "react";
import _ from "lodash";
import { useFormikContext } from "formik";
import DateFnsUtils from "@date-io/date-fns";
import { KeyboardDateTimePicker, MuiPickersUtilsProvider, DateTimePicker } from '@material-ui/pickers';
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
	root: {
		width: "100%",
		marginTop: 38

	},
});


const FormDatePicker = ({ name, label }) => {
	const classes = useStyles();

	const { setFieldTouched, errors, touched, setFieldValue, values } = useFormikContext();

	const validateOnBlur = () => {
		setFieldTouched(name, true)
	}

	return (
		<MuiPickersUtilsProvider utils={DateFnsUtils} >
			<KeyboardDateTimePicker
				className={classes.root}
				disablePast
				error={_.get(errors, name) && _.get(touched, name)}
				format="dd.MM.yyyy    hh:mm a"
				helperText={_.get(touched, name) && _.get(errors, name)}
				hideTabs
				inputVariant="outlined"
				label={label}
				name={name}
				onBlur={input => input.target.onblur = validateOnBlur()}
				onChange={date => setFieldValue(name, date, true)}
				placeholder="dd.mm.yyyy hh:mm"
				value={_.get(values, name)}
				required
			/>
		</MuiPickersUtilsProvider>
	);
}

export default FormDatePicker;