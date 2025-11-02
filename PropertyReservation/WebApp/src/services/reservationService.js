import axios from "axios";

// Luego definir una variable de entorno para la URL base 
const API_URL = "https://localhost:7099/api/Reservation";

export const createReservation = async (reservationData) => {
    const response = await axios.post(API_URL, reservationData);
    return response.data;
}