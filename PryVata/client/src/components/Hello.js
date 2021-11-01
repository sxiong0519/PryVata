import React, { useEffect, useState } from "react";
import Incident from "./Incident/Incident";
import { getAllIncidents } from "../modules/IncidentManager";
import IncidentCard from "./Incident/IncidentCard";
import './HomePage.css'
import { PieChart, Pie} from 'recharts';
import { Link } from "react-router-dom";

export default function Hello() {
  const [ incidents, setIncidents] = useState([]);


  const getIncidents = () => {
    getAllIncidents().then(incidents => setIncidents(incidents));
  };

  useEffect(() => {
    getIncidents()
  }, []);

  
//most recently added
  const incidentMap = incidents.map((incident) => <Incident incident={incident} key={incident.id} />);
  
  //due soon
  for (const i of incidents)
  {
    const date = new Date(i.dateReported)
    date.setDate(date.getDate() + 30)
    i.dueDate = new Date(date).toLocaleDateString()
    if(i.confirmed === true && i.reportable === true)
    {
      i.pieValue = 1;
    } else if (i.confirmed === null)
    {
      i.pieValue = 2;
    } else if (i.confirmed === true && i.reportable === null || i.reportable === false)
    {
      i.pieValue = 3;
    } else
    {
      i.pieValue = 4;
    }
  }

  const sortedIncidentsByDueDate = incidents.sort((b, a) => {
    return (
      b.dueDate > a.dueDate
    );
  })

  const filterSortedIncidents = sortedIncidentsByDueDate.filter(i => i.confirmed === null)


  const mapSortedIncident = filterSortedIncidents.reverse().map((incident) => {
   
    return <IncidentCard incident={incident} key={incident.id} />

});

//pie chart data

const totalConfirmedReportable = sortedIncidentsByDueDate.filter(i => i.pieValue === 1);
const totalUndetermined = sortedIncidentsByDueDate.filter(i => i.pieValue === 2);
const totalConfirmedNotReportable = sortedIncidentsByDueDate.filter(i => i.pieValue === 3);
const totalNotConfirmed = sortedIncidentsByDueDate.filter(i => i.pieValue === 4);


var data = [
  {name: 'Confirmed and Reportable', value: totalConfirmedReportable.length},
  {name: "Undetermined", value: totalUndetermined.length},
  {name: "Confirmed, Not Reportable", value: totalConfirmedNotReportable.length},
  {name: "Not confirmed", value: totalNotConfirmed.length},
];


console.log(mapSortedIncident, 'map')
console.log(sortedIncidentsByDueDate, totalConfirmedReportable.length, totalUndetermined, totalConfirmedNotReportable, totalNotConfirmed)
    return (
    <>

    <div className="homePage">
    <div className="mostRecent">
    <h3>Most Recent Allegations Received</h3>
      <div className="recent">{incidentMap.slice(0,3)}</div>
      </div>
      <div className="dueDate">
      <h3>Due soon</h3>
      <div className="due">{mapSortedIncident.length > 0 ? <> {mapSortedIncident.slice(0,3)} </> : 'All incidents confirmed'}</div>
      </div>
      </div>
      <div className="pieContainer">
      
      <PieChart width={850} height={600}>
          <Pie data={data} dataKey="value" outerRadius={200} label={(entry) => entry.name} fill="#3b4e50" />
        </PieChart>
        <div className="data">
         Confirmed and Reportable: {totalConfirmedReportable.length} <br/>
         Undetermined: {totalUndetermined.length} <br/>
         Confirmed, Not Reportable: {totalConfirmedNotReportable.length} <br/>
         Not confirmed: {totalNotConfirmed.length} <br/>
        </div>
      </div>
     
    </>
  );
}
