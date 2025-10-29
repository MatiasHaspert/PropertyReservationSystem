import axios from "axios";

// Luego definir una variable de entorno para la URL base 
const API_URL = "https://localhost:7099/api/Property";

export const getProperties = async () => {
    const response = await axios.get(API_URL);
    return response.data;
}

export const getProperty = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
}

// Desarrollar endpoint en el backend para esto
export const getPropertiesByUser = async (userId) => {
  const response = await axios.get(`${API_URL}/user/${userId}`);
  return response.data;
}

export const getPropertyDetails = async (id) => {
    const response = await axios.get(`${API_URL}/details/${id}`);
    return response.data;
}

export const createProperty = async (propertyData) => {
    const response = await axios.post(API_URL, propertyData);
    return response.data;
}

export const updateProperty = async (id, propertyData) => {
    const response = await axios.put(`${API_URL}/${id}`, propertyData);
    return response.data;
}

export const deleteProperty = async (id) => {
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data;
}

export const getPropertyCalendar = async (id) => {
    const response = await axios.get(`${API_URL}/${id}/calendar`)
    return response.data;
}


