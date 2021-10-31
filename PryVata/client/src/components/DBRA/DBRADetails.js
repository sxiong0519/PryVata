import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getDBRAById, deleteDBRA } from "../../modules/dbraManager";
import { ListGroup, ListGroupItem } from "reactstrap";


const DBRADetails = () => {
    const [DBRA, setDBRA] = useState();
    const { dbraId } = useParams();

    const history = useHistory();

    useEffect(() => {
    getDBRAById(dbraId).then((i) => { 
        setDBRA(i)
            })
}, []);

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
        {DBRA.exceptionId !== 5 ? <>DBRA meets this exception: <br/> {DBRA.exception.exception} </>: <>DBRA did not meet any exception
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
            <ListGroupItem>{i.informationType}</ListGroupItem>)}
        </ListGroup>
        Controls: 
        <ListGroup>
        {DBRA.control.map(c =>
            <ListGroupItem>{c.control} </ListGroupItem>)}
        </ListGroup>
        </> }
        <br/>
        <Link to={`/DBRA/edit/${DBRA.id}`}>Edit</Link>
        <Link to={`/incident/detail/${DBRA.incidentId}`}> Return to Incident</Link>
        <button onClick={deleteADBRA}>Delete</button>
    </div>
    </>
)

}

export default DBRADetails;