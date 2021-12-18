import React, { useEffect, useState } from "react";
import { getAllNotes } from "../../modules/notesManager.js";
import { useParams } from "react-router-dom";
import Notes from "./Notes.js";


const NotesList = () => {
  const [ notes, setNotes] = useState([]);
  const {id} = useParams()

  const getNotes = () => {
    getAllNotes().then(notes => setNotes(notes));
  };

  useEffect(() => {
    getNotes()
  }, []);

  const findNote = notes.filter(n => n.incidentId === parseInt(id))

  return (
    <div className="container notesList">
      {findNote ? findNote.map((fn) => (
          <Notes fn={fn} key={fn.id} />
        )) : ""}
      
    </div>
  );
};

export default NotesList;