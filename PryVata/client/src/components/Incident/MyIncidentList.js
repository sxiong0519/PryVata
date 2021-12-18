import React, { useEffect, useState } from "react";
import { getMyIncidents } from "../../modules/IncidentManager.js";
import Incident from './Incident.js'


const MyIncidentList = () => {
  const [ incidents, setIncidents] = useState([]);

  const getIncidents = () => {
    getMyIncidents().then(incidents => setIncidents(incidents));
  };

  useEffect(() => {
    getIncidents()
  }, []);

  return (
    <div className="incidentCard container">

      <div className="incident">
      {incidents.map((incident) => (
          <Incident incident={incident} key={incident.id} />
        ))}
      </div>
    </div>
  );
};

export default MyIncidentList;