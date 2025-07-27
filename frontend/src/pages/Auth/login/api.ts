import axios from "axios";

export const api = {
    login: async (data: { email: string; password: string }) => {
        try {
            const response = await axios.post('/auth/login', data);
            return response.data;
        } catch (error) {
            console.error("Login error:", error);
            throw error;
        }
    },
    logout: async () => {
        try {
            const response = await axios.post('/auth/logout');
            return response.data;
        } catch (error) {
            console.error("Logout error:", error);
            throw error;
        }
    }
}