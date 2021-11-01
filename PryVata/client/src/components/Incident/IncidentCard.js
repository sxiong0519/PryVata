import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import './Incident.css'

const IncidentCard = ({ incident }) => {
    
  return (
      <>
      <Card >
      <CardBody className="IncidentCard">
            
          <Link className="Link" to={`/incident/detail/${incident.id}`}>{incident.title}</Link> 
          <div>
          {new Date(incident.dueDate).toLocaleDateString()} 
          </div>
      
      </CardBody>   
    </Card>
    </>
  );
};

export default IncidentCard;