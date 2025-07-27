import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoute } from "./components/ProtectedRoute";
import RegisterPage from "./pages/Auth/register/RegisterPage";
import { AuthProvider } from "./context/AuthContext";

export const router = createBrowserRouter([
  {
    path: "/login",
    element: <div>Login Page</div>, // Replace with actual login component
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
