import React, { useEffect, useState } from 'react';
import { Grid, Paper, Typography } from "@material-ui/core";
import { isBefore, isAfter, formatDistanceToNow } from "date-fns";

import TimeLine from "./common/TimeLine";
import useApi from "../hooks/useApi";
import tasks from "../api/tasks";
import works from "../api/works";
import Form from "./forms/Form";
import FormField from './forms/FormField';
import SubmittButton from './forms/SubmitButton';

const TaskDetails = ({ match }) => {
	const { request: getTaskByUid, data: task } = useApi(tasks.getTaskByUid);
	const { request: getAuthorWorkByTask, data: work } = useApi(works.getAuthorWorkByTask);

	const events = [
		{ id: "published", date: task?.published, description: "Published" },
		{ id: "start", date: task?.submissionStart, description: "Submission Started" },
		{ id: "deadline", date: task?.submissionEnd, description: "Submission Deadline" },
		{ id: "start", date: task?.reviewStart, description: "Review Started" },
		{ id: "deadline", date: task?.reviewEnd, description: "Review Deadline" },
	];

	useEffect(() => {

		getTaskByUid(match.params.uid);
		getAuthorWorkByTask(match.params.uid);

	}, [match.params.uid]);

	console.log(work);
	return (
		<Grid container spacing={5} style={{ marginTop: "18px" }}>
			<Grid item sm={12} md={5}  >
				<Paper variant="elevation" style={{ height: "100%", width: "100%" }}>
					{task && <TimeLine events={events} />}
				</Paper>
			</Grid>
			<Grid item sm={12} md={7} zeroMinWidth >
				<Paper
					variant="elevation"
					style={{ height: "100%", width: "100", overflow: "auto", maxHeight: 450 }}>
					<div style={{ padding: 20 }}>
						<Typography variant="h3">{task?.name}</Typography>
						<Typography variabt="body">{task?.description}</Typography>
						<Typography variabt="h6">Evaluation Criteria</Typography>
						{task?.criteria?.map(c => (
							<>
								<Typography variant="h6">Description {c.description} MaxPoint {c.maxPoints}</Typography>
							</>
						))}
						<Typography>
							orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
							Why do we use it?

							It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).

							Where does it come from?

							Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32.

							The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum et Malorum" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.

						</Typography>
					</div>
				</Paper>
			</Grid>
			<Grid item sm={12}>
				<Paper variant="elevation"
					style={{ height: "100%", width: "100", overflow: "auto", maxHeight: 450 }}
				>
					<div style={{ padding: 20 }}>
						{isBefore(new Date(work?.submissionStart), new Date()) && isAfter(new Date(work?.submissionEnd), new Date()) &&
							<>
								<Typography variant="h4">
									Your work goes here
						</Typography>
								< Form >
									<FormField
										name="content"
										label="Content"
									/>
									<SubmittButton
										variant="contained"
										color="primary"
										title="Save"
									/>
								</Form>
							</>
						}
						{isBefore(new Date(work?.submissionEnd), new Date()) &&
							<>
								<Typography variant="h4">Your submitted work</Typography>
								<Typography variant="body">
									{work?.content}
								</Typography>
							</>
						}
						{isAfter(new Date(work?.submissionStart), new Date()) &&
							<Typography variant="h3">
								You can submit {formatDistanceToNow(new Date(work?.submissionStart), { addSuffix: true })}
							</Typography>
						}
					</div>
				</Paper>
			</Grid>
		</Grid >
	);
}

export default TaskDetails;