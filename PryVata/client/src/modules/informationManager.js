import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/information';

export const getAllInformation = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getInformationById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addInformation = (information) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(information)
    }).then(resp => resp.json())
});
}

export const updateInformation = (information) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${information.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(information)
    }).then(resp => getAllInformation());
  })
};

export const deleteInformation = (informationId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${informationId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};