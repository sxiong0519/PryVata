import React, { useEffect, useState } from "react";
import { useHistory, useParams } from 'react-router-dom';
import { getAllFacilities } from "../../modules/facilityManager";
import { addIncident, getIncidentById, updateIncident } from "../../modules/IncidentManager";
import { getAllUsers } from "../../modules/userManager"

const IncidentForm = () => {
    const [incident, setIncident] = useState({})
    const [users, setUsers] = useState([])
    const [facilities, setFacilities] = useState([])
    const history = useHistory();
    const { incidentId } = useParams();

    useEffect(() => {
        if(incidentId)
        {
            getIncidentById(incidentId)
            .then((i) => {setIncident(i)})
        }
        getAllUsers().then(u => {setUsers(u)})
        getAllFacilities().then(f => {setFacilities(f)})
    }, [])
  

    const handleControlledInputChange = (event) => {
        const newIncident = { ...incident }
          newIncident[event.target.id] = event.target.value
          setIncident(newIncident)
        }

        const handleClickSaveIncident = () => {
            if (incident.title === undefined || incident.description === undefined) {
                window.alert("Please complete the form")
            } 
            else if (incidentId) {
                updateIncident({
                    id: incidentId,
                    title: incident.title,
                    description: incident.description,
                    // dateCreated: new Date(Date.now()).toISOString(),
                    dateReported: incident.dateReported,
                    dateOccurred: incident.dateOccurred,
                    facilityId: incident.facilityId,
                    confirmed: null,
                    reportable: null
                })
                .then((p) => history.push(`/incident/details/${incidentId}`))
            } 
            else {
                const newIncident = {
                    assignedUserId: incident.assignedUserId,
                    title: incident.title,
                    description: incident.description,
                    // dateCreated: new Date(Date.now()).toISOString(),
                    dateReported: incident.dateReported,
                    dateOccurred: incident.dateOccurred,
                    facilityId: incident.facilityId,
                    confirmed: null,
                    reportable: null
              }
              addIncident(newIncident)
              .then((i) => history.push("/incident"))
              }
            }
            
            return (
                <>
                <center>
                <form className="IncidentForm">
                    <h2 className="IncidentForm__title post_header">{!incidentId ? "Create a New Incident" : "Update Incident" }</h2>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="title">Title</label>
                            <input type="text" id="title" required autoFocus className="form-control" placeholder="Required" value={incident.title} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    <fieldset>
                        <div className="form-group">
                            <label htmlFor="description">Description</label>
                            <input type="text" id="description" required autoFocus className="form-control" placeholder="Required" value={incident.description} onChange={handleControlledInputChange} />
                        </div>
                    </fieldset>
                    <fieldset>
                  <div className="form-group">
                    <label htmlFor="assignedUserId">Investigator</label>
                    <select value={incident.assignedUserId} name="assignedUserId" id="assignedUserId" className="form-control" onChange={handleControlledInputChange}>
                      <option value="0">Select an Investigator</option>
                      {users.map(u => (
                        <option key={u.id} value={u.id}>
                          {u.fullName}
                        </option>
                      ))}
                    </select>
                  </div>
                  </fieldset>
                  <fieldset>
                  <div className="form-group">
                    <label htmlFor="facilityId">Facility</label>
                    <select value={incident.facilityId} name="facilityId" id="facilityId" className="form-control" onChange={handleControlledInputChange}>
                      <option value="0">Select a Facility</option>
                      {facilities.map(f => (
                        <option key={f.id} value={f.id}>
                          {f.facilityName}
                        </option>
                      ))}
                    </select>
                  </div>
                  </fieldset>
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
                    {/* <fieldset>
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
                    </fieldset> */}
                    <div className="buttons"><button className="pfbtns" onClick={
                        (event) => {
                            event.preventDefault()
                            handleClickSaveIncident()
                        }
                    }>
                    Save Incident
                    </button> {incidentId ? <button className="pfbtns" onClick={() => history.goBack()}>Cancel</button> : ""}</div> 
                </form>
                </center>
                </>
            )
        }
        
        export default IncidentForm;