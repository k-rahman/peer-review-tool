import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { isBefore } from "date-fns";
import { Button, makeStyles } from "@material-ui/core";
import { Edit as EditIcon } from "@material-ui/icons";
import {
	GridComponent,
	ColumnDirective,
	ColumnsDirective,
	ColumnMenu,
	Page,
	Filter,
	Reorder,
	Resize,
	Sort,
	Inject
} from "@syncfusion/ej2-react-grids";

import "../assets/styles/workshops-table.css";

const useStyles = makeStyles({
	wrapper: {
		padding: 8,
	},
	editColumn: {
		padding: [0, '!important'],
		borderRight: [[1, "solid", "#e0e0e0"], "!important"]
	},
	editBtn: {
		minWidth: "100%",
		minHeight: "100%",
	},
	nameBtn: {
		width: "100%",
		height: "100%",
		textTransform: "none",
	}
})

const WorkshopsTable = ({ data: workshops, onWorkshopEdit, isInstructor, gridRef }) => {
	const classes = useStyles();
	const history = useHistory();

	useEffect(() => {
		if (isInstructor) {
			gridRef?.current.showColumns([' ', 'Link']);
			gridRef?.current.hideColumns(['Instructor', 'Submission Start', 'Review Start']);
		}
	}, [isInstructor, gridRef]);

	const handleWorkshopClicked = props => {
		history.push(`/workshops/${props.uid}`);
	}

	const editColTemplate = props => {
		// const { published } = props;
		return (
			<Button
				className={classes.editBtn}
				size="small"
				color="secondary"
				// disabled={isBefore(new Date(published), new Date())}
				disabled={true} // to be decided
				onClick={() => onWorkshopEdit(props)}>
				<EditIcon />
			</Button>
		);
	}

	const nameColTemplate = props => {
		return (
			<Button
				color="primary"
				className={classes.nameBtn}
				onClick={() => handleWorkshopClicked(props)}>
				{props.name}
			</Button>
		);
	}

	const linkValue = (field, data, column) => {
		return `http://${window.location.host}/${data[field]}`;
	}

	const filterOptions = { type: "CheckBox" };
	const pageOptions = { pageSizes: ["5", "10", "20"], pageSize: "10" };

	return (

		<div className={classes.wrapper}>
			<GridComponent
				allowFiltering={true}
				allowPaging={true}
				allowReordering={true}
				allowResizing={true}
				allowSorting={true}
				allowTextWrap={true}
				dataSource={workshops}
				filterSettings={filterOptions}
				pageSettings={pageOptions}
				ref={gridRef}
				showColumnMenu={true}
			>

				<ColumnsDirective>
					<ColumnDirective headerText=' ' template={editColTemplate} textAlign="center" customAttributes={{ class: classes.editColumn }} allowReordering={false} allowFiltering={false} allowResizing={false} showInColumnChooser={false} visible={false} minWidth="30" width="30" />
					<ColumnDirective field="name" headerText='Name' template={nameColTemplate} headerTextAlign="center" showInColumnChooser={false} minWidth="350" width="350" />
					<ColumnDirective field='published' headerText='Publish Date' type='dateTime' format='dd.MM.yyyy hh:mm a' headerTextAlign='center' textAlign='center' minWidth="170" width="170" />
					<ColumnDirective field='uid' headerText='Link' valueAccessor={linkValue} headerTextAlign="center" textAlign="center" showInColumnChooser={isInstructor} visible={false} minWidth="450" width="450" />
					<ColumnDirective field='submissionStart' headerText='Submission Start' type='dateTime' format='dd.MM.yyyy hh:mm a' headerTextAlign='center' textAlign='center' minWidth="175" width="175" />
					<ColumnDirective field='submissionEnd' headerText='Submission Deadline' type='dateTime' format='dd.MM.yyyy hh:mm a' headerTextAlign='center' textAlign='center' visible={false} minWidth="170" width="175" />
					<ColumnDirective field='reviewStart' headerText='Review Start' type='dateTime' format='dd.MM.yyyy hh:mm a' headerTextAlign='center' textAlign='center' minWidth="170" width="170" />
					<ColumnDirective field='reviewEnd' headerText='Review Deadline' type='dateTime' format='dd.MM.yyyy hh:mm a' headerTextAlign='center' textAlign='center' visible={false} minWidth="170" width="170" />
					<ColumnDirective field='instructor' headerText='Instructor' headerTextAlign="center" textAlign="center" showInColumnChooser={!isInstructor} />
				</ColumnsDirective>

				<Inject services={[ColumnMenu, Filter, Reorder, Resize, Page, Sort]} />
			</GridComponent>
		</div>
	);
};

export default WorkshopsTable;