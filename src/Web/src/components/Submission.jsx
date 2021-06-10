import React from 'react';
import { isBefore, isAfter } from "date-fns";

import BeforeSubmission from './BeforeSubmission';
import DuringSubmission from './DuringSubmission';
import AfterSubmission from './AfterSubmission';

const Submission = ({ startDate, endDate }) => {

	const submissionComponent = _ => {

		// before submission start
		if (isAfter(startDate, new Date()))
			return <BeforeSubmission startDate={startDate} />

		// submission started
		if (isBefore(startDate, new Date()) && isAfter(endDate, new Date()))
			return <DuringSubmission />

		// submission ended
		if (isBefore(endDate, new Date()))
			return <AfterSubmission />
	}

	return (
		<div style={{ padding: 20 }}>
			{submissionComponent()}
		</div>
	);
}

export default Submission;