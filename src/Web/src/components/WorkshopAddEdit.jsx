import React from "react";
import * as Yup from "yup";
import _ from "lodash";
import { add } from "date-fns";
import { AppBar, Slide, Dialog, Toolbar, IconButton, Typography, Divider, makeStyles } from "@material-ui/core";
import { Close as CloseIcon } from '@material-ui/icons';

import Form from "./forms/Form";
import FormField from "./forms/FormField";
import FormSelect from "./forms/FormSelect";
import FormFieldArray from "./forms/FormFieldArray";
import SubmitButton from "./forms/SubmitButton";
import FormDatePicker from "./forms/FormDatePicker";
import FormUpload from "./forms/FormUpload";
import VerticalTabs from "./common/VerticalTabs";
import TabPanel from "./common/TabPanel";

const useStyles = makeStyles((theme) => ({
	main: {
		display: "flex",
		minHeight: 490
	},
	appBar: {
		position: 'relative',
	},
	dialogTitle: {
		marginLeft: theme.spacing(2),
		flex: 1,
	},
	divider: {
		margin: [[6, 0]]
	},
	wrapper: {
		width: "100%",
		padding: 8,
	},
	title: {
		width: "100%",
		fontWeight: [500, "!important"],
		marginBottom: 12,
	},
	subtitle: {
		width: "100%",
		padding: [[6, 0]],
	},
	content: {
		padding: [[6, 16]],
	},
	contentWrapper: {
		display: "flex",
		flexGrow: 1,
		marginBottom: 12
	},
	details: {
		padding: [[0, 16]],
	},
	list: {
		width: "100%",
		paddingTop: 0,
	},
	listItem: {
		padding: 0,
		flexWrap: "wrap",
	},
	listItemText: {
		marginTop: 0,
	},
	criteriaWrapper: {
		display: "flex",
		width: "100%",
	},
	maxPointsWrapper: {
		display: "flex",
		flexDirection: "column",
		justifyContent: "center",
	},
	icon: {
		marginRight: 5,
	},
	uploadBtn: {
		width: "100%",
		padding: [[6, 16]],
		textAlign: "right"
	},
}));


const Transition = React.forwardRef((props, ref) => {
	return <Slide direction="up" ref={ref} {...props} />;
});

