import React, { useEffect, useState } from "react";
import { useHistory, useParams, render } from "react-router-dom";
import { getDBRAById, addDBRA, updateDBRA } from "../../modules/dbraManager";
import { getAllCircumstances } from "../../modules/circumstanceManager";
import { getAllControls } from "../../modules/controlsManager";
import { getAllExceptions } from "../../modules/exceptionManager";
import { getAllInformation } from "../../modules/informationManager";
import { getAllMethods } from "../../modules/methodManager";
import { getAllRecipients } from "../../modules/recipientManager";
import { getAllDispositions } from "../../modules/dispositionManager";


const DBRAForm = ({incident}) => {
    const [ dbra, setDBRA ] = useState({});
    const [ circumstances, setCircumstances ] = useState([]);
    const [ controls, setControls ] = useState([]);
    const [ exceptions, setExceptions ] = useState([]);
    const [ information, setInformation ] = useState([]);
    const [ methods, setMethods ] = useState([]);
    const [ recipients, setRecipients ] = useState([]);
    const [ dispositions, setDispositions ] = useState([]);

    //setting DBRA to fill out the new instance
    const [ dbraMethod, setDbraMethod ] = useState(null)
    const [ dbraRecipient, setDbraRecipient ] = useState(null)
    const [ dbraCircumstance, setDbraCircumstance ] = useState(null)
    const [ dbraControl, setDbraControl ] = useState(null)
    const [ dbraException, setDbraException ] = useState({})
    const [ dbraInformation, setDbraInformation ] = useState(null)
    const [ dbraDisposition, setDbraDisposition ] = useState(null)

    //method to call set to set data
    
    const DBRAMethods = (methodId) => setDbraMethod(methodId)
    const DBRARecipients = (recipientId) => setDbraRecipient(recipientId)
    const DBRACircumstances = (circumstanceId) => setDbraCircumstance(circumstanceId)
    const DBRAControls = (controlId) => setDbraControl(controlId)
    const DBRAExceptions = (exceptionId) => setDbraException(exceptionId)
    const DBRAInformations = (informationId) => setDbraInformation(informationId)
    const DBRADispositions = (dispositionId) => setDbraDisposition(dispositionId)
    
console.log(dbraMethod, dbraRecipient)
    const history = useHistory();
    
    //getting all the data to create the options for forms
    useEffect(() => {
        getAllCircumstances().then(c => {setCircumstances(c)});
        getAllControls().then(co => {setControls(co)})
        getAllDispositions().then(d => setDispositions(d))
        getAllExceptions().then(e => {setExceptions(e)})
        getAllInformation().then(i => {setInformation(i)})
        getAllMethods().then(m => setMethods(m));
        getAllRecipients().then(r => setRecipients(r))
    }, [])

    const handleControlledInputChange = (event) => {
        const newDBRA = { ...dbra };
        newDBRA[event.target.id] = event.target.value;  
        setDBRA(newDBRA);
      };

      
    const handleClickSaveDBRA = () => {
        // if (dbra.method === undefined || incident.description === undefined) {
        //   window.alert("Please complete the form");
        // } else if (id) {
        //   updateIncident({
        //     id: id,
        //     assignedUserId: incident.assignedUserId,
        //     title: incident.title,
        //     description: incident.description,
        //     // dateCreated: new Date(Date.now()).toISOString(),
        //     dateReported: incident.dateReported,
        //     dateOccurred: incident.dateOccurred,
        //     facilityId: incident.facilityId,
        //     confirmed: confirmed,
        //     reportable: reportable,
        //   }).then((p) => history.push(`/incident/detail/${id}`));
        // } else {
          const newDBRA = {
            exceptionId: dbraException,
            methodId: dbraMethod,
            recipientId: dbraRecipient,
            circumstanceId: dbraCircumstance,
            dispositionId: dbraDisposition,
            incidentId: incident.id
          };
          addDBRA(newDBRA).then((d) => history.push(`incident/detail/${incident.id}`));
        }
    //   };

    return (
        <>
        <form className="DBRAForm">
          <h2 className="DBRAForm__title post_header">
            Complete DBRA
          </h2>
          <fieldset>
            <div className="form-group">
              <label htmlFor="exceptionId">Exception</label>
                {exceptions.map((ex) => (
                    <div>
                  <input type="radio" id={ex.id} name="drone" value={dbra.exceptionId} onChange={(event) => {DBRAExceptions(ex.id);}}/>
                  <label htmlFor={ex.id}>{ex.exception}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          {dbraException === 1 || dbraException === 2 || dbraException === 3 || dbraException === 4 ? "Meets exception... submit form. "
          : ""
        }
        {dbraException === 5 ? <>
        <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="methodId">Method of Disclosure</label>
                {methods.map((me) => (
                    <div>
                  <input type="radio" id={me.id} name="drone" value={dbra.methodId} onChange={(event) => {DBRAMethods(me.id);}}/>
                  <label htmlFor={me.id}>{me.methodType}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="recipientId">Recipient</label>
                {recipients.map((re) => (
                    <div>
                  <input type="radio" id={re.id} name="drone" value={dbra.recipientId} onChange={(event) => {DBRARecipients(re.id);}}/>
                  <label htmlFor={re.id}>{re.recipientType}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="circumstanceId">Circumstances of Release</label>
                {circumstances.map((ci) => (
                    <div>
                  <input type="radio" id={ci.id} name="drone" value={dbra.circumstanceId} onChange={(event) => {DBRACircumstances(ci.id);}}/>
                  <label htmlFor={ci.id}>{ci.circumstances}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="dispositionId">Disposition of Information</label>
                {dispositions.map((di) => (
                    <div>
                  <input type="radio" id={di.id} name="drone" value={dbra.dispositionId} onChange={(event) => {DBRADispositions(di.id);}}/>
                  <label htmlFor={di.id}>{di.disposition}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          {/* <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="methodId">Method</label>
                {methods.map((me) => (
                    <div>
                  <input type="radio" id={me.id} name="drone" value={dbra.methodId} onChange={(event) => {DBRAMethods(me.id);}}/>
                  <label htmlFor={me.id}>{me.methodType}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form>
          <form className="DBRAForm">
          <fieldset>
            <div className="form-group">
              <label htmlFor="methodId">Method</label>
                {methods.map((me) => (
                    <div>
                  <input type="radio" id={me.id} name="drone" value={dbra.methodId} onChange={(event) => {DBRAMethods(me.id);}}/>
                  <label htmlFor={me.id}>{me.methodType}</label>
                  </div>
                ))}
            </div>
          </fieldset>
          </form> */} </> : ""}
          <form>
          <div className="buttons">
            <button
              className="pfbtns"
              onClick={(event) => {
                event.preventDefault();
                handleClickSaveDBRA();
              }}
            >
              Submit
            </button>{" "}
            {/* {id ? (
              <button className="pfbtns" onClick={() => history.goBack()}>
                Cancel
              </button>
            ) : (
              ""
            )} */}
          </div>
        </form>
        </>
    )
}

export default DBRAForm;