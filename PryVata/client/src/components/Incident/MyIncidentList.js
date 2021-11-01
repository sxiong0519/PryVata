import React, { useEffect, useState } from "react";
import { getMyIncidents } from "../../modules/IncidentManager.js";
import Incident from './Incident.js'
import { Link } from "react-router-dom";


const MyIncidentList = () => {
  const [ incidents, setIncidents] = useState([]);

  const getIncidents = () => {
    getMyIncidents().then(incidents => setIncidents(incidents));
  };

  useEffect(() => {
    getIncidents()
  }, []);

  return (
    <div className="incidentCard">
      <div>
        <Link to="/incident/add">New Incident</Link>
      </div>
      <div className="incident">
      {incidents.map((incident) => (
          <Incident incident={incident} key={incident.id} />
        ))}
      </div>
    </div>
  );
};

export default MyIncidentList;