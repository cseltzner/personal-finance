import { createBrowserRouter } from "react-router-dom";
import { ProtectedRoute } from "./components/ProtectedRoute";
import RegisterPage from "./pages/Auth/register/RegisterPage";
import { AuthProvider } from "./context/AuthContext";
import LoginPage from "./pages/Auth/login/LoginPage";
import AppLayout from "./layout/AppLayout";
import AccountPage from "./pages/accounts/AccountPage";

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
          <AppLayout />
        </ProtectedRoute>
      </AuthProvider>
    ),
    children: [
      {
        index: true,
        element: <div>Dashboard</div>,
      },
      {
        path: "/accounts",
        element: <AccountPage />,
      },
      {
        path: "/transactions",
        element: <div>Transactions</div>,
      }
    ]
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
]);
