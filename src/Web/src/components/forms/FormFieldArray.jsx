import React, { useEffect } from "react";
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
						<div key={index} style={{ display: "flex", justifyContent: "space-between" }}>
							{children(index)}
							{index <= 0 ?
								<div style={{ marginRight: 50 }}></div>// show remove button starting from the second field
								:
								<IconButton
									onClick={() => remove(index)}
									style={{ paddingTop: 56, paddingRight: 12 }}
								>
									<Close color="secondary" />
								</IconButton>
							}
						</div>
					))}
					<Button
						variant="contained" color="primary"
						onClick={() => push(value)}
						style={{ margin: 30 }}
					>
						Add Criterion
							</Button>
				</div>
			)}
		</FieldArray>
	);
}

export default FormFieldArray;