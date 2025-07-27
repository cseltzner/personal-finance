import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoute } from "./components/ProtectedRoute";
import RegisterPage from "./pages/Auth/register/RegisterPage";
import { AuthProvider } from "./context/AuthContext";
import LoginPage from "./pages/Auth/login/LoginPage";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />, // Replace with actual login component
  },
  {
    path: "/",
    element: (
      <AuthProvider>
        <ProtectedRoute>
          <div>Home Page</div>
        </ProtectedRoute>
      </AuthProvider>
    ),
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
]);
