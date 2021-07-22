import React from 'react';
import { List, ListItem, ListItemText, Paper, makeStyles, Typography } from "@material-ui/core";
import { LabelImportantTwoTone as ListIcon } from "@material-ui/icons";
import { useAuth0 } from '@auth0/auth0-react';

const useStyles = makeStyles({
	root: {
		padding: [[6, 18]],
	},
	wrapper: {
		width: "100%",
		padding: [[6, 16]],
	},
	title: {
		width: "100%",
		fontWeight: [500, "!important"],
		marginBottom: 12,
	},
	subtitle: {
		width: "100%",
		padding: [[6, 0]],
	},
	content: {
		padding: [[6, 16]],
	},
	contetnWrapper: {
		width: "100%",
		padding: [[0, 8]],
	},
	subContentWrapper: {
		flexGrow: 1,
		width: "100%",
		padding: [[6, 16]]
	},
	contentPaper: {
		marginBottom: 12,
	},
	details: {
		padding: [[0, 16]],
	},
	list: {
		width: "100%",
		paddingTop: 0,
	},
	listItem: {
		padding: 0,
		paddingBottom: 6,
		flexWrap: "wrap",
	},
	listItemText: {
		marginRight: 5,
		marginTop: 0,
	},
	criteriaWrapper: {
		display: "flex",
	},
	icon: {
		marginRight: 5,
	},
});

const ReviewView = ({ data }) => {
	const classes = useStyles();
	const { user } = useAuth0();

	return (
		<div className={classes.root}>
			<div className={classes.wrapper}>
				{user['https://schemas.peer-review-tool/roles'].indexOf("Instructor") !== -1 &&
					<>
						<Typography variant="h6" className={classes.title}>
							Reviewer
						</Typography>
						<div className={classes.contetnWrapper}>
							<Paper variant="outlined" className={classes.contentPaper}>
								<Typography className={classes.content}>
									{data?.reviewer}
								</Typography>
							</Paper>
						</div>
					</>
				}
				<Typography variant="h6" className={classes.title}>
					Review
				</Typography>
				<List className={classes.list}>
					{data?.grades?.map((g, i) => (
						<ListItem key={i} className={classes.listItem}>
							<div className={classes.criteriaWrapper}>
								<ListIcon color="primary" className={classes.icon} />
								<ListItemText className={classes.listItemText}>{g.description}</ListItemText>
							</div>
							{g.points === null ?
								<Typography variant="body2" color="secondary" component="div">
									This criterion was not reviewed.
								</Typography>
								:
								<div className={classes.subContentWrapper} >
									<Typography variant="body2" className={classes.subtitle}>
										Points
									</Typography>
									<Paper variant="outlined" className={classes.contentPaper}>
										<Typography variant="body1" className={classes.content}>
											{g.points}<b>/{g.maxPoints}</b>
										</Typography>
									</Paper>
									<Typography variant="body2" className={classes.subtitle}>
										Feedback
									</Typography>
									<Paper variant="outlined" className={classes.contentPaper}>
										<Typography variant="body1" className={classes.content}>
											{g.feedback}
										</Typography>
									</Paper>
								</div>
							}
						</ListItem>
					))}
				</List>
			</div>
		</div>
	);
}

export default ReviewView;