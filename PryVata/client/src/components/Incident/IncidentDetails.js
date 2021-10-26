import React, { useEffect, useState } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
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
        <Link to={`/incident/edit/${incident.id}`}>Edit</Link>
    </div>
    </>
)

}

export default IncidentDetails;