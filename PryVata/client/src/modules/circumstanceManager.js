import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/circumstance';

export const getAllCircumstances = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getCircumstanceById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addCircumstance = (circumstance) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(circumstance)
    }).then(resp => resp.json())
});
}

export const updateCircumstance = (circumstance) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${circumstance.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(circumstance)
    }).then(resp => getAllCircumstances());
  })
};

export const deleteCircumstance = (circumstanceId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${circumstanceId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};