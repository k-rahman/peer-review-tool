import React from 'react';
import { Slide, Dialog as MaterialDialog } from "@material-ui/core";

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