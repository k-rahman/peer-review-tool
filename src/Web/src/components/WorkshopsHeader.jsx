import React from 'react';
import { Typography, Fab, makeStyles } from "@material-ui/core";
import { Add as PlusIcon } from "@material-ui/icons";

const useStyles = makeStyles({
	wrapper: {
		width: "100%",
		padding: 8,
	},
	title: {
		display: "flex",
		justifyContent: "space-between",
		alignItems: "flex-end",
		textAlign: "left",
		marginBottom: 12,
	}
});

const WorkshopsHeader = ({ handleAddClick, isInstructor }) => {
	const classes = useStyles();

	return (
		<div className={classes.wrapper}>
			<Typography variant="h4" component="h1" className={classes.title}>
				Workshops
				{isInstructor &&
					<span>
						<Fab color="secondary" aria-label="add" onClick={handleAddClick}>
							<PlusIcon />
						</Fab>
					</span>
				}
			</Typography>
		</div>
	);
}

export default WorkshopsHeader;