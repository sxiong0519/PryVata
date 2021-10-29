import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/incident';

export const getAllIncidents = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}
  export const getMyIncidents = (id) => {
    return getToken().then((token) => {
      return fetch(`${baseUrl}/myIncidents`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`
        }
    }).then(resp => resp.json())
});
}

export const getIncidentById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addIncident = (incident) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(incident)
    }).then(resp => resp.json())
});
}

export const updateIncident = (incident) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${incident.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(incident)
    }).then(resp => getAllIncidents());
  })
};

export const deleteIncident = (incidentId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${incidentId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};