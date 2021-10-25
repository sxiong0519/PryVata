import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Incident = ({ incident }) => {
    
  return (
      <>
      <Card >
      <CardBody className="IncidentCard">
            
          <Link to={`/incident/detail/${incident.id}`}>{incident.description}</Link>
      
      </CardBody>   
    </Card>
    </>
  );
};

export default Incident;