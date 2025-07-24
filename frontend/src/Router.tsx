import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoute } from "./components/ProtectedRoute";
import RegisterPage from "./pages/Auth/RegisterPage";

export const router = createBrowserRouter([
    {
        path: "/login",
        element: <div>Login Page</div>, // Replace with actual login component
    },
    {
        path: "/",
        element: (
            <ProtectedRoute>
                <div>Home Page</div>
            </ProtectedRoute>
        )
    },
    {
        path: "/register",
        element: <RegisterPage />,
    }
])