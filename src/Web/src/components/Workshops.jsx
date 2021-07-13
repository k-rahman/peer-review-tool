import React, { useEffect, useState, useRef } from 'react';
import { Typography, Paper, makeStyles } from '@material-ui/core';
import { useAuth0 } from "@auth0/auth0-react";

import useApi from "../hooks/useApi";
import workshopService from "../api/workshops";
import WorkshopAddEdit from './WorkshopAddEdit';
import WorkshopsTable from './WorkshopsTable';
import WorkshopsHeader from './WorkshopsHeader';
import SuccessDialog from './common/SuccessDialog';

const useStyles = makeStyles({
	root: {
		minHeight: "100vh",
		padding: 18,
	},
	link: {
		flexBasis: "100%",
		textAlign: "center"
	}
});

const Workshops = _ => {
	const classes = useStyles();

	const workshopsGrid = useRef();
	const [role, setRole] = useState([]);
	const [tabValue, setTabValue] = useState(0);
	const [workshopLink, setWorkshopLink] = useState("");
	const { user, getIdTokenClaims } = useAuth0();
	const { request: getWorkshops, data: workshops } = useApi(workshopService.getWorkshops);
	const { error: addError, request: addWorkshop } = useApi(workshopService.addWorkshop);
	const { request: updateWorkshop } = useApi(workshopService.updateWorkshop);

	const [openAdd, setOpenAdd] = React.useState(false);
	const [openSuccess, setOpenSuccess] = React.useState(false);
	const [row, setRow] = React.useState(null);

	useEffect(_ => {
		setRole(user['https://schemas.peer-review-tool/roles']);
		getWorkshops();
	}, [user]);

	console.log(user, getIdTokenClaims())

	const handleDialogOpen = () => {
		setOpenAdd(true);
		setTabValue(0);
		setRow(null)
	};

	const handleClose = () => {
		setOpenAdd(false);
		setOpenSuccess(false);
	};

	const handleWorkshopEdit = params => {
		setOpenAdd(true);
		setRow(params);
		// alert(JSON.stringify(params));
	}

	const handleSubmit = async (values, { setSubmitting }) => {
		const { data: newWorkshop } = await addWorkshop(values);
		refreshWorkshopsTable(newWorkshop);
		setSubmitting(false);
		setOpenAdd(false);
		setOpenSuccess(true);
		setWorkshopLink(`http://${window.location.host}/${newWorkshop.uid}`);
	};

	const refreshWorkshopsTable = workshop => {
		workshopsGrid.current.dataSource.unshift(workshop);
		workshopsGrid.current.refresh();
	}

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
	};

	return (
		<Paper variant="outlined" className={classes.root}>
			<WorkshopAddEdit
				close={handleClose}
				data={row}
				handleTabChange={handleTabChange}
				handleSubmit={handleSubmit}
				open={openAdd}
				tabValue={tabValue}
			/>
			<WorkshopsHeader
				handleAddClick={handleDialogOpen}
				isInstructor={role.indexOf("Instructor") !== -1}
			/>
			<WorkshopsTable
				data={workshops}
				onWorkshopEdit={handleWorkshopEdit}
				gridRef={workshopsGrid}
				isInstructor={role.indexOf("Instructor") !== -1}
			/>
			<SuccessDialog
				open={openSuccess}
				close={handleClose}
				label="Your Workshop Is Ready"
			>
				<div className={classes.link}>
					<Typography variant="subtitle1" component="h2" >
						Here is the workshop link to share with your participants <br />
					</Typography>
					<Typography variant="h6" >
						{workshopLink}
					</Typography>
				</div>
			</SuccessDialog>
		</Paper >
	);
}

export default Workshops;