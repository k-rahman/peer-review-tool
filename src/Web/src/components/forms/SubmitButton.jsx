import React from "react";
import { useFormikContext } from "formik";
import { Button } from "@material-ui/core";

const SubmittButton = ({ variant, color, title }) => {
	const { handleSubmit } = useFormikContext();

	return (
		<Button
			variant={variant}
			color={color}
			onClick={handleSubmit}>{title}</Button>
	);
}

export default SubmittButton;