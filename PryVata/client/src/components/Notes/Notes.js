import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { deleteNotes } from "../../modules/notesManager";

const Notes = ({ fn }) => {
    
    const deleteANote = (event) => {
        event.preventDefault();
        const confirmDelete = window.confirm(
          "Are you sure you would like to delete the note?"
        );
        if (confirmDelete) {
          deleteNotes(fn.id)
        }
      };

  return (
      <>
      <Card >
      <CardBody className="NotesCard">
          {fn.description}
          <br/>
          {fn.imageUrl ? <><img src={fn.imageUrl} alt={fn.notes}/></> : ""}
          <br/>
          <Link to={`/notes/edit/${fn.id}`}>Edit</Link>
        <button onClick={deleteANote}>Delete</button>
      </CardBody>   
    </Card>
    </>
  );
};

export default Notes;