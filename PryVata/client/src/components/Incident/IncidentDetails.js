import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getIncidentById, deleteIncident } from "../../modules/IncidentManager";
import DBRAList from "../DBRA/DBRAList";
import NotesForm from "../Notes/NotesForm";
import NotesList from "../Notes/NotesList";
import { ListGroup, ListGroupItem } from "reactstrap";
import PatientForm from "../Patient/PatientForm";
import { deletePatient } from "../../modules/patientManager";



const IncidentDetails = () => {
    const [incident, setIncident] = useState();
    const [noteForm, setNoteForm] = useState(false);
    const [patientForm, setPatientForm] = useState(false);
    const { id } = useParams();

    const history = useHistory();

    useEffect(() => {
    getIncidentById(id).then((i) => { 
        setIncident(i)
            })
}, []);

const deleteAnIncident = (event) => {
    event.preventDefault();
    const confirmDelete = window.confirm(
      "Are you sure you would like to delete the incident?"
    );
    if (confirmDelete) {
      deleteIncident(incident.id).then(() => {
        history.push("/incident");
      });
    }
  };

  const deleteAPatient = (event) => {
    event.preventDefault();
    const confirmDelete = window.confirm(
      "Are you sure you would like to delete the patient information?"
    );
    if (confirmDelete) {
      deletePatient(incident.patient.id).then(() => {
        history.push("/incident");
      });
    }
  };

if(!incident) {
    return null;
}

//where to show form
const showNoteForm = () => setNoteForm(true)
const style = noteForm ? {display: 'block'} : {display: 'none'}
const showPatientForm = () => setPatientForm(true)
const patientStyle = patientForm ? {display: 'block'} : {display: 'none'}

// 30day date


const addThirty = (days) => {
  const date = new Date(incident.dateReported)
  date.setDate(date.getDate() + days);
  return date.toLocaleDateString();
};


return (
    <>
    <div className="container">
        <h1>{incident.title}</h1>
        <br/>
        Assigned: {incident.user.fullName}
        <br/>
        Description: {incident.description}
        <br/>
        Date Occurred: {new Date(incident.dateOccurred).toLocaleDateString()} 
        <br/>
        Date Reported/Received: {new Date(incident.dateReported).toLocaleDateString()} 
        <br/>
        Due Date: {addThirty(30)}
        <br/>
        Facility: {incident.facility.facilityName}
        <br/>
        Confirmed? {incident.confirmed === null ? "Undetermined" : <>{incident.confirmed === true ? "Yes" : "No"}</>}
        <br/>
        Reportable? {incident.reportable === null ? "Undetermined" : <>{incident.reportable === true ? "Yes" : "No"}</>}
        <br/>
        <Link className="Link" onClick={showPatientForm}>Add Patient</Link>
        <br/>
        
        <div style={patientStyle}>
        <PatientForm incident={incident}/>
        </div>
        Patient Involved: 
        <ListGroup>
        {incident.patient.map(pt => 
            <ListGroupItem className="ListGroupItem">{pt.firstName} {pt.lastName} <button className="Link" onClick={(event) => {
              event.preventDefault();
              const confirmDelete = window.confirm(
                "Are you sure you would like to delete the patient information?"
              );
              if (confirmDelete) {
                deletePatient(pt.id).then(() => {
                  window.location.reload();
                });
              }
            }}>Delete Patient</button></ListGroupItem>)}
        </ListGroup>
        <br/>
        <Link className="Link" to={`/incident/edit/${incident.id}`}>Edit</Link> || {" "}
        <Link className="Link" onClick={deleteAnIncident}>Delete</Link>
        <div className="dbList">
        <DBRAList/>
        </div>
        <div>
        <Link className="Link" onClick={showNoteForm}>New note</Link>
      </div>
      <div style={style}>
        <NotesForm incident={incident} />
      </div>
      <div>
        <NotesList/>
        </div>
    </div>
    </>
)

}

export default IncidentDetails;