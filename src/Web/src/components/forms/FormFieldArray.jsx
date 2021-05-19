import React from "react";
import { useFormikContext, FieldArray } from "formik";
import { Button, IconButton } from "@material-ui/core";
import { Close } from "@material-ui/icons";

const FormFieldArray = ({ children, name, value }) => {

	const { values } = useFormikContext();

	return (
		<FieldArray name={name}>
			{({ remove, push }) => (
				<div>
					{values[name].map((_, index) => (
						<div key={index}>
							{children(index)}
							{index > 0 && // show close button starting from the second field
								<IconButton
									onClick={() => remove(index)}
								>
									<Close color="secondary" />
								</IconButton>
							}
						</div>
					))}
					<Button
						variant="contained" color="primary"
						onClick={() => push(value)}
					>
						Add Criterion
							</Button>
				</div>
			)}
		</FieldArray>
	);
}

export default FormFieldArray;