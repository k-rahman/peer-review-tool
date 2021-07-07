import React from 'react';
import { AppBar, makeStyles, Tabs, Tab } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
	root: {
		flexGrow: 1,
		backgroundColor: theme.palette.background.paper,
	},
}));

const HorizontalTabs = ({ handleChange, tabs, value }) => {
	const classes = useStyles();

	const a11yProps = (index) => {
		return {
			id: `hortizontal-tab-${index}`,
			'aria-controls': `horizontal-tabpanel-${index}`,
		};
	}

	return (
		<div className={classes.root}>
			<AppBar position="static" color="default">
				<Tabs
					aria-label="hotizontal tabs"
					onChange={handleChange}
					value={value}
				>

					{tabs?.map((tab, index) => (
						<Tab
							key={index}
							label={tab}
							{...a11yProps(index)}
						/>
					))}
				</Tabs>
			</AppBar>
		</div>
	);
}

export default HorizontalTabs;