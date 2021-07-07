import React from 'react';
import { makeStyles, Slide, Dialog as MaterialDialog } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
	root: styles => ({
		minHeight: styles.minHeight,
		minWidth: styles.minWidth,
		display: styles.display,
		justifyContent: styles.justifyContent,
		padding: 20,
		paddingTop: 0,
	}),
}));

const Transition = React.forwardRef((props, ref) => {
	return <Slide direction="up" ref={ref} {...props} />;
});

const Dialog = ({ children, open, close, AppBar }) => {

	return (
		<MaterialDialog
			closeAfterTransition
			fullWidth={true}
			maxWidth='md'
			open={open}
			onClose={close}
			TransitionComponent={Transition}
		>
			{AppBar}
			{children}
		</MaterialDialog >
	);
}

export default Dialog;