const WorkshopAddEdit = ({ open, close, data, tabValue, handleTabChange, handleValidationError, handleSubmit }) => {
	const classes = useStyles();

	// form validation 
	const validationSchema = Yup.object({
		name: Yup.string().required("Required*").min(5, "Must be at least 5 characters"),
		description: Yup.string().required("Required*").min(5, "Must be at least 5 characters"),
		published: Yup.date().typeError("Invalide date").min(add(new Date(), { minutes: 5 }), "Publish date must be at least 5 mins from now").required("Required*").nullable(),
		criteria: Yup.array().min(1).of(
			Yup.object({
				description: Yup.string().required("Required*"),
				maxPoints: Yup.number().required("Required*")
			})
		),
		participants: Yup.mixed().required("Required*").nullable(),
		submissionStart: Yup.date().typeError("Invalide date").min(Yup.ref('published'), "Submission start date can not be before publish date!").required("Required*").nullable(),
		submissionEnd: Yup.date().typeError("Invalide date").min(Yup.ref('submissionStart'), "Submission end date can not be before submission start date!").required("Required*").nullable(),
		reviewStart: Yup.date().typeError("Invalide date").min(Yup.ref('submissionEnd'), "Review start date can not be before submission end date!").required("Required*").nullable(),
		reviewEnd: Yup.date().typeError("Invalide date").min(Yup.ref('reviewStart'), "Review end date can not be before review start date!").required("Required*").nullable(),
		numberOfReviews: Yup.number().required("Required*"),
	});

	// form initialvalues
	const initialValues = {
		name: "",
		description: "",
		published: add(new Date(), { minutes: 10 }),
		criteria: [
			{
				description: "",
				maxPoints: 0
			}
		],
		participants: null,
		submissionStart: add(new Date(), { minutes: 20 }),
		submissionEnd: add(new Date(), { minutes: 30 }),
		reviewStart: add(new Date(), { minutes: 40 }),
		reviewEnd: add(new Date(), { minutes: 50 }),
		numberOfReviews: 1,
	}

	return (
		<div >
			<Dialog
				closeAfterTransition
				fullWidth={true}
				maxWidth='md'
				open={open}
				onClose={close}
				TransitionComponent={Transition}
			>
				<Form
					initialValues={data ? data : initialValues}
					onSubmit={handleSubmit}
					validationSchema={validationSchema}
				>

					{/*Header*/}
					<AppBar className={classes.appBar}>
						<Toolbar>
							<IconButton edge="start" color="inherit" onClick={close} aria-label="close">
								<CloseIcon />
							</IconButton>
							<Typography variant="h6" className={classes.dialogTitle}>
								Your new workshop
							</Typography>
							<SubmitButton variant="contained" color="secondary" title="Create" onValidationError={handleValidationError} />
						</Toolbar>
					</AppBar>

					<div className={classes.main}>
						<VerticalTabs
							handleChange={handleTabChange}
							tabs={[
								{ workshop: ["name", "description", "published"] },
								{ participants: ["participants"] },
								{ criteria: ["criteria"] },
								{ submission: ["submissionStart", "submissionEnd"] },
								{ review: ["reviewStart", "reviewEnd", "numberOfReviews"] },
							]}
							value={tabValue}
						/>

						{/*Workshop*/}
						<TabPanel value={tabValue} index={0} >
							<div className={classes.wrapper}>
								<Typography variant="h6" component="h3" className={classes.title}>
									Your workshop needs a name and description
								</Typography>
								<div className={classes.wrapper}>
									<div className={classes.contentWrapper}>
										<FormField name="name" label="Name" />
									</div>
									<div className={classes.contentWrapper}>
										<FormField name="description" label="Description" multiline={true} />
									</div>
								</div>
							</div>
							<Divider variant="middle" className={classes.divider} />
							<div className={classes.wrapper}>
								<div className={classes.title}>
									<Typography variant="h6" component="h3" >
										Choose when participants will see your workshop
									</Typography>
									<Typography variant="body2" >
										Remember to choose date and time
									</Typography>
								</div>
								<div className={classes.wrapper}>
									<div className={classes.contentWrapper}>
										<FormDatePicker name="published" label="Publish On" />
									</div>
								</div>
							</div>
						</TabPanel>

						{/*Participants*/}
						<TabPanel value={tabValue} index={1} >
							<div className={classes.wrapper}>
								<div className={classes.title}>
									<Typography variant="h6" component="h3" >
										Select a CSV file that contains workshop participants' emails
									</Typography>
									<Typography variant="body2" >
										CVS file must be in the following format: email, email, email
									</Typography>
								</div>
								<div className={classes.wrapper}>
									<FormUpload
										name="participants"
										title="Select CSV file"
									/>
								</div>
							</div>
						</TabPanel>

						{/*Criteria*/}
						<TabPanel value={tabValue} index={2} >
							<div className={classes.wrapper}>
								<div className={classes.title}>
									<Typography variant="h6" component="h3" >
										Criteria used in evaluating the participant's submission
									</Typography>
									<Typography variant="body2">
										Provide at least 1 criterion
									</Typography>
								</div>
								<div className={classes.wrapper}>
									<FormFieldArray name="criteria" value={{ description: "", maxPoints: 0 }} addButtonText="Add more criteria">
										{(index) => (
											<div className={classes.contentWrapper}>
												<FormField
													name={`criteria.${index}.description`}
													label="Description"
												/>
												<FormSelect
													label={'Max Points'}
													menuItems={_.range(0, 101)}
													name={`criteria.${index}.maxPoints`}
												/>
											</div>
										)}
									</FormFieldArray>
								</div>
							</div>
						</TabPanel>

						{/*Submission*/}
						<TabPanel value={tabValue} index={3} >
							<div className={classes.wrapper}>
								<div className={classes.title}>
									<Typography variant="h6" component="h3">
										Choose when participants are allowed to submit their work
									</Typography>
									<Typography variant="body2" >
										Remember to choose date and time
									</Typography>
								</div>
								<div className={classes.wrapper}>
									<div className={classes.contentWrapper}>
										<FormDatePicker name="submissionStart" label="Submission Start" />
									</div>
								</div>
								<div className={classes.wrapper}>
									<div className={classes.contentWrapper}>
										<FormDatePicker name="submissionEnd" label="Submission End" />
									</div>
								</div>
							</div>
						</TabPanel>

						{/*Review*/}
						<TabPanel value={tabValue} index={4}>
							<div className={classes.wrapper}>
								<Typography variant="h6" component="h3" className={classes.title}>
									Choose how many reviews each participant must carry out
								</Typography>
								<div className={classes.wrapper}>
									<div className={classes.contentWrapper}>
										<FormSelect
											label="Number of reviews"
											menuItems={_.range(1, 6)}
											name="numberOfReviews"
										/>
									</div>
								</div>
								<Divider variant="middle" className={classes.divider} />
								<div className={classes.wrapper}>
									<div className={classes.title}>
										<Typography variant="h6" component="h3">
											Choose when participants can review own works and peers' works
										</Typography>
										<Typography variant="body2">
											Remember to choose date and time
										</Typography>
									</div>
									<div className={classes.wrapper}>
										<div className={classes.contentWrapper}>
											<FormDatePicker name="reviewStart" label="Review Start" />
										</div>
									</div>
									<div className={classes.wrapper}>
										<div className={classes.contentWrapper}>
											<FormDatePicker name="reviewEnd" label="Review End" />
										</div>
									</div>
								</div>
							</div>
						</TabPanel>
					</div>
				</Form>
			</Dialog>
		</div >
	);
}

export default WorkshopAddEdit;