import React from 'react';
import _ from "lodash";
import { Typography } from "@material-ui/core";

import HorizontalTabs from './common/HorizontalTabs';
import TabPanel from "./common/TabPanel";
import ReviewAddEdit from "./ReviewAddEdit";

const DuringReview = ({
	handleTabChange,
	handleFormValueChanged,
	handleDiscard,
	tabChanged,
	refreshForm,
	data: reviews,
	tabValue,
}) => {

	const { selfReview, peerReviews } = reviews;

	return (
		<>
			{!selfReview ?
				<Typography variant="body1">
					You have not submitted your work! please contact your teacher, thank you.
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