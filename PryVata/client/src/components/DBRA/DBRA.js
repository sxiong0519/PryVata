import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const DBRA = ({ db }) => {
    
  return (
      <>
      <Card >
      <CardBody className="DBRACard">
          <Link className="Link" to={`/DBRA/detail/${db.id}`}>View DBRA Details</Link>
      
      </CardBody>   
    </Card>
    </>
  );
};

export default DBRA;