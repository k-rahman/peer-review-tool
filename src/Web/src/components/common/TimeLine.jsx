import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Timeline from '@material-ui/lab/Timeline';
import TimelineItem from '@material-ui/lab/TimelineItem';
import TimelineSeparator from '@material-ui/lab/TimelineSeparator';
import TimelineConnector from '@material-ui/lab/TimelineConnector';
import TimelineContent from '@material-ui/lab/TimelineContent';
import TimelineOppositeContent from '@material-ui/lab/TimelineOppositeContent';
import TimelineDot from '@material-ui/lab/TimelineDot';
import { } from '@material-ui/icons';
import { Icon } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import { format, isAfter, isBefore } from "date-fns";

const useStyles = makeStyles((theme) => ({
	paper: {
		padding: '6px 16px',
	},
	secondaryTail: {
		backgroundColor: theme.palette.secondary.main,
	},
}));

const TimeLine = ({ events }) => {
	const classes = useStyles();

	const today = new Date();
	let startEvent = null;

	const highlightCurrentEvent = event => {
		if (event.date === undefined)
			return

		if (event.id === "start")
			startEvent = event;

		const startDate = startEvent !== null ? startEvent.date : new Date();

		if (isAfter(new Date(event.date), today) && isBefore(new Date(startDate), today)) {
			event.color = "primary"
			event.icon = "flag";
		}
		else if (isAfter(new Date(event.date), today)) {
			event.color = "grey";
			event.icon = "schedule";
		}
		else {
			event.color = "secondary";
			event.icon = "close";
		}
	};

	return (
		<Timeline align="left">
			{events.map((event, index) => (
				<TimelineItem>
					{highlightCurrentEvent(event)}
					<TimelineOppositeContent>
						<Typography variant="body2" color="textSecondary">
							{event.date && format(new Date(event.date), "dd.MM.yyyy")}
						</Typography>
					</TimelineOppositeContent>
					<TimelineSeparator>
						<TimelineDot color={event.color}>
							<Icon >{event.icon}</Icon>
						</TimelineDot>
						{index < events.length - 1 && <TimelineConnector />}
					</TimelineSeparator>
					<TimelineContent>
						<Paper elevation={3} className={classes.paper}>
							<Typography variant="h6" component="h1">
								{event.description}
							</Typography>
							<Typography>some comment</Typography>
						</Paper>
					</TimelineContent>
				</TimelineItem>
			))}
		</Timeline>
	);
}

export default TimeLine;