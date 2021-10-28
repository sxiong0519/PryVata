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
        history.push(`/incident/${DBRA.incidentId}`);
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
        {DBRA.exceptionId !== 5 ? <>DBRA meets this exception: {DBRA.exception.exception} </>: <>DBRA did not meet any exception</>}
        <br/>
        {DBRA.method ? <>Method of Disclosure: {DBRA.method.methodType} </>: "" }
        <br/>
        {DBRA.recipient ? <>Recipient Type: {DBRA.recipient.recipientType} </> : ""}
        <br/>
        {DBRA.circumstance ? <> Circumstance: {DBRA.circumstance.circumstances} </> : "" }
        <br/>
        {DBRA.disposition ? <> Disposition: {DBRA.disposition.disposition} </> : ""}
        <br/>
        {DBRA.information ? 
        <>
        Information: 
        <ListGroup>
        {DBRA.information.map(i => 
            <ListGroupItem>{i.informationType}</ListGroupItem>)}
        </ListGroup>
        </> : ""}
        {DBRA.control ?
        <>
        Controls: 
        <ListGroup>
        {DBRA.control.map(c =>
            <ListGroupItem>{c.control} </ListGroupItem>)}
        </ListGroup>
        </> : ""}
        <Link to={`/DBRA/edit/${DBRA.id}`}>Edit</Link>
        <button onClick={deleteADBRA}>Delete</button>
    </div>
    </>
)

}

export default DBRADetails;