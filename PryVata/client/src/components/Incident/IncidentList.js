import React, { useEffect, useState } from "react";
import { getAllIncidents } from "../../modules/IncidentManager.js";
import Incident from './Incident.js'
import { Link } from "react-router-dom";


const IncidentList = () => {
  const [ incidents, setIncidents] = useState([]);

  const getIncidents = () => {
    getAllIncidents().then(incidents => setIncidents(incidents));
  };

  useEffect(() => {
    getIncidents()
  }, []);

  return (
    <div className="incidentCard container">
      <div>
        <Link className="Link" to="/incident/add">New Incident</Link>
      </div>
      <div className="incident">
      {incidents.map((incident) => (
          <Incident incident={incident} key={incident.id} />
        ))}
      </div>
    </div>
  );
};

export default IncidentList;