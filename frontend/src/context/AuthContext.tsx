import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

interface AuthContextType {
  isAuthenticated: boolean;
  loading: boolean;
  login: () => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    const checkAuthStatus = async () => {
        try {
            const res = await axios.get("/auth/statuscheck", {
                withCredentials: true,
            })

            setIsAuthenticated(res.status === 200);
        } catch (error) {
            console.error("Error checking authentication status:", error);
            setIsAuthenticated(false);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        checkAuthStatus();
    }, [])

    const login = () => {
        setIsAuthenticated(true);
    }

    const logout = () => {
        setIsAuthenticated(false);
    }

    return (
        <AuthContext.Provider value={{ isAuthenticated, loading, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
}