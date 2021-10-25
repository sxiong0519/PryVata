import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Authorization/Login";
import Hello from "./Hello";
import IncidentList from "./Incident/IncidentList";
import IncidentDetails from "./Incident/IncidentDetails";

export default function ApplicationViews({ isLoggedIn }) {

  return (
    <main>
      <Switch>
        <Route path="/" exact>
          {isLoggedIn ? <Hello /> : <Redirect to="/login" />}
        </Route>

        <Route path="/login">
          <Login />
        </Route>

        <Route path="/incident" exact>
          <IncidentList />
        </Route>

        <Route path="/incident/detail/:id">
          <IncidentDetails />
        </Route>

        {/* <Route path="/register">
          <Register />
        </Route> */}
      </Switch>
    </main>
  );
};