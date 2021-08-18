import React from 'react';
import _ from "lodash";
import { FormControl, InputLabel, Select, MenuItem, FormHelperText } from "@material-ui/core";
import { useFormikContext } from "formik";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(theme => ({
	formControl: styles => ({
		minWidth: 92,
		flexGrow: 1,
		margin: styles.margin,
		width: styles.width,
	}),
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

const FormSelect = ({ menuItems, name, label, isDisabled, isReadOnly, styles }) => {
	const classes = useStyles(styles);
	const { errors, touched, values, handleChange } = useFormikContext();

	return (
		<FormControl required className={classes.formControl} variant="outlined">
			<InputLabel>{label}</InputLabel>
			<Select
				disabled={isDisabled}
				error={_.get(errors, name) && _.get(touched, name)}
				label={`${label}`}
				MenuProps={{ classes: { paper: classes.menuPaper } }}
				name={name}
				onChange={handleChange}
				readOnly={isReadOnly}
				value={_.get(values, name) === 0 ? 0 : _.get(values, name) || ""} // default value to empty string to avoid MUI warning
			>
				{menuItems.map((item, index) => (
					<MenuItem key={index} value={item}>{item}</MenuItem> // items to select from 
				))}
			</Select>
			{_.get(touched, name) && <FormHelperText color="secondary">{_.get(errors, name)}</FormHelperText>}
		</FormControl >
	);
}

export default FormSelect;