import React, { useEffect, useState } from 'react';
import { IconButton, Typography, Fab } from "@material-ui/core";
import { Add, Edit } from "@material-ui/icons";
import Slide from '@material-ui/core/Slide';
import {
	GridComponent,
	ColumnDirective,
	ColumnsDirective,
	ColumnChooser,
	ColumnMenu,
	Toolbar,
	Page,
	Filter,
	Reorder,
	Resize,
	Sort,
	Inject
} from "@syncfusion/ej2-react-grids";
import { useAuth0 } from "@auth0/auth0-react";
// import { useStyles } from "../assets/styles/table";
import WorkshopAddEdit from './WorkshopAddEdit';
import useApi from "../hooks/useApi";
import workshops from "../api/workshops";

import styles from "../assets/styles/table.module.css";
// import { styles } from '@material-ui/pickers/views/Calendar/Calendar';

const Transition = React.forwardRef((props, ref) => {
	return <Slide direction="up" ref={ref} {...props} />;
});

const Workshops = ({ history }) => {
	// const classes = useStyles();

	const [role, setRole] = useState([]);
	const { request: getWorkshops, data: rows, loading } = useApi(workshops.getWorkshops);
	const { request: updateWorkshop } = useApi(workshops.updateWorkshop);
	const {
		getIdTokenClaims,
		user
	} = useAuth0();


	useEffect(_ => {
		(async _ => {
			getWorkshops();
			var profile = await getIdTokenClaims();
			setRole(profile['https://schemas.peer-review-tool/roles']);
		})();
	}, []);

	const [open, setOpen] = React.useState(false);
	const [row, setRow] = React.useState(null);

	const handleClickOpen = () => {
		setOpen(true);
		setRow(null)
	};

	const handleClose = () => {
		setOpen(false);
	};


	const handleRowClick = params => {
		history.push(`workshops/${params.row.uid}`);
	}

	const handleRowEdit = params => {
		setOpen(true);
		setRow(params);
		// alert(JSON.stringify(params));
	}

	const EditButton = params => {
		return (
			<IconButton size="small" color="primary" onClick={() => handleRowEdit(params)}>
				<Edit />
			</IconButton>
		);
	}

	const rowSelected = params => {
		history.push(`workshops/${params.data.uid}`);
		// alert(JSON.stringify(args.data.uid));
	}

	const valueAccess = (field, data, column) => {
		return `http://192.168.1.7:3000/${data[field]}`;
	}

	const toolbarOptions = ['ColumnChooser'];
	const filterSettings = { type: "CheckBox" };

	return (
		<>
			<WorkshopAddEdit open={open} handleClose={handleClose} transition={Transition} data={row} />

			<Typography variant="h4" style={{ display: "flex", justifyContent: "space-between", textAlign: "left", paddingTop: 18, marginLeft: 30 }}>Assignments
			{role.indexOf("Instructor") > -1 &&
					<span style={{ marginRight: 30 }}>
						<Fab className={{ backgroundColor: "#ff8f00" }} aria-label="add" onClick={handleClickOpen} >
							<Add />
						</Fab>
					</span>
				}
			</Typography>
			{/* <TasksTable rows={rows} columns={instructorColumns} loading={loading} onRowClick={handleRowClick} /> */}
			<div className={styles.table}>
				<GridComponent
					dataSource={rows}
					allowFiltering={true}
					allowPaging={true}
					allowReordering={true}
					allowResizing={true}
					allowSorting={true}
					allowTextWrap={true}
					filterSettings={filterSettings}
					pageSettings={{ pageSize: 10 }}
					rowSelected={rowSelected}
					showColumnChooser={true}
					showColumnMenu={true}
					toolbar={toolbarOptions}
				>
					<ColumnsDirective>
						<ColumnDirective template={EditButton} textAlign="Center" width="40" allowReordering={false} allowFiltering={false} allowResizing={false} showInColumnChooser={false} />
						<ColumnDirective field='name' headerText='Name' />
						<ColumnDirective field='published' headerText='Published' type='dateTime' format='dd.MM.yyyy HH:mm' width="150" headerTextAlign='Center' textAlign='Center' />
						<ColumnDirective field='uid' headerText='Link' valueAccessor={valueAccess} />
						<ColumnDirective field='submissionStart' headerText='Submission Start' type='dateTime' format='dd.MM.yyyy HH:mm' width="150" headerTextAlign='Center' textAlign='Center' visible={false} />
						<ColumnDirective field='submissionEnd' headerText='Submission Deadline' type='dateTime' format='dd.MM.yyyy HH:mm' width="150" headerTextAlign='Center' textAlign='Center' visible={false} />
						<ColumnDirective field='reviewStart' headerText='Review Start' type='dateTime' format='dd.MM.yyyy HH:mm' width="150" headerTextAlign='Center' textAlign='Center' visible={false} />
						<ColumnDirective field='reviewEnd' headerText='Review Deadline' type='dateTime' format='dd.MM.yyyy HH:mm' width="150" headerTextAlign='Center' textAlign='Center' visible={false} />
						<ColumnDirective field='instructorId' headerText='Instructor' />
					</ColumnsDirective>
					<Inject services={[ColumnChooser, ColumnMenu, Filter, Reorder, Resize, Page, Sort, Toolbar]} />
				</GridComponent>
			</div>
		</>
	);
}

export default Workshops;