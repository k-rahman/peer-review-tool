import React from "react";
import { useFormikContext } from "formik";
import { Button, makeStyles } from "@material-ui/core";

const withStyles = makeStyles({
	root: styles => ({
		width: styles.width,
		padding: styles.padding,
		borderRadius: styles.borderRadius,
	}),
});

const SubmittButton = ({ variant, color, title, styles }) => {
	const classes = withStyles(styles);
	const { handleSubmit } = useFormikContext();

	return (
		<Button
			className={classes.root}
			variant={variant}
			color={color}
			onClick={_ => { handleSubmit() }}>{title}</Button>
	);
}

export default SubmittButton;