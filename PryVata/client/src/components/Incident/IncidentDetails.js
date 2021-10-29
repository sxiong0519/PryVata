import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { getIncidentById, deleteIncident } from "../../modules/IncidentManager";
import DBRAList from "../DBRA/DBRAList";
import NotesForm from "../Notes/NotesForm";
import NotesList from "../Notes/NotesList";



const IncidentDetails = () => {
    const [incident, setIncident] = useState();
    const [noteForm, setNoteForm] = useState(false);
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

if(!incident) {
    return null;
}

//where to show form
const showNoteForm = () => setNoteForm(true)
const style = noteForm ? {display: 'block'} : {display: 'none'}


return (
    <>
    <div className="container">
        Title: {incident.title}
        <br/>
        Assigned: {incident.user.fullName}
        <br/>
        Description: {incident.description}
        <br/>
        <Link to={`/incident/edit/${incident.id}`}>Edit</Link>
        <button onClick={deleteAnIncident}>Delete</button>
        <DBRAList/>
        <div>
        <Link onClick={showNoteForm}>New note</Link>
      </div>
      <div style={style}>
        <NotesForm incident={incident} />
      </div>
        <NotesList/>
    </div>
    </>
)

}

export default IncidentDetails;