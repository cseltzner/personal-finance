import axios from "axios";

export const api = {
    register: async (data: {
        username: string;
        email: string;
        password: string;
        firstName: string;
        lastName: string;
    }) => {
        try {
            const response = await axios.post('/auth/register', data);
            return response.data;
        } catch (error) {
            console.error("Registration error:", error);
            throw error;
        }
    }
}