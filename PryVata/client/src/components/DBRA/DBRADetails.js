import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getDBRAById, deleteDBRA } from "../../modules/dbraManager";
import { ListGroup, ListGroupItem } from "reactstrap";
import './DBRA.css'


const DBRADetails = () => {
    const [DBRA, setDBRA] = useState();
    const { dbraId } = useParams();

    const history = useHistory();

    useEffect(() => {
    getDBRAById(dbraId).then((i) => { 
        setDBRA(i)
            })
}, [dbraId]);

const deleteADBRA = (event) => {
    event.preventDefault();
    const confirmDelete = window.confirm(
      "Are you sure you would like to delete the DBRA?"
    );
    if (confirmDelete) {
      deleteDBRA(DBRA.id).then(() => {
        history.goBack();
      });
    }
  };

if(!DBRA) {
    return null;
}


return (
    <>
    <div className="container">
        Completed by: {DBRA.user.fullName}
        <br/>
        {DBRA.exceptionId !== 5 ? <><b>DBRA meets this exception: </b><br/> {DBRA.exception.exception} </>: <><b>DBRA did not meet any exception</b>
        <br/>
        Method of Disclosure: {DBRA.method.methodType} 
        <br/>
        Recipient Type: {DBRA.recipient.recipientType} 
        <br/>
        Circumstance: {DBRA.circumstance.circumstances}
        <br/>
        Disposition: {DBRA.disposition.disposition}
        <br/>
        <label>Information: </label>
        <ListGroup>
        {DBRA.information.map(i => 
            <ListGroupItem className="ListGroupItem">{i.informationType}</ListGroupItem>)}
        </ListGroup>
        Controls: 
        { DBRA.control.length > 0 ? <ListGroup>
        { DBRA.control.map(c =>
            <ListGroupItem className="ListGroupItem">{c.control} </ListGroupItem>)}
        </ListGroup> : " No controls"}</>}
        <br/>
        Risk Level: {DBRA.riskValue}
        <br/>
        <Link className="Link" to={`/incident/detail/${DBRA.incidentId}`}> Return to Incident</Link> || {" "}
        <Link className="Link" to={`/DBRA/edit/${DBRA.id}`}>Edit</Link> ||  {" "}
        <Link className="Link" onClick={deleteADBRA}>Delete</Link>
    </div>
    </>
)

}

export default DBRADetails;