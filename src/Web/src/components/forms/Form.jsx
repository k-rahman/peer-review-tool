import React from "react";
import _ from "lodash";
import { Formik } from "formik";

const Form = ({
	children,
	initialValues,
	onSubmit,
	validationSchema,
	enableReinitialize = false,
	checkChanges = () => { },
}) => {

	return (
		<Formik
			initialValues={initialValues}
			onSubmit={onSubmit}
			validateOnChange={true}
			validateOnBlur={true}
			validationSchema={validationSchema}
			enableReinitialize={enableReinitialize} // when initialvalues change reinsialize with new initial values
		>
			{({ handleSubmit, initialValues, values, }) => (
				<>
					{children}
					{checkChanges(!_.isEqual(initialValues, values))}
				</>
			)}
		</Formik>
	);
};

export default Form;
