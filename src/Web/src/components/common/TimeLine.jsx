import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Timeline from '@material-ui/lab/Timeline';
import TimelineItem from '@material-ui/lab/TimelineItem';
import TimelineSeparator from '@material-ui/lab/TimelineSeparator';
import TimelineConnector from '@material-ui/lab/TimelineConnector';
import TimelineContent from '@material-ui/lab/TimelineContent';
import TimelineOppositeContent from '@material-ui/lab/TimelineOppositeContent';
import TimelineDot from '@material-ui/lab/TimelineDot';
import { Icon } from '@material-ui/core';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import { formatDistanceToNow, format, isAfter, isBefore } from "date-fns";

const useStyles = makeStyles((theme) => ({
	timeLine: {
		height: "100%",
		margin: 0,
		padding: 18,
		justifyContent: "center",
	},
	timeLineOpposite: {
		flex: 0,
	},
	eventContent: {
		padding: [[6, 16]],
	},
	secondaryTail: {
		backgroundColor: theme.palette.secondary.main,
	},
}));

const TimeLine = ({ events }) => {
	const classes = useStyles();

	const today = new Date();
	let startEvent = null;

	const setEventProps = event => {
		if (event.date === undefined)
			return

		if (event.id === "start")
			startEvent = event;

		const startDate = startEvent !== null ? startEvent.date : new Date(); // publish is the only event without start and deadline

		if (isAfter(new Date(event.date), today) && isBefore(new Date(startDate), today)) { // between start and deadline events (this will be "deadline" events only)
			event.color = "primary";
			event.icon = "flag";
			event.status = "Closes";
		}
		else if (isAfter(new Date(event.date), today)) { // event is in the future
			event.color = "grey";
			event.icon = "schedule";
			if (event.id === "start") event.status = "Starts";
			if (event.id === "published") event.status = "Opens";
		}
		else { // event is over
			event.color = "secondary";
			event.icon = "close";
			if (event.id === "start") event.status = "Started";
			if (event.id === "published") event.status = "Published";
			if (event.id === "deadline") event.status = "Closed";
		}
	};

	return (
		<Timeline align="left" className={classes.timeLine}>
			{events.map((event, index) => (
				<TimelineItem key={index}>
					{setEventProps(event)}
					<TimelineOppositeContent className={classes.timeLineOpposite}>
						<Typography variant="body2" color="textSecondary">
							{event.date && format(new Date(event.date), "dd.MM.yyyy hh:mm a")}
						</Typography>
					</TimelineOppositeContent>
					<TimelineSeparator>
						<TimelineDot color={event.color}>
							<Icon >{event.icon}</Icon>
						</TimelineDot>
						{index < events.length - 1 && <TimelineConnector />}  {/* remove timeline connector from last element*/}
					</TimelineSeparator>
					<TimelineContent>
						<Paper elevation={3} className={classes.eventContent}>
							<Typography variant="h6" component="h1">
								{event.description}
							</Typography>
							<Typography variant="body2">
								{(event.date && event.status) &&
									`${event.status} ${formatDistanceToNow(new Date(event.date), { addSuffix: true })}`}
							</Typography>
						</Paper>
					</TimelineContent>
				</TimelineItem>
			))}
		</Timeline>
	);
}

export default TimeLine;
