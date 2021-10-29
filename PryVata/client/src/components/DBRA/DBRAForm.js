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

const DBRAForm = ({ incident }) => {
  const [dbra, setDBRA] = useState({});
  const [circumstances, setCircumstances] = useState([]);
  const [controls, setControls] = useState([]);
  const [exceptions, setExceptions] = useState([]);
  const [information, setInformation] = useState([]);
  const [methods, setMethods] = useState([]);
  const [recipients, setRecipients] = useState([]);
  const [dispositions, setDispositions] = useState([]);

  //eventlistener to show dbraform
  const [dbraForm, setDbraForm] = useState(true);
  const style = dbraForm ? {display: 'block'} : {display: 'none'}
  const hideDbraForm = () => setDbraForm(false)

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

  console.log(dbraMethod, dbraRecipient, dbraInformation);

  //getting all the data to create the options for forms
  useEffect(() => {
    if(dbraId)
    {getDBRAById(dbraId).then((res) => setDBRA(res))}
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
        controlIds: [],
      };
      for (const di of dbraInformation) {
        update.informationIds.push(di);
      }
      for (const dc of dbraControl) {
        update.controlIds.push(dc);
      }
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
        controlIds: [],
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

  return (
    <>
    <div style={style}>
      <form className="DBRAForm">
        <h2 className="DBRAForm__title post_header">Complete DBRA</h2>
        <fieldset>
          <div className="form-group">
            <label htmlFor="exceptionId">Exception</label>
            {exceptions.map((ex) => (
              <div>
                <input
                  type="radio"
                  id={ex.id}
                  name="drone"
                  value={dbra.exceptionId}
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
                      onChange={(event) => {
                        DBRAMethods(me.id);
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
                      onChange={(event) => {
                        DBRARecipients(re.id);
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
                      onChange={(event) => {
                        DBRACircumstances(ci.id);
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
                      onChange={(event) => {
                        DBRADispositions(di.id);
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
                      onChange={(e) => {
                        !dbraInformation.includes(e.target.id) ? (
                          <>
                            {setDbraInformation([
                              ...dbraInformation,
                              e.target.id,
                            ])}
                          </>
                        ) : (
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
                        !dbraControl.includes(e.target.id) ? (
                          <>{setDbraControl([...dbraControl, e.target.id])}</>
                        ) : (
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
            className="pfbtns"
            onClick={(event) => {
              event.preventDefault();
              handleClickSaveDBRA();
              hideDbraForm();
            }}
          >
            Submit
          </button>{" "}
          {dbraId ? (
              <button className="pfbtns" onClick={() => history.goBack()}>
                Cancel
              </button>
            ) : (
              ""
            )}
        </div>
      </form>
      </div>
    </>
  );
};

export default DBRAForm;
