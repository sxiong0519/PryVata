import React, { useEffect, useState } from "react";
import { useHistory, useParams, render } from "react-router-dom";
import { addNotes, getNotesById, updateNotes } from "../../modules/notesManager";



const NotesForm = ({incident}) => {
    const [ note, setNote ] = useState({})
    const [noteForm, setNoteForm] = useState(true)
    
    const history = useHistory();
    const { notesId } = useParams();

    useEffect(() => {
        if(notesId) {
            getNotesById(notesId).then((n) => setNote(n))
        }
    }, [])

    const handleControlledInputChange = (event) => {
        const newNote = { ...note };
        newNote[event.target.id] = event.target.value;
        setNote(newNote);
      };

    const handleClickSaveNotes = () => {
        if (note.description === undefined) {
          window.alert("Please complete the form");
        } 
        else if (notesId) {
          updateNotes({
            id: notesId,        
            createDateTime: new Date(Date.now()).toISOString(),   
            description: note.description,
            imageUrl: note.imageUrl,
          }).then((p) => history.goBack());
        } 
        else {
          const newNote = {
            createDateTime: new Date(Date.now()).toISOString(),   
            description: note.description,
            imageUrl: note.imageUrl,
            incidentId: incident.id
          };
          addNotes(newNote).then(() => history.go(0));
        }
      };
    
      const showNoteForm = () => setNoteForm(false)
      const style = noteForm ? {display: 'block'} : {display: 'none'}

    return (
        <>
         <form style={style} className="notesForm">
            <h2 className="notesForm__title notes_header">Create a New Note</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="description">Description</label>
                    <textarea type="text" id="description" required autoFocus className="form-control" placeholder="Investigation details" rows="10" columns="5" value={note.description} onChange={handleControlledInputChange} />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="imageUrl">Image</label>
                    <input type="text" id="imageUrl" required autoFocus className="form-control" placeholder="Add URL" value={note.imageUrl} onChange={handleControlledInputChange} />
                </div>
            </fieldset>
            <div className="buttons"><button className="Link"  onClick={
                (event) => {
                    event.preventDefault()
                    handleClickSaveNotes()
                    showNoteForm()
                }
            }>
            {notesId ? "Update" : "Save"}
            </button> {notesId ? <button className="Link"  onClick={() => history.goBack()}>Cancel</button> : ""}</div> 
        </form>
        </>
    )
}

export default NotesForm