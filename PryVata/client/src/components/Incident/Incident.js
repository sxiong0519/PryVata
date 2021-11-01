import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import './Incident.css'

const Incident = ({ incident }) => {
    
  return (
      <>
      <Card >
      <CardBody className="IncidentCard">
            
          <Link to={`/incident/detail/${incident.id}`}>{incident.title}</Link> 
          <div>
          {new Date(incident.dateReported).toLocaleDateString()} 
          </div>
      
      </CardBody>   
    </Card>
    </>
  );
};

export default Incident;