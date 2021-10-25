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
    <div className="container">
      {incidents.map((incident) => (
          <Incident Incident={incident} key={incident.id} />
        ))}
      
    </div>
  );
};

export default IncidentList;