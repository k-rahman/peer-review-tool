import React, { useEffect } from "react";
import { useFormikContext } from "formik";
import DateFnsUtils from "@date-io/date-fns";
import {
	KeyboardDateTimePicker,
	MuiPickersUtilsProvider,
} from '@material-ui/pickers';
import styles from "../../assets/styles/form-date-picker.module.css";

const FormDatePicker = ({ name, label }) => {
	const { errors, touched, setFieldValue, values } = useFormikContext();

	return (
		<MuiPickersUtilsProvider utils={DateFnsUtils}>
			<KeyboardDateTimePicker
				className={styles['date-time-picker']}
				error={errors[name] && touched[name]}
				format="dd.MM.yyyy"
				helperText={touched[name] && errors[name]}
				label={label}
				// minDate={new Date()}
				onChange={date => setFieldValue(name, date)}
				placeholder="dd.mm.yyyy"
				value={values[name]}
			/>
		</MuiPickersUtilsProvider>
	);
}

export default FormDatePicker;