import React from "react";
import { Switch } from "react-router-dom";
import { Container } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { useAuth0 } from "@auth0/auth0-react";

import Workshops from "./components/Workshops";
import WorkshopDetails from "./components/WorkshopDetails";
import ProtectedRoute from "./components/auth/ProtectedRoute";
import Loading from "./components/Loading";
import NavBar from "./components/common/NavBar";
import Breadcrumbs from "./components/common/Breadcrumbs";

const useStyles = makeStyles({
  root: {
    height: "100%",
  },
  "@global": {
    "html, body, #root": {
      height: "100%",
    },
  },
});

const App = _ => {
  const classes = useStyles();
  const { isLoading } = useAuth0();

  if (isLoading) {
    return <Loading />;
  }

  return (
    <Container maxWidth="lg" disableGutters className={classes.root}>
      <NavBar />
      <Breadcrumbs />
      <Switch>
        <ProtectedRoute path="/workshops/:uid" component={WorkshopDetails} />
        <ProtectedRoute path="/workshops" component={Workshops} />
      </Switch>
    </Container>
  );
};

export default App;
