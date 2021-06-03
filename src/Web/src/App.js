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

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));
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
  const history = useHistory();
  const classes = useStyles();
  const { user, loginWithRedirect, isAuthenticated, logout, getIdTokenClaims } =
    useAuth0();

  return (
    <Container maxWidth="lg" style={{ height: "100%" }}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            Peer Review Tool
          </Typography>
          {isAuthenticated ? (
            <>
              <Typography variant="h5">{user.nickname}</Typography>
              <Button
                color="inherit"
                onClick={() =>
                  logout({ returnTo: "http://localhost:3000/tasks" })
                }
              >
                Logout
              </Button>
            </>
          ) : (
            <Button color="inherit" onClick={() => loginWithRedirect()}>
              Login
            </Button>
          )}
        </Toolbar>
      </AppBar>
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
          <Route path="/tasks/:uid" component={TaskDetails} />
          <Route path="/tasks" component={Tasks} />
          <Redirect to="/tasks" />
        </Switch>
      </div>
    </Container>
  );
};

export default App;
