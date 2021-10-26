import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Authorization/Login";
import Hello from "./Hello";
import IncidentList from "./Incident/IncidentList";
import IncidentDetails from "./Incident/IncidentDetails";
import IncidentForm from "./Incident/IncidentForm";
import DBRADetails from "./DBRA/DBRADetails";

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
        {isLoggedIn ? <IncidentList /> : <Redirect to="/login" />}
          
        </Route>

        <Route path="/incident/detail/:id">
        {isLoggedIn ? <IncidentDetails /> : <Redirect to="/login" />}
          
        </Route>

        <Route path="/incident/add">
        {isLoggedIn ? <IncidentForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/incident/edit/:id">
        {isLoggedIn ? <IncidentForm /> : <Redirect to="/login" />}          
        </Route>

        
        <Route path="/DBRA/detail/:dbraId">
        {isLoggedIn ? <DBRADetails /> : <Redirect to="/login" />}          
        </Route>

        {/* <Route path="/register">
          <Register />
        </Route> */}
      </Switch>
    </main>
  );
};