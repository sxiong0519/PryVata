import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import './Incident.css'

const Incident = ({ incident }) => {
    
  return (
      <>
      <div className="i">
      <Card >
      <CardBody className="IncidentCard">
            <div className="incidentLink">
          <Link className="Link" to={`/incident/detail/${incident.id}`}>{incident.title}</Link> 
          </div>
          <div width="15px">
          {new Date(incident.dateReported).toLocaleDateString()} 
          </div>
          <div>
            {incident.user.fullName}
          </div>
          <div>
            {incident.reportable !== null ? "Closed" : " "}
          </div>
      
      </CardBody>   
    </Card>
 
    </div>
    </>
  );
};

export default Incident;