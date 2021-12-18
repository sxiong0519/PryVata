import React, { useEffect, useState } from 'react';
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Authorization/Login";
import Hello from "./Hello";
import IncidentList from "./Incident/IncidentList";
import IncidentDetails from "./Incident/IncidentDetails";
import IncidentForm from "./Incident/IncidentForm";
import DBRADetails from "./DBRA/DBRADetails";
import DBRAForm from "./DBRA/DBRAForm";
import MyIncidentList from "./Incident/MyIncidentList";
import { getCurrentUser } from '../modules/userManager';

export default function ApplicationViews({ isLoggedIn }) {
  const [ user, setUser] = useState({})

  useEffect(() => {
    if(isLoggedIn){
      getCurrentUser().then((user) => setUser(user))
    }
  }, [isLoggedIn]);

  console.log(user, 'us')
  return (
    <main>
      <Switch>
        
        <Route path="/" exact>
          {isLoggedIn ? <Hello user={user} /> : <Redirect to="/login" />}
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
        {isLoggedIn ? <IncidentForm user={user} /> : <Redirect to="/login" />}
        </Route> 

        <Route path="/incident/edit/:id">
        {isLoggedIn ? <IncidentForm user={user}/> : <Redirect to="/login" />}          
        </Route> 

        <Route path="/myIncidents">
        {isLoggedIn ? <MyIncidentList /> : <Redirect to="/login" />}          
        </Route>
        
        <Route path="/DBRA/detail/:dbraId">
        {isLoggedIn ? <DBRADetails /> : <Redirect to="/login" />}          
        </Route>

        <Route path="/DBRA/add">
        {isLoggedIn ? <DBRAForm /> : <Redirect to="/login" />}          
        </Route>

        <Route path="/DBRA/edit/:dbraId">
        {isLoggedIn ? <DBRAForm /> : <Redirect to="/login" />}          
        </Route>

        {/* <Route path="/register">
          <Register />
        </Route> */}
      </Switch>
    </main>
  );
};