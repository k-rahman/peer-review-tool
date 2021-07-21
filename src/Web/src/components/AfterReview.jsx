import React, { useEffect, useState } from 'react';
import _ from "lodash";
import { Typography } from "@material-ui/core";
import ReviewView from './ReviewView';

import HorizontalTabs from './common/HorizontalTabs';
import TabPanel from './common/TabPanel';

const AfterReview = ({ data: reviewsSummary }) => {
	const { selfReview, peerReviews, instructorReview } = reviewsSummary;

	const [indexValue, setIndexValue] = useState(0);
	const [tabValue, setTabValue] = useState(0);
	const [tabsNames, setTabsNames] = useState([]);

	useEffect(() => {
		// increment index value by one for each existing review so the last review (instructor review) will be the indexValue
		let lastTabPanelIndex = 0;
		if (!checkForNoReview(selfReview)) lastTabPanelIndex = 1;
		peerReviews?.forEach(p => {
			if (!checkForNoReview(p)) lastTabPanelIndex += 1
		});

		setIndexValue(lastTabPanelIndex);
	}, [selfReview]);

	useEffect(() => { // create tab names, according to existing reviews 
		if (peerReviews !== null && peerReviews !== undefined) {
			const tabs = _.without(["Self Review", ...peerReviews, "Instructor Review"].map((t, i, arr) => {
				switch (i) {
					case 0:
						return checkForNoReview(selfReview) ? null : "Self Review";
					case arr.length - 1:
						return checkForNoReview(instructorReview) ? null : "Instructor Review";
					default:
						return checkForNoReview(t) ? null : `Received Review ${i}`;
				}
			}), null);

			setTabsNames(tabs);
		}
	}, [selfReview]);

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
	};

	const checkForNoReview = review => review?.grades.every(g => g.points === null); // return false if none of the reviews grades have points (not even 0)

	return (
		<>
			{!selfReview ? // in the case that student didn't submit any thing
				<Typography variant="body1">
					You have not submitted your work! please contact your instructor, thank you.
				</Typography>
				:
				<>
					{tabsNames.length === 0 ? // if the student submitted work but didn't review self/peer, there must be a teacher review on the work
						<Typography variant="body1">
							You have do not have neither self-review nor peer reviews. Once you are reviewed by your instructor, review will show here.
						</Typography>
						:
						<>
							<HorizontalTabs
								handleChange={handleTabChange}
								value={tabValue}
								tabs={tabsNames}
							/>

							{checkForNoReview(selfReview) ? null :
								< TabPanel value={tabValue} index={0} >
									<ReviewView data={selfReview} />
								</TabPanel>
							}

							{peerReviews?.map((peerReview, i) =>
								checkForNoReview(peerReview) ?
									null
									:
									< TabPanel key={i} value={tabValue} index={checkForNoReview(selfReview) ? i : i + 1}>
										<ReviewView data={peerReview} />
									</TabPanel>
							)}

							{checkForNoReview(instructorReview) ? null :
								<TabPanel value={tabValue} index={indexValue}>
									<ReviewView data={instructorReview} />
								</TabPanel>
							}
						</>
					}
				</>
			}
		</>
	);
}

export default AfterReview;