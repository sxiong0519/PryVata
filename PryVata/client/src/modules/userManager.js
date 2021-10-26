import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/user';

export const getAllUsers = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getUserById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addUser = (user) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(user)
    }).then(resp => resp.json())
});
}

export const updateUser = (user) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${user.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(user)
    }).then(resp => getAllUsers());
  })
};

export const deleteUser = (userId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${userId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};