import React, { useState, useEffect } from "react";
import { Redirect, Switch } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { ToastContainer } from "react-toastify";
import { Container } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

import Workshops from "./components/Workshops";
import Workshop from "./components/Workshop";
import ProtectedRoute from "./components/auth/ProtectedRoute";
import ProtectedRouteWithNavBar from "./components/auth/ProtectedRouteWithNavBar";
import CompleteProfile from "./components/auth/CompleteProfile";
import Loading from "./components/Loading";
import NotFound from "./components/common/NotFound";

import "../node_modules/@syncfusion/ej2-base/styles/material.css";
import "../node_modules/@syncfusion/ej2-buttons/styles/material.css";
import "../node_modules/@syncfusion/ej2-calendars/styles/material.css";
import "../node_modules/@syncfusion/ej2-dropdowns/styles/material.css";
import "../node_modules/@syncfusion/ej2-inputs/styles/material.css";
import "../node_modules/@syncfusion/ej2-navigations/styles/material.css";
import "../node_modules/@syncfusion/ej2-popups/styles/material.css";
import "../node_modules/@syncfusion/ej2-splitbuttons/styles/material.css";
import "../node_modules/@syncfusion/ej2-react-grids/styles/material.css";
import "react-toastify/dist/ReactToastify.css";

const useStyles = makeStyles(theme => ({
  root: {
    display: "flex",
    flexDirection: "column",
    backgroundColor: theme.palette.background.default,
  },
  profile: {
    display: "flex",
    flexDirection: "column",
    margin: 0,
    backgroundColor: "#000",
    minHeight: "100vh",
    maxWidth: "100%",
  },
  "@global": {
    "html, body, #root": {
      minHeight: "100vh",
      height: "100%",
    },
  },
}));

const App = _ => {
  const classes = useStyles();
  const [hasName, setHasName] = useState(true); // user has name set up

  const { user, isLoading } = useAuth0();

  useEffect(() => {
    if (user && (!user.name || user.name.length === 0)) setHasName(false); //check if user doesn't have a name in the user object
  }, [user]);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <>
      <Container
        maxWidth="lg"
        disableGutters
        className={hasName ? classes.root : classes.profile}
      >
        <ToastContainer
          autoClose={3000}
          pauseOnHover={false}
          pauseOnFocusLose={false}
          limit={1}
        />
        <Switch>
          <ProtectedRoute
            path="/complete-profile"
            component={() => <CompleteProfile setHasName={setHasName} />}
          />
          {!hasName && <Redirect to="/complete-profile" />}
          <ProtectedRoute path="/not-found" component={NotFound} />
          <ProtectedRouteWithNavBar
            path="/workshops/:uid"
            component={Workshop}
          />
          <ProtectedRouteWithNavBar path="/workshops" component={Workshops} />
          <Redirect path="/" exact to="/workshops" />
          <Redirect to="/not-found" />
        </Switch>
      </Container>
    </>
  );
};

export default App;
