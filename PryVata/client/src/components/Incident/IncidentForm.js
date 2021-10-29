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

const IncidentForm = () => {
  const [incident, setIncident] = useState({});
  const [users, setUsers] = useState([]);
  const [facilities, setFacilities] = useState([]);
  const history = useHistory();
  const { id } = useParams();

  const [confirmed, setConfirmed] = useState(null);
  const [reportable, setReportable] = useState(null);

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
        assignedUserId: incident.assignedUserId,
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


  return (
    <>
      <center>
        <form className="IncidentForm">
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
              <input
                type="text"
                id="description"
                required
                autoFocus
                className="form-control"
                placeholder="Required"
                value={incident.description}
                onChange={handleControlledInputChange}
              />
            </div>
          </fieldset>
          <fieldset>
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
            </div>
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
          <fieldset>
          <div>
            <link
              href="https://cdn.jsdelivr.net/css-toggle-switch/latest/toggle-switch.css"
              rel="stylesheet"
            />
            <label>Confirmed?</label>
            <div className="switch-toggle switch-3 switch-candy">
              <input id="on" name="state-d" type="radio" onClick={ShowA} />
              <label htmlFor="on">YES</label>

              <input
                id="na"
                name="state-d"
                type="radio"
                onClick={ShowC}
                checked="checked"
              />
              <label htmlFor="na">N/A</label>

              <input id="off" name="state-d" type="radio" onClick={ShowB} />
              <label htmlFor="off">NO</label>

              <a></a>
            </div>
          </div>
          </fieldset>
          {confirmed === true && !incident.id ? "Create the incident before completing the assessment" : ""}
          {incident.id && incident.confirmed === true || incident.id && confirmed === true ? <> 
          <div style={style}>
          <DBRAForm incident={incident} />
          </div>
          <fieldset>
          <div>
            <link
              href="https://cdn.jsdelivr.net/css-toggle-switch/latest/toggle-switch.css"
              rel="stylesheet"
            />
            <label>Reportable?</label>
            <div className="switch-toggle switch-3 switch-candy">
              <input id="Ron" name="state-d" type="radio" onClick={ShowT} />
              <label htmlFor="Ron">YES</label>

              <input
                id="Rna"
                name="state-d"
                type="radio"
                onClick={ShowN}
                checked="checked"
              />
              <label htmlFor="Rna">N/A</label>

              <input id="Roff" name="state-d" type="radio" onClick={ShowF} />
              <label htmlFor="Roff">NO</label>

              <a></a>
            </div>
          </div>
          </fieldset></> : ""}
          <div className="buttons">
            {id ? <>
              <button
              className="pfbtns"
              onClick={(event) => {
                event.preventDefault();
                handleClickSaveIncident();
              }}
            >
              Update Incident
            </button>
              <button className="pfbtns" onClick={() => history.goBack()}>
                Cancel
              </button>
            </> : 
            <>
            <button
              className="pfbtns"
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
      </center>
    </>
  );
};

export default IncidentForm;
