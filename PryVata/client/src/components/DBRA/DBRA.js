import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

const DBRA = ({ db }) => {
    
  return (
      <>
      <Card >
      <CardBody className="DBRACard">
            {db.id}
          {/* <Link to={`/DBRA/detail/${DBRA.id}`}>{DBRA.title}</Link>
       */}
      </CardBody>   
    </Card>
    </>
  );
};

export default DBRA;