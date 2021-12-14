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
import { addNotes } from "../../modules/notesManager";
import "./DBRA.css";

const DBRAForm = ({ incident }) => {
  const [dbra, setDBRA] = useState({});
  const [circumstances, setCircumstances] = useState([]);
  const [controls, setControls] = useState([]);
  const [exceptions, setExceptions] = useState([]);
  const [information, setInformation] = useState([]);
  const [methods, setMethods] = useState([]);
  const [recipients, setRecipients] = useState([]);
  const [dispositions, setDispositions] = useState([]);
  const [ riskValue, setRiskValue ] = useState([])
  var values = 0


  //eventlistener to show dbraform
  const [dbraForm, setDbraForm] = useState(true);
  const style = dbraForm ? {display: 'block'} : {display: 'none'}
  const hideDbraForm = () => setDbraForm(false)

  //calculate risk value
  const addRiskValue = (value) => {
      let addRisk = riskValue
      addRisk.push(value)
      setRiskValue(addRisk)
      
  }

  //to edit:
  const history = useHistory();
  const { dbraId } = useParams();

  //setting DBRA to fill out the new instance
  const [dbraMethod, setDbraMethod] = useState(null);
  const [dbraRecipient, setDbraRecipient] = useState(null);
  const [dbraCircumstance, setDbraCircumstance] = useState(null);
  const [dbraControl, setDbraControl] = useState([]);
  const [dbraException, setDbraException] = useState({});
  const [dbraInformation, setDbraInformation] = useState([]);
  const [dbraDisposition, setDbraDisposition] = useState(null);

  //method to call set to set data

  const DBRAMethods = (methodId) => setDbraMethod(methodId);
  const DBRARecipients = (recipientId) => setDbraRecipient(recipientId);
  const DBRACircumstances = (circumstanceId) => setDbraCircumstance(circumstanceId);
  const DBRAControls = (controlId) => setDbraControl(controlId);
  const DBRAExceptions = (exceptionId) => setDbraException(exceptionId);
  const DBRAInformations = (informationId) => setDbraInformation(informationId);
  const DBRADispositions = (dispositionId) => setDbraDisposition(dispositionId);

  console.log(dbra, dbraException, dbraMethod, dbraRecipient, dbraInformation, "sos");

  //getting all the data to create the options for forms
  useEffect(() => {
    if(dbraId)
    {getDBRAById(dbraId).then((res) => { 
        setDBRA(res)
    })}
    getAllCircumstances().then((c) => {
      setCircumstances(c);
    });
    getAllControls().then((co) => {
      setControls(co);
    });
    getAllDispositions().then((d) => setDispositions(d));
    getAllExceptions().then((e) => {
      setExceptions(e);
    });
    getAllInformation().then((im) => {
      setInformation(im);
    });
    getAllMethods().then((m) => setMethods(m));
    getAllRecipients().then((r) => setRecipients(r));
  }, []);

  console.log(dbra)

  const handleControlledInputChange = (event) => {
    const newDBRA = { ...dbra };
    newDBRA[event.target.id] = event.target.value;
    setDBRA(newDBRA);
  };

  const handleClickSaveDBRA = () => {
    if (dbra.exception === null) {
      window.alert("Please complete the form");
    } else if (dbraId) {
      const update = {
        id: dbraId,
        exceptionId: dbraException,
        methodId: dbraMethod,
        recipientId: dbraRecipient,
        circumstanceId: dbraCircumstance,
        dispositionId: dbraDisposition,
        informationIds: [],
        controlIds: []
      };
      for (const di of dbraInformation) {
        update.informationIds.push(di);
      }
      for (const dc of dbraControl) {
        update.controlIds.push(dc);
      }      
      console.log(update, "update")
      updateDBRA(update).then((p) =>
        history.goBack()
      );
    } else {
      const newDBRA = {
        exceptionId: dbraException,
        methodId: dbraMethod,
        recipientId: dbraRecipient,
        circumstanceId: dbraCircumstance,
        dispositionId: dbraDisposition,
        incidentId: incident.id,
        informationIds: [],
        controlIds: []
      };
      for (const di of dbraInformation) {
        newDBRA.informationIds.push(di);
      }
      for (const dc of dbraControl) {
        newDBRA.controlIds.push(dc);
      }
      addDBRA(newDBRA);
    }
  };


  for (var i=riskValue.length; i--;) {
    values += riskValue[i];
  }


  return (
    <>
    <div className="container">
    <progress id="file" max="25" value={values}/>
    <div className="dbraForm" style={style}>
      <form className="DBRAForm">
        <h2 className="DBRAForm__title post_header">Complete DBRA</h2>
        <fieldset>  
          <div className="form-group">
            <label htmlFor="exceptionId">Exception</label>
            {exceptions.map((ex) => (
              <div>
                <input
                  type="radio"
                  id="exId"
                  name="hello"
                  value={dbra.exceptionId}
                  defaultChecked={dbraException === ex.id ? true : false}
                  onChange={(event) => {
                    DBRAExceptions(ex.id);
                  }}
                />
                <label htmlFor={ex.id}>{ex.exception}</label>
              </div>
            ))}
          </div>
        </fieldset>
      </form>
      {dbraException === 1 || dbraException === 2 || dbraException === 3 || dbraException === 4 ? "Meets exception... submit form. " : ""}
      {dbraException === 5 ? (
        <>
          <form className="DBRAForm">
            <fieldset>
              <div className="form-group">
                <label htmlFor="methodId">Method of Disclosure</label>
                {methods.map((me) => (
                  <div>
                    <input
                      type="radio"
                      id={me.id}
                      name="drone"
                      value={dbra.methodId}
                      defaultChecked={dbraMethod === me.id ? true : false}
                      onChange={(event) => {
                          event.preventDefault()
                        DBRAMethods(me.id);
                        addRiskValue(me.methodValue)
                      }}
                    />
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
                    <input
                      type="radio"
                      id={re.id}
                      name="drone"
                      value={dbra.recipientId}
                      defaultChecked={dbraRecipient === re.id ? true : false}
                      onChange={(event) => {
                        DBRARecipients(re.id);
                        addRiskValue(re.recipientValue)
                      }}
                    />
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
                    <input
                      type="radio"
                      id={ci.id}
                      name="drone"
                      value={dbra.circumstanceId}
                      defaultChecked={dbraCircumstance === ci.id ? true : false}
                      onChange={(event) => {
                        DBRACircumstances(ci.id);
                        addRiskValue(ci.circumstanceValue)
                      }}
                    />
                    <label htmlFor={ci.id}>{ci.circumstances}</label>
                  </div>
                ))}
              </div>
            </fieldset>
          </form>
          <form className="DBRAForm">
            <fieldset>
              <div className="form-group">
                <label htmlFor="dispositionId">
                  Disposition of Information
                </label>
                {dispositions.map((di) => (
                  <div>
                    <input
                      type="radio"
                      id={di.id}
                      name="drone"
                      value={dbra.dispositionId}
                      defaultChecked={dbraDisposition === di.id ? true : false}
                      onChange={(event) => {
                        DBRADispositions(di.id);
                        addRiskValue(di.dispositionValue)
                      }}
                    />
                    <label htmlFor={di.id}>{di.disposition}</label>
                  </div>
                ))}
              </div>
            </fieldset>
          </form>
          <form className="DBRAForm">
            <fieldset>
              <div className="form-group">
                <label htmlFor="informationId">Information Breached</label>
                {information.map((inf) => (
                  <div>
                    <input
                      type="checkbox"
                      id={inf.id}
                      name="drone"
                      value={dbra.informationIds}
                      defaultChecked={dbraInformation === inf.id ? true : false}
                      onChange={(e) => {
                        !dbraInformation.includes(e.target.id) ? 
                          <>
                            {setDbraInformation([
                              ...dbraInformation,
                              e.target.id
                            ])}
                            {addRiskValue(inf.informationValue)}
                          </>
                         : (
                          <>
                            {dbraInformation.splice(
                              dbraInformation.indexOf(e.target.id),
                              1
                            )}
                          </>
                        );
                      }}
                    />
                    <label htmlFor={inf.id}>{inf.informationType}</label>
                  </div>
                ))}
              </div>
            </fieldset>
          </form>
          <form className="DBRAForm">
            <fieldset>
              <div className="form-group">
                <label htmlFor="controlsId">Controls Breached</label>
                {controls.map((con) => (
                  <div>
                    <input
                      type="checkbox"
                      id={con.id}
                      name="drone"
                      value={dbra.controlsId}
                      onChange={(e) => {
                        !dbraControl.includes(e.target.id) ? 
                          <>{setDbraControl([...dbraControl, e.target.id])}
                          {addRiskValue(con.controlValue)}</>
                         : (
                          <>
                            {dbraControl.splice(
                              dbraControl.indexOf(e.target.id),
                              1
                            )}
                          </>
                        );
                      }}
                    />
                    <label htmlFor={con.id}> {con.control}</label>
                  </div>
                ))}
              </div>
            </fieldset>
          </form>
        </>
      ) : (
        ""
      )}
      <form>
        <div className="buttons">
          <button
            id="dbraSubmit"
            className="Link"
            onClick={(event) => {
              event.preventDefault();
              handleClickSaveDBRA();
              hideDbraForm();
            }}
          >
            Submit
          </button>{" "}
          {dbraId ? (
              <button className="Link" onClick={() => history.push(`/DBRA/detail/${dbraId}`)}>
                Cancel
              </button>
            ) : (
              ""
            )}
        </div>
      </form>
      </div>
      <br/>
      {dbraException !== 5 ? <h3>Exception met - 0 to Low Risk, No report necessary</h3> : <>{values >= 15 ? <h3>HIGH RISK: REPORTABLE REQUIRED</h3> : <h3>LOW - MEDIUM RISK: INVESTIGATOR DISCRETION</h3>}</>}
      </div>
    </>
  );
};

export default DBRAForm;
