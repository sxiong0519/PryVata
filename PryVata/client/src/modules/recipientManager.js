import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/recipient';

export const getAllRecipients = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getRecipientById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addRecipient = (recipient) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(recipient)
    }).then(resp => resp.json())
});
}

export const updateRecipient = (recipient) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${recipient.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(recipient)
    }).then(resp => getAllRecipients());
  })
};

export const deleteRecipient = (recipientId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${recipientId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};