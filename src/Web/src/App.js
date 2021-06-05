import React, { useEffect } from "react";
import { useHistory, Route, Redirect, Switch } from "react-router-dom";
import {
  Container,
  AppBar,
  Toolbar,
  Typography,
  Button,
  makeStyles,
  Breadcrumbs,
} from "@material-ui/core";
import Chip from "@material-ui/core/Chip";
import HomeIcon from "@material-ui/icons/Home";
import { emphasize, withStyles } from "@material-ui/core/styles";
import { useAuth0 } from "@auth0/auth0-react";

import "./App.css";
import Tasks from "./components/Tasks";
import TaskDetails from "./components/TaskDetails";
import NavBar from "./components/NavBar";
import Loading from "./components/Loading";
import ProtectedRoute from "./components/auth/ProtectedRoute";

const StyledBreadcrumb = withStyles(theme => ({
  root: {
    backgroundColor: theme.palette.grey[100],
    height: theme.spacing(3),
    color: theme.palette.grey[800],
    fontWeight: theme.typography.fontWeightRegular,
    "&:hover, &:focus": {
      backgroundColor: theme.palette.grey[300],
    },
    "&:active": {
      boxShadow: theme.shadows[1],
      backgroundColor: emphasize(theme.palette.grey[300], 0.12),
    },
  },
}))(Chip);

const App = _ => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    return <Loading />;
  }

  return (
    <Container maxWidth="lg" style={{ height: "100%" }}>
      <NavBar />
      <Breadcrumbs
        aria-label="breadcrumb"
        style={{ marginTop: 18, marginLeft: 30 }}
      >
        <StyledBreadcrumb
          component="a"
          href="#"
          label="Home"
          icon={<HomeIcon fontSize="small" />}
        />
        {/* <StyledBreadcrumb component="a" href="#" label="Catalog" />
          <StyledBreadcrumb
            label="Accessories"
            deleteIcon={<ExpandMoreIcon />}
          /> */}
      </Breadcrumbs>
      <div className="App">
        {/* <TaskAddEdit /> */}
        <Switch>
          <ProtectedRoute path="/tasks/:uid" component={TaskDetails} />
          <ProtectedRoute path="/tasks" component={Tasks} />
          {/* <ProtectedRoute path="/" component={Home} /> */}
        </Switch>
      </div>
    </Container>
  );
};

export default App;
