import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/notes';

export const getAllNotes = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getNotesById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addNotes = (notes) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(notes)
    }).then(resp => resp.json())
});
}

export const updatenotes = (notes) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${notes.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(notes)
    }).then(resp => getAllNotes());
  })
};

export const deleteNotes = (notesId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${notesId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};