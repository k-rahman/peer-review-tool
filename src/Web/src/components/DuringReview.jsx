import React from 'react';
import _ from "lodash";
import { differenceInMinutes } from 'date-fns';
import { Typography } from "@material-ui/core";

import HorizontalTabs from './common/HorizontalTabs';
import TabPanel from "./common/TabPanel";
import ReviewAddEdit from "./ReviewAddEdit";
import Wait from "./common/Wait";

import animationData from "../assets/lottie-files/waiting.json";

const DuringReview = ({
	handleTabChange,
	handleFormValueChanged,
	handleDiscard,
	tabChanged,
	refreshForm,
	data: reviews,
	tabValue,
	startDate,
}) => {

	const { selfReview, peerReviews } = reviews;

	if (!selfReview && (differenceInMinutes(startDate, new Date()) <= 0 && differenceInMinutes(startDate, new Date()) >= -2)) { // no reviews after 2 mins means student has no submission 
		return <Wait
			label="Please wait, reviews are being distributed"
			animationData={animationData}
			height={100}
			width={100}
		/>
	}

	return (
		<>
			{!selfReview ?
				<Typography variant="body1">
					You have not submitted your work! please contact your instructor, thank you.
				</Typography>
				:
				<>
					<HorizontalTabs
						handleChange={handleTabChange}
						value={tabValue}
						tabs={
							_.fill(_.range(0, peerReviews?.length + 1), "Peer review") // the names of the horizontal tabs
								.map((r, i) => i === 0 ? "Self Review" : `Peer Review ${i}`)
						}
					/>

					<TabPanel value={tabValue} index={0}>
						<ReviewAddEdit
							data={selfReview}
							checkFormChanged={tabChanged}
							checkChanges={handleFormValueChanged}
							handleDiscard={handleDiscard}
							refreshForm={refreshForm}
						/>
					</TabPanel>

					{peerReviews?.map((peerReview, i) =>
						<TabPanel key={i} value={tabValue} index={i + 1}>
							<ReviewAddEdit
								data={peerReview}
								checkFormChanged={tabChanged}
								checkChanges={handleFormValueChanged}
								handleDiscard={handleDiscard}
								refreshForm={refreshForm}
							/>
						</TabPanel>
					)}
				</>
			}
		</>
	);
}

export default DuringReview;