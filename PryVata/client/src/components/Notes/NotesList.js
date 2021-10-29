import React, { useEffect, useState } from "react";
import { getAllNotes } from "../../modules/notesManager.js";
import { getIncidentById } from "../../modules/IncidentManager.js";
import { useParams, useHistory, Link } from "react-router-dom";
import Notes from "./Notes.js";


const NotesList = () => {
  const [ notes, setNotes] = useState([]);
  const [ incident, setIncident ] = useState({})
  const {id} = useParams()

  const getNotes = () => {
    getAllNotes().then(notes => setNotes(notes));
  };

  useEffect(() => {
    getNotes()
    getIncidentById(id)
    .then((resp) => setIncident(resp))
  }, []);

  const findNote = notes.filter(n => n.incidentId === parseInt(id))

  return (
    <div className="container">
      <div>
        {/* <Link to="/note/add">New note</Link> */}
      </div>
      {findNote ? findNote.slice(0,1).map((fn) => (
          <Notes fn={fn} key={fn.id} />
        )) : ""}
      
    </div>
  );
};

export default NotesList;