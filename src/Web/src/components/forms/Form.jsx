import React from "react";
import { Formik } from "formik";

const Form = ({
	children,
	initialValues,
	onSubmit,
	validationSchema,
}) => {
	return (
		<Formik
			initialValues={initialValues}
			onSubmit={onSubmit}
			validateOnChange={false}
			validationSchema={validationSchema}
		>
			{() => (
				<>
					{children}
				</>
			)}
		</Formik>
	);
};

export default Form;
