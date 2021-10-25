import React, { useEffect, useState } from "react";
import { useParams, useHistory } from "react-router-dom";
import { getIncidentById } from "../../modules/IncidentManager";




const IncidentDetails = () => {
    const [incident, setIncident] = useState();
    const { id } = useParams();

    const history = useHistory();

    useEffect(() => {
    getIncidentById(id).then((i) => { setIncident(i)
})
}, []);

if(!incident) {
    return null;
}

return (
    <>
    <div className="container">
        Title: {incident.title}
        <br/>
        Assigned: {incident.user.fullName}
        <br/>
        Description: {incident.description}
        <br/>
    </div>
    </>
)

}

export default IncidentDetails;