import React, { useEffect, useState } from "react";
import { getAllDBRAs } from "../../modules/dbraManager.js";
import DBRA from "./DBRA.js";
import { useParams } from "react-router-dom";


const DBRAList = () => {
  const [ DBRAs, setDBRAs] = useState([]);
  const {id} = useParams()

  const getDBRAs = () => {
    getAllDBRAs().then(DBRAs => setDBRAs(DBRAs));
  };

  useEffect(() => {
    getDBRAs()
  }, []);

  const findDBRA = DBRAs.filter(d => d.incidentId === parseInt(id))
  console.log('find', findDBRA)
  return (
    <div className="container">
      {findDBRA.length === 0 ? 'DBRA not Completed' : findDBRA.slice(0,1).map((db) => (
          <DBRA db={db} key={db.id} />
        )) }
      
    </div>
  );
};

export default DBRAList;