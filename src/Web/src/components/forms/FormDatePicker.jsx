import React from "react";
import _ from "lodash";
import { useFormikContext } from "formik";
import DateFnsUtils from "@date-io/date-fns";
import { KeyboardDateTimePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';

const FormDatePicker = ({ name, label }) => {
  const { setFieldTouched, errors, touched, setFieldValue, values } = useFormikContext();

  const validateOnBlur = () => {
    _.get(errors, name) && setFieldTouched(name, true);
  }

  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils} >
      <KeyboardDateTimePicker
        disablePast
        error={_.get(errors, name) && _.get(touched, name)}
        format="dd.MM.yyyy    hh:mm a"
        fullWidth
        helperText={_.get(touched, name) && _.get(errors, name)}
        hideTabs
        inputVariant="outlined"
        label={label}
        name={name}
        onBlur={input => input.target.onblur = validateOnBlur()}
        onChange={date => setFieldValue(name, date, true)}
        placeholder="dd.mm.yyyy hh:mm"
        required
        value={_.get(values, name)}
      />
    </MuiPickersUtilsProvider>
  );
}

export default FormDatePicker;