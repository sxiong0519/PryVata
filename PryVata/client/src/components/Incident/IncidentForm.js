import React, { useEffect, useState } from "react";
import { useHistory, useParams, render } from "react-router-dom";
import { getAllFacilities } from "../../modules/facilityManager";
import {
  addIncident,
  getIncidentById,
  updateIncident,
} from "../../modules/IncidentManager";
import { getAllUsers } from "../../modules/userManager";
import DBRAForm from "../DBRA/DBRAForm";
import "./Incident.css";


const IncidentForm = ({user}) => {
  const [incident, setIncident] = useState({});
  const [users, setUsers] = useState([]);
  const [facilities, setFacilities] = useState([]);
  const history = useHistory();
  const { id } = useParams();

  const [confirmed, setConfirmed] = useState({});
  const [reportable, setReportable] = useState({});

  const ShowA = () => {setConfirmed(true); setDbraForm(true)}
  const ShowB = () => {setConfirmed(false); setDbraForm(false)}
  const ShowC = () => {setConfirmed(null); setDbraForm(false)}

  const ShowT = () => {setReportable(true); setDbraForm(false)}
  const ShowF = () => {setReportable(false); setDbraForm(false)}
  const ShowN = () => {setReportable(null); setDbraForm(false)}

  useEffect(() => {
    if (id) {
      getIncidentById(id).then((i) => {
        setIncident(i);
        setConfirmed(i.confirmed);
        setReportable(i.reportable);
      });
    }
    getAllUsers().then((u) => {
      setUsers(u);
    });
    getAllFacilities().then((f) => {
      setFacilities(f);
    });
  }, []);

  const handleControlledInputChange = (event) => {
    const newIncident = { ...incident };
    newIncident[event.target.id] = event.target.value;
    setIncident(newIncident);
  };

  const handleClickSaveIncident = () => {
    if (incident.title === undefined || incident.description === undefined) {
      window.alert("Please complete the form");
    } else if (id) {
      updateIncident({
        id: id,
        assignedUserId: incident.assignedUserId,
        title: incident.title,
        description: incident.description,
        dateReported: incident.dateReported,
        dateOccurred: incident.dateOccurred,
        facilityId: incident.facilityId,
        confirmed: confirmed,
        reportable: reportable,
      }).then((p) => history.push(`/incident/detail/${id}`));
    } else {
      const newIncident = {
        assignedUserId: user.id,
        title: incident.title,
        description: incident.description,
        dateReported: incident.dateReported,
        dateOccurred: incident.dateOccurred,
        facilityId: incident.facilityId,
        confirmed: confirmed,
        reportable: reportable,
      };
      addIncident(newIncident).then((i) => history.push(`/incident`));
    }
  };

  //eventlistener to show dbraform
  const [dbraForm, setDbraForm] = useState(false);
  const style = dbraForm ? {display: 'block'} : {display: 'none'}


  // let filterUser = users.filter(us => us.id === user.id)
  console.log(confirmed, reportable, 'con')

  return (
    <>
    <link
              href="https://cdn.jsdelivr.net/css-toggle-switch/latest/toggle-switch.css"
              rel="stylesheet"
            />
        <form className="IncidentForm container">
          <h2 className="IncidentForm__title post_header">
            {!id ? "Create a New Incident" : "Update Incident"}
          </h2>
          <fieldset>
            <div className="form-group">
              <label htmlFor="title">Title</label>
              <input
                type="text"
                id="title"
                required
                autoFocus
                className="form-control"
                placeholder="Required"
                value={incident.title}
                onChange={handleControlledInputChange}
              />
            </div>
          </fieldset>
          <fieldset>
            <div className="form-group">
              <label htmlFor="description">Description</label>
              <textarea
                type="text"
                id="description"
                required
                autoFocus
                className="form-control"
                placeholder="Required"
                rows="10" columns="5"
                value={incident.description}
                onChange={handleControlledInputChange}
              />
            </div>
          </fieldset>
          <fieldset>     
          {user.userTypeId === 1 && id ? <>       
            <div className="form-group">
              
             
                <label htmlFor="assignedUserId">Investigator</label>
              <select
                value={incident.assignedUserId}
                name="assignedUserId"
                id="assignedUserId"
                className="form-control"
                onChange={handleControlledInputChange}
              >
                <option value="0">Select an Investigator</option>
                {users.map((u) => (
                  <option key={u.id} value={u.id}>
                    {u.fullName}
                  </option>
                ))}
              </select>
            </div></> 
            : 
            ""}
          </fieldset>
          <fieldset>
            <div className="form-group">
              <label htmlFor="facilityId">Facility</label>
              <select
                value={incident.facilityId}
                name="facilityId"
                id="facilityId"
                className="form-control"
                onChange={handleControlledInputChange}
              >
                <option value="0">Select a Facility</option>
                {facilities.map((f) => (
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
              <input
                type="datetime-local"
                id="dateReported"
                required
                autoFocus
                className="form-control"
                placeholder="Enter a date"
                value={incident.dateReported}
                onChange={handleControlledInputChange}
              />
            </div>
          </fieldset>
          <fieldset>
            <div className="form-group">
              <label htmlFor="dateOccurred">Date Occurred</label>
              <input
                type="datetime-local"
                id="dateOccurred"
                required
                autoFocus
                className="form-control"
                placeholder="Enter a date"
                value={incident.dateOccurred}
                onChange={handleControlledInputChange}
              />
            </div>
          </fieldset>
            <label>Confirmed?</label>
            <div className="switch-toggle switch-3 switch-candy Link">
              <input id="on" name="state-d" type="radio" value="true" onClick={ShowA} checked={confirmed === true ? true : false}/>
              <label htmlFor="on">YES</label>

              <input
                id="na"
                name="state-d"
                type="radio"
                value="null"
                onClick={ShowC}
                checked={confirmed === null ? true : false}/>

              <label htmlFor="na">Undetermined</label>

              <input id="off" name="state-d" type="radio" value="false" onClick={ShowB} checked={confirmed === false ? true : false}/>
              <label htmlFor="off">NO</label>

              <a></a>
            </div>
          {confirmed === true && !incident.id ? "Create the incident before completing the assessment" : ""}
          {incident.id && incident.confirmed === true || incident.id && confirmed === true ? <> 
          <div style={style}>
          <DBRAForm incident={incident} />
          </div>
          </> : ""}
          <fieldset>
          <div>

            <label>Reportable?</label>
            <div className="switch-toggle switch-3 switch-candy Link">
              <input id="Ron" name="state-dd" type="radio" onClick={ShowT} checked={reportable === true ? "checked" : false}/>
              <label htmlFor="Ron">YES</label>

              <input
                id="Rna"
                name="state-dd"
                type="radio"
                onClick={ShowN}
        
                checked={reportable === null ? "checked" : false}
              />
              <label htmlFor="Rna">Undetermined</label>

              <input id="Roff" name="state-dd" type="radio" onClick={ShowF} checked={reportable === false ? "checked" : false}/>
              <label htmlFor="Roff">NO</label>

              <a></a>
            </div>
          </div>
          </fieldset>
          <div className="buttons">
            {id ? <>
              <button
              className="Link"
              onClick={(event) => {
                event.preventDefault();
                handleClickSaveIncident();
              }}
            >
              Update Incident
            </button>
              <button className="Link" onClick={() => history.push(`/incident/detail/${id}`)}>
                Cancel
              </button>
            </> : 
            <>
            <button
              className="Link"
              onClick={(event) => {
                event.preventDefault();
                handleClickSaveIncident();
              }}
            >
              Save Incident
            </button>
            </>}
          </div>
        </form>
    </>
  );
};

export default IncidentForm;
