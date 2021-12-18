import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getIncidentById, deleteIncident } from "../../modules/IncidentManager";
import DBRAList from "../DBRA/DBRAList";
import NotesForm from "../Notes/NotesForm";
import NotesList from "../Notes/NotesList";
import { ListGroup, ListGroupItem } from "reactstrap";
import PatientForm from "../Patient/PatientForm";
import { deletePatient, getAllPatients } from "../../modules/patientManager";



const IncidentDetails = () => {
    const [incident, setIncident] = useState();
    const [noteForm, setNoteForm] = useState(false);
    const [patientForm, setPatientForm] = useState(false);
    const [ patients, setPatients ] = useState([]);
    const { id } = useParams();

    const history = useHistory();

    useEffect(() => {
    getIncidentById(id).then((i) => { 
        setIncident(i)
            })
    getAllPatients().then((p) => setPatients(p))
}, [id]);

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

if(!incident) {
    return null;
}


console.log(patients, "patient")

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

const filteredPatients = patients.filter(p => p.incidentId === incident.id);
console.log(filteredPatients, "PATIENTS")

return (
    <>
    <div className="container">
        <h1>{incident.title}</h1>
        <br/>
        <h4>Incident Details:</h4>
        <b>Assigned:</b> {incident.user.fullName}
        <br/>
        <b>Description</b>: {incident.description}
        <br/>
        <b>Date Occurred:</b> {new Date(incident.dateOccurred).toLocaleDateString()} 
        <br/>
        <b>Date Reported/Received:</b> {new Date(incident.dateReported).toLocaleDateString()} 
        <br/>
        <b>Due Date:</b> {addThirty(30)}
        <br/>
        <b>Facility:</b> {incident.facility.facilityName}
        <br/>
        <b>Confirmed?</b> {incident.confirmed === null ? "Undetermined" : <>{incident.confirmed === true ? "Yes" : "No"}</>}
        <br/>
        <b>Reportable?</b> {incident.reportable === null ? "Undetermined" : <>{incident.reportable === true ? "Yes" : "No"}</>}
        <br/>
        {incident.confirmed === true ? <><b>DBRA: </b>
        <div className="dbList">
        <DBRAList/>
        </div> </>: <><b>DBRA: </b>Insufficient evidence, DBRA not necessary </>}
        <br/>
        <Link className="Link" to={`/incident/edit/${incident.id}`}>Edit</Link> || {" "}
        <Link className="Link" onClick={deleteAnIncident}>Delete</Link>
        <p/>
        <h4>Patient Involved: </h4>
        <Link className="Link" onClick={showPatientForm}>Add Patient</Link>
        <br/>
        
        <div style={patientStyle}>
        <PatientForm incident={incident}/>
        </div>
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
        <p/>
        <p/>
        <h4>Notes:</h4>
        <div>
        <Link className="Link" onClick={showNoteForm}>Add note</Link>
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