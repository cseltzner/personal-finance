import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
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

const RegisterPage = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const [registerForm, setRegisterForm] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    emailTouched: false,
    passwordTouched: false,
    confirmPasswordTouched: false,
  });

  const [registerError, setRegisterError] = useState({
    email: "",
    password: "",
    confirmPassword: "",
  });

  const [loading, setLoading] = useState(false);
  const [registerSubmissionError, setRegisterSubmissionError] = useState("");

  const handleFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRegisterForm({ ...registerForm, [name]: value });
    setRegisterError((prev) => ({ ...prev, [name]: "" }));
    setRegisterSubmissionError("");
  };

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRegisterForm((prev) => ({ ...prev, [`${name}Touched`]: true }));

    // Validate required
    if (value.trim() === "") {
      setRegisterError((prev) => ({ ...prev, [name]: "Field is required" }));
      return;
    }

    // Validate email
    if (name === "email") {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(value)) {
        setRegisterError((prev) => ({ ...prev, email: "Invalid email format" }));
        return;
      }
    }

    // Validate password match
    if (name === "confirmPassword") {
      if (value !== registerForm.password) {
        setRegisterError((prev) => ({
          ...prev,
          confirmPassword: "Passwords do not match",
        }));
        return;
      }
    }

    // Clear error if valid
    setRegisterError((prev) => ({ ...prev, [name]: "" }));
  };

  const handleSubmit = async () => {
    const hasError = Object.values(registerError).some((error) => error !== "");
    const hasEmptyField = Object.values(registerForm).some(
      (value) => typeof value === "string" && value === ""
    );

    if (hasError || hasEmptyField) return;

    setLoading(true);
    try {
      await api.register({
        username: registerForm.email.split("@")[0], // Use email prefix as username for now
        email: registerForm.email,
        password: registerForm.password,
        firstName: "",
        lastName: "",
      });

      // Redirect to home
      navigate("/", { replace: true });
    } catch (error: any) {
      const errorMessage =
        error.response?.data || "Registration failed. Please try again.";

      setRegisterSubmissionError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      minHeight="100vh"
      bgcolor={theme.palette.background.default}
      position="relative"
      top={-32}
    >
      <Paper
        elevation={1}
        sx={{
          p: 12,
          width: "100%",
          maxWidth: 500,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Box display="flex" gap={2} alignItems="center" mb={1}>
          <Paid color="primary" fontSize="large" />
          <Typography
            variant="h4"
            fontFamily="serif"
            fontWeight="bold"
            color="text.primary"
          >
            CMoney
          </Typography>
        </Box>
        <Typography variant="subtitle1" color="text.secondary" mb={2}>
          Sign up to get started
        </Typography>
        <Box
          marginTop={2}
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
            value={registerForm.email}
            onChange={handleFormChange}
            onBlur={handleBlur}
            fullWidth
            margin="dense"
            autoComplete="email"
            error={!!registerError.email}
            helperText={registerError.email || " "}
          />
          <TextField
            id="password"
            name="password"
            label="Password"
            placeholder="Enter your password"
            type="password"
            value={registerForm.password}
            onChange={handleFormChange}
            onBlur={handleBlur}
            fullWidth
            margin="dense"
            autoComplete="new-password"
            error={!!registerError.password}
            helperText={registerError.password || " "}
          />
          <TextField
            id="confirmPassword"
            name="confirmPassword"
            label="Confirm Password"
            placeholder="Re-enter your password"
            type="password"
            value={registerForm.confirmPassword}
            onChange={handleFormChange}
            onBlur={handleBlur}
            fullWidth
            margin="dense"
            autoComplete="new-password"
            error={!!registerError.confirmPassword}
            helperText={registerError.confirmPassword || " "}
          />
          {registerSubmissionError && (
            <Alert severity="error" sx={{ mt: 2, mb: 1 }}>
              {registerSubmissionError}
            </Alert>
          )}
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            size="large"
            sx={{ mt: 4 }}
            disabled={
              loading ||
              Object.values(registerError).some((error) => error !== "")
            }
            startIcon={loading ? <CircularProgress size={20} /> : null}
          >
            {loading ? "Signing Up..." : "Sign Up"}
          </Button>
        </Box>
        <Typography variant="body2" sx={{ mt: 6 }}>
          Already have an account?{" "}
          <Link
            to="/login"
            style={{
              color: theme.palette.primary.main,
              textDecoration: "none",
            }}
          >
            Log in
          </Link>
        </Typography>
      </Paper>
    </Box>
  );
};

export default RegisterPage;