import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { addPatient } from "../../modules/patientManager";
import './Patient.css'



const PatientForm = ({incident}) => {
    const [ patient, setPatient ] = useState({})
    const [ patientForm, setPatientForm ] = useState(true)
    
    const history = useHistory();

    const handleControlledInputChange = (event) => {
        const newPatient = { ...patient };
        newPatient[event.target.id] = event.target.value;
        setPatient(newPatient);
      };

    const handleClickSavePatient = () => {
        if (patient.patientNumber === undefined) {
          window.alert("Please complete the form");
        }
        else {
          const newPatient = {
            patientNumber: patient.patientNumber,
            firstName: patient.firstName,
            lastName: patient.lastName,
            incidentId: incident.id
          };
          addPatient(newPatient).then(() => history.go(0));
        }
      };
    
      const showPatientForm = () => setPatientForm(false)
      const style = patientForm ? {display: 'block'} : {display: 'none'}

    return (
        <>
         <form style={style} className="patientForm">
            <h2 className="patientForm__title patient_header">Add Patient(s) affected</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="patientNumber">Patient Number</label>
                    <input type="text" id="patientNumber" required autoFocus className="form-control patientForm" placeholder="Required" value={patient.patientNumber} onChange={handleControlledInputChange} />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="firstName">First Name</label>
                    <input type="text" id="firstName" required autoFocus className="form-control patientForm" placeholder="Required" value={patient.firstName} onChange={handleControlledInputChange} />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="lastName">Last Name</label>
                    <input type="text" id="lastName" required autoFocus className="form-control patientForm" placeholder="Required" value={patient.lastName} onChange={handleControlledInputChange} />
                </div>
            </fieldset>
            <div className="buttons"><button className="pfbtns" onClick={
                (event) => {
                    event.preventDefault()
                    handleClickSavePatient()
                    showPatientForm()
                }
            }>
            Save
            </button></div> 
        </form>
        </>
    )
}

export default PatientForm;