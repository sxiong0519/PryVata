import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/facility';

export const getAllFacilities = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getFacilityById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addFacility = (facility) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(facility)
    }).then(resp => resp.json())
});
}

export const updateFacility = (facility) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${facility.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(facility)
    }).then(resp => getAllFacilities());
  })
};

export const deleteFacility = (facilityId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${facilityId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};