import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import './Incident.css'

const IncidentCard = ({ incident }) => {
    
    return (
        <>
        <Card >
        <CardBody className="IncidentCard">
        <div className="incidentLink">
            <Link className="Link titleLink" to={`/incident/detail/${incident.id}`}>{incident.title}</Link> 
            </div>
            <div>
            {new Date(incident.dueDate).toLocaleDateString()} 
            </div>
            <div>
              {incident.user.fullName}
            </div>
        
        </CardBody>   
      </Card>

      </>
    );
  };
  

export default IncidentCard;