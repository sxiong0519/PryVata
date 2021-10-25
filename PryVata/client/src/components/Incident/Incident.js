import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Incident = ({ incident }) => {
    
  return (
      <>
      <Card >
      <CardBody className="IncidentCard">
            
          {incident.description}
      
      </CardBody>   
    </Card>
    </>
  );
};

export default Incident;