import React, { useEffect, useState } from "react";
import { useHistory, useParams } from 'react-router-dom';
import { addIncident, getIncidentById, updateIncident } from "../../modules/IncidentManager";
// import { getAllUsers } from "../../modules/usersManager"

const IncidentForm = () => {
    const [incident, setIncident] = useState({})
    const [users, setUsers] = useState([])
    const history = useHistory();
    const incidentId = useParams();

    useEffect(() =>
    get)

    const handleControlledInputChange = (event) => {
        const newIncident = { ...incident }
          newIncident[event.target.id] = event.target.value
          setIncident(newIncident)
        }

        const handleClickSavePost = () => {
            if (incident.title === undefined || incident.description === undefined) {
                window.alert("Please complete the form")
            } 
            // else if (postId) {
            //     updatePost({
            //         id: postId,
            //         title: post.title,
            //         content: post.content,
            //         imageLocation: post.imageLocation,
            //         createDateTime: new Date(Date.now()).toISOString(),
            //         publishDateTime: post.publishDateTime,
            //         isApproved: true,
            //         categoryId: post.categoryId
            //     })
            //     .then((p) => history.push("/posts"))
            // } 
            else {
                const newIncident = {
                    assignedUserId: incident,
                    title: post.title,
                    description: post.content,
                    dateCreated: new Date(Date.now()).toISOString(),
                    dateReported: post.publishDateTime,
                    dateOccurred: post.publishDateTime,
                    facilityId: incident,
                    confirmed: incident,
                    reportable: incident
              }
              addIncident(newIncident)
              .then((i) => history.push("/incident"))
              }
            }
            
            return (
                <>
                <center>
                <form className="IncidentForm">
                    <h2 className="IncidentForm__title post_header">Create a New Post</h2>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="title">Title</label>
                            <input type="text" id="title" required autoFocus className="form-control" placeholder="Required" value={incident.title} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="description">Description</label>
                            <input type="text" id="description" required autoFocus className="form-control" placeholder="Required" value={incident.title} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    {/* <fieldset>
                  <div className="form-group">
                    <label htmlFor="category">Category</label>
                    <select value={post.categoryId} name="categoryId" id="categoryId" className="form-control" onChange={handleControlledInputChange}>
                      <option value="0">Select a Category</option>
                      {category.map(c => (
                        <option key={c.id} value={c.id}>
                          {c.name}
                        </option>
                      ))}
                    </select>
                  </div>
                  </fieldset> */}
                  <fieldset>
                        <div className="form-group">
                            <label htmlFor="dateReported">Date Reported</label>
                            <input type="date" id="dateReported" required autoFocus className="form-control" placeholder="Enter a date" value={incident.dateReported} onChange={handleControlledInputChange} />
                        </div>
                 </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="dateOccurred">Date Occurred</label>
                            <input type="date" id="dateOccurred" required autoFocus className="form-control" placeholder="Enter a date" value={incident.dateOccurred} onChange={handleControlledInputChange} />
                        </div>
                 </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="imageLocation">Image Location</label>
                            <input type="text" id="imageLocation" autoFocus className="form-control" placeholder="" value={post.imageLocation} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="content">Body</label>
                            <textarea type="text" id="content" required autoFocus className="form-control" placeholder="Required" rows="10" columns="5" value={post.content} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    <div className="buttons"><button className="pfbtns" onClick={
                        (event) => {
                            event.preventDefault()
                            handleClickSavePost()
                        }
                    }>
                    {postId ? "Update Post" : "Save post"}
                    </button> {postId ? <button className="pfbtns" onClick={() => history.goBack()}>Cancel</button> : ""}</div> 
                </form>
                </center>
                </>
            )
        }
        
        export default IncidentForm;