import React, { useEffect, useState, useRef } from 'react';
import { Typography, Paper, makeStyles } from '@material-ui/core';
import { useAuth0 } from "@auth0/auth0-react";

import useApi from "../hooks/useApi";
import workshopService from "../api/workshops";
import WorkshopAddEdit from './WorkshopAddEdit';
import WorkshopsTable from './WorkshopsTable';
import WorkshopsHeader from './WorkshopsHeader';
import SuccessDialog from './common/SuccessDialog';
import Loading from "./Loading";

const useStyles = makeStyles({
	container: {
		minHeight: "calc(100vh - 64px - 48px - 61px - 18px)",
		height: "100%",
	},
	root: {
		minHeight: "calc(100vh - 64px - 48px - 61px - 18px)",
		height: "100%",
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
	const { user } = useAuth0();
	const { request: getWorkshops, data: workshops, error: workshopsError, response: workshopsResponse } = useApi(workshopService.getWorkshops);
	const { request: addWorkshop, loading: addWorkshopLoading } = useApi(workshopService.addWorkshop);
	// const { request: updateWorkshop } = useApi(workshopService.updateWorkshop);

	const [openAdd, setOpenAdd] = React.useState(false);
	const [openSuccess, setOpenSuccess] = React.useState(false);
	const [row, setRow] = React.useState(null);

	useEffect(_ => {
		setRole(user['https://schemas.peer-review-tool/roles']);
	}, [user]);

	useEffect(_ => {
		getWorkshops();
	}, []);

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
		const { data: newWorkshop, ok } = await addWorkshop({ ...values, instructor: user.name });
		setSubmitting(false);

		if (ok) {
			setOpenAdd(false);
			setOpenSuccess(true);
			setWorkshopLink(`${window.location}/${newWorkshop.uid}`);
			refreshWorkshopsTable(newWorkshop);
		}
	};

	const refreshWorkshopsTable = workshop => {
		workshopsGrid.current.dataSource.unshift(workshop);
		workshopsGrid.current.refresh();
	}

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
	};

	if (addWorkshopLoading)
		return <Loading />

	return (
		<div className={classes.container}>
			<Paper variant="outlined" className={classes.root}>
				{workshopsError && !workshopsResponse.status ?
					<Typography variant="subtitle2" color="secondary" align="center" component="div">
						Workshop service is down at the moment.
					</Typography>
					:
					<>
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
					</>
				}
			</Paper >
		</div>
	);
}

export default Workshops;