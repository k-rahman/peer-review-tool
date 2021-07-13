import React from 'react';
import { List, ListItem, ListItemText, Typography, makeStyles } from '@material-ui/core';
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

const useStyles = makeStyles((theme) => ({
	root: {
		padding: [[8, 8]],
	},
	wrapper: {
		padding: [[6, 16]],
	},
	details: {
		padding: [[0, 16]],
	},
	title: {
		fontWeight: [500, "!important"],
		padding: [[6, 0]],
	},
	list: {
		width: "100%",
		paddingTop: 0,
	},
	listItem: {
		paddingLeft: 0,
		paddingRight: 20
	},
	listItemText: {
		display: "flex",
		justifyContent: "space-between",
		width: "100%",
	},
	icon: {
		marginRight: 5,
	},
	maxPoints: {
		textAlign: "right",
	},
}));

const Participants = ({ data: participants }) => {
	const classes = useStyles();
	console.log(participants)

	const handlePageChanged = props => {
		console.log(props)
	}

	// const participantColValueAccessor = (field, data) => {
	// 	if (!data[field] && data[field].length === 0)
	// 		return "User has not provide a name yet.";

	// 	return data[field];
	// };

	const nameColTemplate = props => {
		const { name } = props;

		if (!name && name.length === 0)
			return (
				<Typography color="secondary" variant="caption">User has not provide a name yet</Typography>
			);

		return (
			<div style={{ display: "flex", alignItems: "center", justifyContent: "center" }}>
				<div style={{ flexGrow: 1, flexBasis: "40%" }}></div>
				<div style={{ textAlign: "left", flexGrow: 1, flexBasis: "70%" }}>{name}</div>
			</div>
		);

	}

	const emailColTemplate = props => {
		const { email } = props;
		return (
			<div style={{ display: "flex", alignItems: "center", justifyContent: "center" }}>
				<div style={{ flexGrow: 1, flexBasis: "40%" }}></div>
				<div style={{ textAlign: "left", flexGrow: 1, flexBasis: "100%" }}>{email}</div>
			</div>
		)
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
			{/* <List dense className={classes.list}>
					{participants?.map((p, i) => (
						<ListItem key={i} className={classes.listItem}>
							<ListIcon color="primary" className={classes.icon} />
							<div className={classes.listItemText}>
								<ListItemText>{p}</ListItemText>
							</div>
						</ListItem>
					))}
				</List> */}
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
					<ColumnDirective field="name" headerText='Name' headerTextAlign="center" textAlign="center" headerTemplate={nameHeaderTemplate} template={nameColTemplate} />
					<ColumnDirective field="email" headerText='Email' headerTextAlign="center" textAlign="center" headerTemplate={emailHeaderTemplate} template={emailColTemplate} />
				</ColumnsDirective>
				<Inject services={[Filter, Resize, Page, Sort]} />
			</GridComponent>
		</div >
	);
}

export default Participants;