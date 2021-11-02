import React, { useEffect, useState } from "react";
import { getAllDBRAs, getDBRAById } from "../../modules/dbraManager.js";
import { getIncidentById } from "../../modules/IncidentManager.js";
import DBRA from "./DBRA.js";
import { useParams, useHistory, Link } from "react-router-dom";


const DBRAList = () => {
  const [ DBRAs, setDBRAs] = useState([]);
  const [ incident, setIncident ] = useState({})
  const {id} = useParams()

  const getDBRAs = () => {
    getAllDBRAs().then(DBRAs => setDBRAs(DBRAs));
  };

  useEffect(() => {
    getDBRAs()
    getIncidentById(id)
    .then((resp) => setIncident(resp))
  }, []);

  const findDBRA = DBRAs.filter(d => d.incidentId === parseInt(id))

  return (
    <div className="container">
      {findDBRA ? findDBRA.slice(0,1).map((db) => (
          <DBRA db={db} key={db.id} />
        )) : "No DBRA assigned"}
      
    </div>
  );
};

export default DBRAList;