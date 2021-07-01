import React from 'react';
import { makeStyles, Tab, Tabs as MaterialTabs } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
	root: {
		overflow: "visible",
	},
	tabs: {
		borderRight: `1px solid ${theme.palette.divider}`,
		paddingTop: 30,
	},
}), { name: 'MuiTabs' });

const VerticalTabs = ({ value, handleChange, tabs }) => {
	const classes = useStyles();

	const a11yProps = (index) => {
		return {
			id: `vertical-tab-${index}`,
			'aria-controls': `vertical-tabpanel-${index}`,
		};
	}

	return (
		<MaterialTabs
			orientation="vertical"
			variant="scrollable"
			value={value}
			onChange={handleChange}
			aria-label="Vertical tabs example"
			className={classes.tabs}
		>
			{tabs.map((label, index) => <Tab key={index} label={label} {...a11yProps(index)} />)}
		</MaterialTabs>
	);
}

export default VerticalTabs;