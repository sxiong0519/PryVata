import React from "react";
import { Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";
import { deleteNotes } from "../../modules/notesManager";
import { useHistory } from "react-router-dom";
import './Notes.css'

const Notes = ({ fn }) => {
    const history = useHistory();
    
    const deleteANote = (event) => {
        event.preventDefault();
        const confirmDelete = window.confirm(
          "Are you sure you would like to delete the note?"
        );
        if (confirmDelete) {
          deleteNotes(fn.id)
          .then(() => history.go(0))
        }
      };

  return (
      <>
      <Card >
      <CardBody className="NotesCard">
          {fn.description}
          <br/>
          {fn.imageUrl ? <><img class="notesImg" src={fn.imageUrl} alt={fn.notes}/></> : ""}
          <br/>
          {new Date(fn.createDateTime).toLocaleDateString()} 
          <br/>
          <Link className="Link" to={`/notes/edit/${fn.id}`}>Edit</Link> || {" "}
        <Link className="Link"  onClick={deleteANote}>Delete</Link>
      </CardBody>   
    </Card>
    </>
  );
};

export default Notes;