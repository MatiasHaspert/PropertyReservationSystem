import axios from "axios";

const API_URL = "https://localhost:44395/api/Property";

export const getProperties = () => axios.get(API_URL);

export const getProperty = (id) => axios.get(`${API_URL}/${id}`);

export const getPropertiesByUser = async (userId) => {
    const response = await axios.get(`${API_URL}/user/${userId}`);
    return response.data;
}

export const getPropertyDetails = (id) => axios.get(`${API_URL}/details/${id}`);

export const createProperty = (propertyData) => axios.post(API_URL, propertyData);

export const updateProperty = (id, propertyData) => axios.put(`${API_URL}/${id}`, propertyData);

export const deleteProperty = (id) => axios.delete(`${API_URL}/${id}`);
