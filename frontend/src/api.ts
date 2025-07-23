import axios from "axios";

export const apiInit = () => {
    axios.defaults.baseURL = 'http://localhost:5058/api'; // Set global base URL
    axios.defaults.withCredentials = true; // Enable sending cookies with requests
}