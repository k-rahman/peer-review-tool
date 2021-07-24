import React from "react";
import { Route } from "react-router-dom";
import { withAuthenticationRequired } from "@auth0/auth0-react";
import Loading from "../Loading";
import NavBar from "../common/NavBar";
import Breadcrumbs from "../common/Breadcrumbs";
import Footer from "../common/Footer";


const ProtectedRoute = ({ component, ...args }) => (
	<>
		<NavBar />
		<Breadcrumbs />
		<Route
			component={withAuthenticationRequired(component, {
				onRedirecting: () => <Loading />,
			})}
			{...args}
		/>
		<Footer />
	</>
);

export default ProtectedRoute;