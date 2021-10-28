import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getIncidentById, deleteIncident } from "../../modules/IncidentManager";
import DBRAForm from "../DBRA/DBRAForm";
import DBRAList from "../DBRA/DBRAList";



const IncidentDetails = () => {
    const [incident, setIncident] = useState();
    const { id } = useParams();

    const history = useHistory();

    useEffect(() => {
    getIncidentById(id).then((i) => { 
        setIncident(i)
            })
}, []);

const deleteAnIncident = (event) => {
    event.preventDefault();
    const confirmDelete = window.confirm(
      "Are you sure you would like to delete the incident?"
    );
    if (confirmDelete) {
      deleteIncident(incident.id).then(() => {
        history.push("/incident");
      });
    }
  };

if(!incident) {
    return null;
}


return (
    <>
    <div className="container">
        Title: {incident.title}
        <br/>
        Assigned: {incident.user.fullName}
        <br/>
        Description: {incident.description}
        <br/>
        <Link to={`/incident/edit/${incident.id}`}>Edit</Link>
        <button onClick={deleteAnIncident}>Delete</button>
        <DBRAForm incident={incident}/>
        <DBRAList/>
    </div>
    </>
)

}

export default IncidentDetails;