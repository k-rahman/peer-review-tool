import React from 'react';
import { makeStyles, Tab, Tabs as MaterialTabs } from "@material-ui/core";
import { Error as ErrorIcon } from "@material-ui/icons";
import { useFormikContext } from 'formik';
import _ from 'lodash';

const useStyles = makeStyles((theme) => ({
	tabs: {
		borderRight: `1px solid ${theme.palette.divider}`,
		paddingTop: 16,
	},
}), { name: 'MuiTabs' });

const VerticalTabs = ({ value, handleChange, tabs }) => {
	const classes = useStyles();
	const { errors, touched } = useFormikContext();

	const a11yProps = (index) => {
		return {
			id: `vertical-tab-${index}`,
			'aria-controls': `vertical-tabpanel-${index}`,
		};
	}


	const checkForErrors = tab => {
		for (const field in errors) { // get field name that has an error  ex:("name", "description" ...)
			for (const label in tab) {
				const fields = tab[label];  // get fields from tab

				if (Array.isArray(errors[field])) { // if field value is an array as in ("Criteria")
					for (const item in errors[field])  // get the index of the field in the array
						if (_.get(touched, `${field}.${item}`) && fields.includes(field))  // if the field is touched && exists in the current tab
							return true
				}

				else if (_.get(touched, field) && fields.includes(field)) // if field is touched && exists in the current tab
					return true;
			}
		}
	}

	return (
		<MaterialTabs
			aria-label="Vertical tabs example"
			className={classes.tabs}
			onChange={handleChange}
			orientation="vertical"
			value={value}
			variant="scrollable"
		>
			{tabs.map((tab, index) => (
				<Tab
					key={index}
					icon={checkForErrors(tab) && <ErrorIcon color="secondary" fontSize="small" />}
					label={Object.keys(tab)}
					{...a11yProps(index)}
				/>
			))}
		</MaterialTabs>
	);
}

export default VerticalTabs;