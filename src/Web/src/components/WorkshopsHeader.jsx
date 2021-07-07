import React from 'react';
import { Typography, Fab, makeStyles } from "@material-ui/core";
import { Add as PlusIcon } from "@material-ui/icons";

const useStyles = makeStyles({
	root: {
		display: "flex",
		justifyContent: "space-between",
		alignItems: "flex-end",
		textAlign: "left",
		padding: [[18, 28]]

	}
});

const WorkshopsHeader = ({ handleAddClick, isInstructor }) => {
	const classes = useStyles();

	return (
		<Typography variant="h4" className={classes.root} component="h1">
			Workshops
			{isInstructor &&
				<span>
					<Fab color="secondary" aria-label="add" onClick={handleAddClick}>
						<PlusIcon />
					</Fab>
				</span>
			}
		</Typography>
	);
}

export default WorkshopsHeader;