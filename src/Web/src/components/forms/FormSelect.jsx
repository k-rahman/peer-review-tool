import React from 'react';
import _ from "lodash";
import { FormControl, InputLabel, Select, MenuItem, FormHelperText } from "@material-ui/core";
import { useFormikContext } from "formik";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
	formControl: {
		marginLeft: theme.spacing(1),
		minWidth: 92,
	},
	label: {
		position: "relative"
	},
	selectEmpty: {
		marginTop: theme.spacing(2)
	},
	menuPaper: {
		maxHeight: 350
	}
}));

const FormSelect = ({ menuItems, name, label, isDisabled, isReadOnly }) => {
	const classes = useStyles();
	const { errors, touched, values, handleChange } = useFormikContext();

	return (
		<FormControl required className={classes.formControl} variant="outlined">
			<InputLabel className={classes.label}>{label}</InputLabel>
			<Select
				readOnly={isReadOnly}
				disabled={isDisabled}
				name={name}
				value={_.get(values, name)}
				onChange={handleChange}
				error={_.get(errors, name) && _.get(touched, name)}
				MenuProps={{ classes: { paper: classes.menuPaper } }}
			>
				{menuItems.map((item, index) => (
					<MenuItem key={index} value={item}>{item}</MenuItem>
				))}
			</Select>
			{_.get(touched, name) && <FormHelperText>{_.get(errors, name)}</FormHelperText>}
		</FormControl >
	);
}



export default FormSelect;