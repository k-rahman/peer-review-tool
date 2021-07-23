import React from "react";
import { useFormikContext, FieldArray } from "formik";
import { Button, IconButton } from "@material-ui/core";
import { Close as CloseIcon } from "@material-ui/icons";
import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
	main: {
		display: "flex",
		// marginTop: 10,
	},
	removeBtn: {
		alignItems: "flex-start",
		marginLeft: 8,
	},
	addBtn: {
		display: "flex",
		justifyContent: "flex-end",
		flexGrow: 1,
		padding: [[14, 0]],
		textAlign: "right"
	}
})

const FormFieldArray = ({ children, name, value, addButtonText }) => {
	const classes = useStyles();
	const { values } = useFormikContext();

	return (
		<FieldArray name={name}>
			{({ remove, push }) => (
				<>
					{values[name].map((_, index) => (
						<div key={index} className={classes.main}>
							{children(index)}
							<IconButton className={classes.removeBtn} onClick={() => remove(index)} disabled={index <= 0}>
								<CloseIcon color={index <= 0 ? "disabled" : "secondary"} />
							</IconButton>
						</div>
					))}

					<div className={classes.addBtn}>
						<Button
							variant="contained" color="primary"
							onClick={() => push(value)}
						>
							{addButtonText}
						</Button>
					</div>
				</>
			)}
		</FieldArray>
	);
}

export default FormFieldArray;