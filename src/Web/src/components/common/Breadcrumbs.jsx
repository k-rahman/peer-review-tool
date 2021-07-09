import React from "react";
import { Link, useLocation } from "react-router-dom";
import { Chip, emphasize, Breadcrumbs as MaterialBreadcrumbs, withStyles, makeStyles } from "@material-ui/core";
import { HammerWrench as WorkshopIcon, } from "mdi-material-ui";

// Styles
const StyledBreadcrumb = withStyles(theme => ({
	root: {
		backgroundColor: theme.palette.grey[100],
		height: theme.spacing(3),
		color: theme.palette.grey[800],
		fontWeight: theme.typography.fontWeightRegular,
		"&:hover, &:focus": {
			backgroundColor: theme.palette.grey[300],
		},
		"&:active": {
			boxShadow: theme.shadows[1],
			backgroundColor: emphasize(theme.palette.grey[300], 0.12),
		},
	},
}))(Chip);

const useStyles = makeStyles({
	root: {
		margin: [[18, 28]]
	},
});

// Component
const Breadcrumbs = _ => {
	const classes = useStyles();
	const location = useLocation();
	const pathnames = location.pathname.split("/").filter(x => x).slice(1);

	return (
		<MaterialBreadcrumbs aria-label="breadcrumb" className={classes.root}>
			<StyledBreadcrumb
				component={Link}
				to="/workshops"
				label="Workshops"
				icon={<WorkshopIcon />}
			/>
			{pathnames.map((value, index) => {
				const last = index === pathnames.length - 1;
				const to = `/${pathnames.slice(0, index + 1).join("/")}`;

				return last ? (
					<StyledBreadcrumb key={to} label={value} />
				) : (
					<StyledBreadcrumb key={to} component={Link} to={to} label={value} />
				);
			})
			}
		</MaterialBreadcrumbs >
	);
}

export default Breadcrumbs;