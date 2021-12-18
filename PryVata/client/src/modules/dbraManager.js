import "firebase/auth";
import { getToken } from "./authManager";

const baseUrl = '/api/DBRA';

export const getAllDBRAs = () => {
  return getToken().then((token) => {  
  return fetch(baseUrl, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json())
  });
}
  export const getMyDBRAs = (id) => {
    return getToken().then((token) => {
      return fetch(`${baseUrl}/myDBRA`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`
        }
    }).then(resp => resp.json())
});
}

export const getDBRAById = (id) => {
  return getToken().then((token) => {  
    return fetch(`${baseUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.json())
});
}

export const addDBRA = (DBRA) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(DBRA)
    }).then(resp => resp.json())
});
}

export const updateDBRA = (DBRA) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/edit/${DBRA.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(DBRA)
    }).then(resp => getAllDBRAs());
  })
};

export const deleteDBRA = (DBRAId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${DBRAId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      }
    });
  });
};