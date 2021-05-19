import React from "react";
import { useFormikContext } from "formik";
import DateFnsUtils from "@date-io/date-fns";
import {
	KeyboardDateTimePicker,
	MuiPickersUtilsProvider,
} from '@material-ui/pickers';

const FormDatePicker = ({ name, label }) => {
	const { errors, touched, setFieldValue, values } = useFormikContext();

	console.log(values[name], name);

	return (
		<MuiPickersUtilsProvider utils={DateFnsUtils}>
			<KeyboardDateTimePicker
				error={errors[name] && touched[name]}
				format="dd.MM.yyyy"
				inputVariant="outlined"
				helperText={touched[name] && errors[name]}
				label={label}
				minDate={new Date()}
				onChange={date => setFieldValue(name, date)}
				placeholder="dd.mm.yyyy"
				value={values[name]}
				variant="inline"
			/>
		</MuiPickersUtilsProvider>
	);
}

export default FormDatePicker;