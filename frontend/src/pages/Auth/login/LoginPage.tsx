import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { api } from "./api";
import { Paid } from "@mui/icons-material";
import {
  Box,
  Button,
  TextField,
  Typography,
  Paper,
  CircularProgress,
  Alert,
  useTheme,
} from "@mui/material";

const LoginPage = () => {
  const navigate = useNavigate();
  const theme = useTheme();

  const [loginForm, setLoginForm] = useState({
    email: "",
    password: "",
  });

  const [loginError, setLoginError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setLoginForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async () => {
    if (!loginForm.email || !loginForm.password) {
      setLoginError("Email and password are required.");
      return;
    }

    setLoading(true);
    setLoginError("");

    try {
      await api.login({
        email: loginForm.email,
        password: loginForm.password,
      });

      navigate("/");
    } catch (error) {
      setLoginError("Login failed. Please check your credentials and try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh" bgcolor={theme.palette.background.default} position={"relative"} top={-32}>
      <Paper elevation={1} sx={{ p: 12, width: "100%", maxWidth: 500, display: "flex", flexDirection: "column", alignItems: "center" }}>
        <Box display="flex" gap={2} alignItems="center" mb={1}>
          <Paid color="primary" fontSize="large" />
          <Typography variant="h4" fontFamily="serif" fontWeight="bold" color="text.primary">
            CMoney
          </Typography>
        </Box>
        <Typography variant="subtitle1" color="text.secondary" mb={2}>
          Sign in to continue
        </Typography>
        <Box
          component="form"
          width="100%"
          onSubmit={(e) => {
            e.preventDefault();
            handleSubmit();
          }}
        >
          <TextField
            id="email"
            name="email"
            label="Email Address"
            placeholder="email@example.com"
            type="email"
            value={loginForm.email}
            onChange={handleFormChange}
            fullWidth
            margin="normal"
            autoComplete="email"
          />
          <TextField
            id="password"
            name="password"
            label="Password"
            placeholder="Enter your password"
            type="password"
            value={loginForm.password}
            onChange={handleFormChange}
            fullWidth
            margin="normal"
            autoComplete="current-password"
          />
          {loginError && (
            <Alert severity="error" sx={{ mt: 2, mb: 1 }}>
              {loginError}
            </Alert>
          )}
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            size="large"
            sx={{ mt: 8 }}
            disabled={loading}
            startIcon={loading ? <CircularProgress size={20} /> : null}
          >
            {loading ? "Signing In..." : "Sign In"}
          </Button>
        </Box>
        <Typography variant="body2" sx={{ mt: 6 }}>
          Don't have an account?{" "}
          <Link to="/register" style={{ color: theme.palette.primary.main, textDecoration: "none" }}>
            Sign up
          </Link>
        </Typography>
      </Paper>
    </Box>
  );
};

export default LoginPage;