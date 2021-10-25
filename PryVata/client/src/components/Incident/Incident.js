import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const Incident = ({ incident }) => {
    
  return (
      <>
      <Card >
      <CardBody className="IncidentCard">
      <div>
          {incident.title}
          <br/>
          {incident.description}
      </div>
      </CardBody>   
    </Card>
    </>
  );
};

export default Incident;