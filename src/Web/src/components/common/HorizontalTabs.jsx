import React from 'react';
import { AppBar, makeStyles, Tabs, Tab } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
	root: {
		flexGrow: 1,
		backgroundColor: theme.palette.background.paper,
	},
	appbar: {
		backgroundColor: theme.palette.background.default,
		color: theme.palette.primary.main,
		boxShadow: "0px 1px 4px -1px rgb(0 0 0 / 20%), 0px 2px 0px 0px rgb(0 0 0 / 14%), 0px 0px 0px 0px rgb(0 0 0 / 12%)"
	}
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
			<AppBar position="static" className={classes.appbar}>
				<Tabs
					aria-label="hotizontal tabs"
					onChange={handleChange}
					value={value}
				>

					{tabs?.map((tab, index) => (
						<Tab
							key={index}
							label={tab?.name || tab}
							{...a11yProps(index)}
							disabled={tab?.disabled || false}
						/>
					))}
				</Tabs>
			</AppBar>
		</div>
	);
}

export default HorizontalTabs;