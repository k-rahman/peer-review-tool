import React from 'react';
import { makeStyles, Grid, Typography, IconButton } from "@material-ui/core";
import { GitHub as GitHubIcon, LinkedIn as LinkedInIcon } from "@material-ui/icons";

const useStyles = makeStyles(theme => ({
	container: {
		backgroundColor: theme.palette.primary.main,
		color: "#fff",
		marginTop: 18,
	},
	copyrights: {
		marginLeft: 106,
		flexGrow: 1,
	},
	iconWrapper: {
		textAlign: "right",
		paddingRight: 18,
	}
}));

const Footer = () => {
	const classes = useStyles();

	const handleGitHubClicked = _ => {
		window.open("https://github.com/k-rahman");
	}

	const handleLinkedInClicked = _ => {
		window.open("https://linkedin.com");
	}

	return (
		<Grid container alignItems="center" className={classes.container}>
			<Grid item container sm={12} lg={12} alignItems="center" justify="center" zeroMinWidth>
				<Typography variant="body2" align="center" className={classes.copyrights}>© {new Date().getFullYear()} Karim Abdelrahman</Typography>
				<Grid item sm={1} lg={1}>
					<IconButton onClick={() => handleGitHubClicked()}>
						<GitHubIcon style={{ color: "#fff" }} />
					</IconButton>
					<IconButton onClick={() => handleLinkedInClicked()}>
						<LinkedInIcon style={{ color: "#fff" }} />
					</IconButton>
				</Grid>
			</Grid>
		</Grid>
	);
}

export default Footer;