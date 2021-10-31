import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/patient';

export const addPatient = (patient) => {
    return getToken().then((token) => {
      return fetch(baseUrl, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json"
        },
        body: JSON.stringify(patient)
      }).then(resp => resp.json())
  });
  }

  export const deletePatient = (id) => {
    return getToken().then((token) => {
      return fetch(`${baseUrl}/${id}`, {
        method: "DELETE",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json"
        }
      });
    });
  };