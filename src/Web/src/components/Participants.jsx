import React from 'react';
import { makeStyles } from '@material-ui/core';
import { EmailOutlined as EmailIcon } from "@material-ui/icons";
import { CardAccountDetailsOutline as NameIcon } from "mdi-material-ui";
import {
	GridComponent,
	ColumnDirective,
	ColumnsDirective,
	Page,
	Filter,
	Sort,
	Resize,
	Inject
} from "@syncfusion/ej2-react-grids";

import "../assets/styles/pager.css";

const useStyles = makeStyles({
	root: {
		padding: [[8, 8]],
	},
});

const Participants = ({ data: participants }) => {
	const classes = useStyles();

	const nameValueAccessor = (field, data) => {
		if (!data[field] || data[field].length === 0)
			return "(no name)";
		return data[field];
	}

	const nameHeaderTemplate = _ => {
		return (
			<div style={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
				<NameIcon color="primary" style={{ marginRight: "10px" }} />
				Name
			</div>
		);
	}

	const emailHeaderTemplate = _ => {
		return (
			<div style={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
				<EmailIcon color="primary" style={{ marginRight: "10px" }} />
				Email
			</div>
		)
	}

	const filterSettings = { type: "CheckBox" };
	const pageOptions = { pageSizes: ["10", "20", "30", "40"], pageSize: "20" };

	return (
		<div className={classes.root}>
			<GridComponent
				dataSource={participants}
				allowFiltering={true}
				allowPaging={true}
				allowSorting={true}
				allowResizing={true}
				filterSettings={filterSettings}
				pageSettings={pageOptions}
			>
				<ColumnsDirective>
					<ColumnDirective field="name" headerTextAlign="center" textAlign="center" headerTemplate={nameHeaderTemplate} valueAccessor={nameValueAccessor} />
					<ColumnDirective field="email" headerTextAlign="center" textAlign="center" headerTemplate={emailHeaderTemplate} />
				</ColumnsDirective>
				<Inject services={[Filter, Resize, Page, Sort]} />
			</GridComponent>
		</div >
	);
}

export default Participants;