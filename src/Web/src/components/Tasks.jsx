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
import TaskAddEdit from './TaskAddEdit';
import useApi from "../hooks/useApi";
import tasks from "../api/tasks";
import authStorage from "../auth/storage";

import styles from "../assets/styles/table.module.css";
// import { styles } from '@material-ui/pickers/views/Calendar/Calendar';

const Transition = React.forwardRef((props, ref) => {
	return <Slide direction="up" ref={ref} {...props} />;
});

const Tasks = ({ history }) => {
	// const classes = useStyles();

	const [role, setRole] = useState([]);
	const { request: getTasks, data: rows, loading } = useApi(tasks.getTasks);
	const { request: updateTask } = useApi(tasks.updateTask);
	const {
		getAccessTokenSilently,
		isAuthenticated,
		getIdTokenClaims
	} = useAuth0();


	useEffect(_ => {
		if (isAuthenticated) {
			(async _ => {
				var token = await getAccessTokenSilently();
				authStorage.storeToken(token);
				getTasks();
				var profile = await getIdTokenClaims();
				setRole(profile['https://schemas.peer-review-tool/roles']);
			})();
		} else {
			authStorage.removeToken();
		}
	}, [isAuthenticated]);

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
		history.push(`tasks/${params.row.uid}`);
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
		history.push(`tasks/${params.data.uid}`);
		// alert(JSON.stringify(args.data.uid));
	}

	const valueAccess = (field, data, column) => {
		return `http://192.168.1.7:3000/${data[field]}`;
	}

	const toolbarOptions = ['ColumnChooser'];
	const filterSettings = { type: "CheckBox" };

	return (
		<>
			<TaskAddEdit open={open} handleClose={handleClose} transition={Transition} data={row} />

			<Typography variant="h4" style={{ display: "flex", justifyContent: "space-between", textAlign: "left", paddingTop: 18, marginLeft: 30 }}>Assignments
			{role.indexOf("Instructor") > -1 &&
					<span style={{ marginRight: 30 }}>
						<Fab color="primary" aria-label="add" onClick={handleClickOpen} >
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

export default Tasks;