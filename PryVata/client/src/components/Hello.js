import React, { useEffect, useState } from "react";
import Incident from "./Incident/Incident";
import { getAllIncidents } from "../modules/IncidentManager";
import IncidentCard from "./Incident/IncidentCard";
import './HomePage.css'


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

const totalConfirmedReportable = sortedIncidentsByDueDate.filter(i => i.pieValue === 1).length;
const totalUndetermined = sortedIncidentsByDueDate.filter(i => i.pieValue === 2);
const totalConfirmedNotReportable = sortedIncidentsByDueDate.filter(i => i.pieValue === 3);
const totalNotConfirmed = sortedIncidentsByDueDate.filter(i => i.pieValue === 4);

// anychart.onDocumentReady(function() {
// var data = [
//   {x: "Confirmed and Reportable", value: totalConfirmedReportable},
//   {x: "Undetermined", value: totalUndetermined},
//   {x: "Confirmed, Not Reportable", value: totalConfirmedNotReportable},
//   {x: "Determined, no evidence to support allegation", value: totalNotConfirmed},
// ];

//  // create the chart
//  var chart = anychart.pie();

//  // set the chart title
//  chart.title("Population by Race for the United States: 2010 Census");

//  // add the data
//  chart.data(data);

//  // display the chart in the container
//  chart.container('container');
//  chart.draw();
// })

console.log(mapSortedIncident, 'map')
console.log(sortedIncidentsByDueDate, totalConfirmedReportable.length, totalUndetermined, totalConfirmedNotReportable, totalNotConfirmed)
    return (
    <>
    <div className="homePage">
    <div className="mostRecent">
    <h3>Most Recent Allegations Received</h3>
      <div className="otherLP">{incidentMap.slice(0,3)}</div>
      </div>
      <div className="dueDate">
      <h3>Due soon</h3>
      <div className="due">{mapSortedIncident.length > 0 ? <> {mapSortedIncident.slice(0,3)} </> : 'All incidents confirmed'}</div>
      </div>
      <div className="container"></div>
      </div>
     
    </>
  );
}
