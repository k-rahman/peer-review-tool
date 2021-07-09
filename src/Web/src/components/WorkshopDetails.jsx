import React from 'react';
import { Paper, List, ListItem, ListItemText, ListItemIcon, Accordion, AccordionSummary, AccordionDetails, Typography, makeStyles } from '@material-ui/core';
import { ExpandMore as ExpandMoreIcon, LabelImportantTwoTone as ListIcon } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
	root: {
		width: '100%',
		height: "100%",
		display: "flex",
		flexDirection: "column",
		justifyContent: "start",
		overflow: "auto",
		padding: [[12, 18]],
		margin: 0
	},
	wrapper: {
		padding: [[6, 16]],
	},
	details: {
		padding: [[0, 16]],
	},
	title: {
		fontWeight: [500, "!important"],
		padding: [[6, 0]],
	},
	list: {
		width: "100%",
		paddingTop: 0,
	},
	listItem: {
		paddingLeft: 0,
		paddingRight: 20
	},
	listItemText: {
		display: "flex",
		justifyContent: "space-between",
		width: "100%",
	},
	icon: {
		marginRight: 5,
	},
	maxPoints: {
		textAlign: "right",
	},
}));

const WorkshopDetails = ({ workshop }) => {
	const classes = useStyles();
	const [expanded, setExpanded] = React.useState("description");

	const handleChange = (panel) => (e, isExpanded) => {
		setExpanded(panel === "description" ? "description" : "criteria")
		// setExpanded(isExpanded ? panel : false);
	};
	return (
		<div className={classes.root}>
			<div className={classes.wrapper}>
				<Typography variant="h5" className={classes.title}>{workshop.name}</Typography>
			</div>
			<div className={classes.wrapper}>
				<Accordion expanded={expanded === 'description'} onChange={handleChange('description')}>
					<AccordionSummary
						className={classes.summary}
						expandIcon={<ExpandMoreIcon />}
						aria-controls="panel1bh-content"
						id="panel1bh-header"
					>
						<Typography variant="h6">Description</Typography>
					</AccordionSummary>
					<AccordionDetails>
						<Typography variant="body2">{workshop.description}</Typography>
					</AccordionDetails>
				</Accordion>
			</div>
			<div className={classes.wrapper}>
				<Accordion expanded={expanded === 'criteria'} onChange={handleChange('criteria')}>
					<AccordionSummary
						className={classes.summary}
						expandIcon={<ExpandMoreIcon />}
						aria-controls="panel1bh-content"
						id="panel1bh-header"
					>
						<Typography variant="h6">Evaluation Criteria</Typography>
					</AccordionSummary>
					<AccordionDetails className={classes.details}>
						<List dense className={classes.list}>
							<Typography variant="subtitle2" align="right">Max Points</Typography>
							{workshop.criteria?.map((c, i) => (
								<ListItem key={i} className={classes.listItem}>
									<ListIcon color="primary" className={classes.icon} />
									<div className={classes.listItemText}>
										<ListItemText>{c.description}</ListItemText>
										<ListItemText className={classes.maxPoints}>{c.maxPoints}</ListItemText>
									</div>
								</ListItem>
							))}
						</List>
					</AccordionDetails>
				</Accordion>
			</div>
		</div>
	);
}

export default WorkshopDetails;