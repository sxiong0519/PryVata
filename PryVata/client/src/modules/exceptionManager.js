import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/exception';

export const getAllExceptions = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}

export const getExceptionById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addException = (exception) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(exception)
    }).then(resp => resp.json())
});
}

export const updateException = (exception) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${exception.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(exception)
    }).then(resp => getAllExceptions());
  })
};

export const deleteException = (exceptionId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/delete/${exceptionId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};