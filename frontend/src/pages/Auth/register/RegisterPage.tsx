import { Link, Navigate, useNavigate } from "react-router-dom";
import DollarCircle from "../../../assets/icons/DollarCircle";
import Button from "../../../components/Button";
import Input from "../../../components/forms/Input";
import { useState } from "react";
import { api } from "./api";

const RegisterPage = () => {
  const navigate = useNavigate();
  const [registerForm, setRegisterForm] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    emailTouched: false,
    passwordTouched: false,
    confirmPasswordTouched: false
  })

  const [registerError, setRegisterError] = useState({
    email: "",
    password: "",
    confirmPassword: ""
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
        setRegisterError((prev) => ({ ...prev, confirmPassword: "Passwords do not match" }));
        return;
      }
    }

    // Clear error if valid
    setRegisterError((prev) => ({ ...prev, [name]: "" }));
  };

  const handleSubmit = async () => {
    const hasError = Object.values(registerError).some(error => error !== "");
    const hasEmptyField = Object.values(registerForm).some(value => typeof(value) === "string" && value === "");

    if (hasError || hasEmptyField) return;

    setLoading(true);
    try {
      await api.register({
        username: registerForm.email.split("@")[0], // Use email prefix as username for now
        email: registerForm.email,
        password: registerForm.password,
        firstName: "",
        lastName: ""
      });

      // Redirect to home
      navigate("/", { replace: true });
    } catch (error: any) {
      const errorMessage = error.response?.data || "Registration failed. Please try again.";

      setRegisterSubmissionError(errorMessage);
    } finally {
      setLoading(false);
    }

  }

  return (
    <div className="flex justify-center items-center h-screen">
      <div className="flex flex-col items-center w-full max-w-sm">
        <div className="flex gap-1 items-center">
          <DollarCircle className="size-10 text-indigo-600" />
          <h1 className="text-3xl tracking-wide font-serif font-bold text-zinc-800">CMoney</h1>
        </div>
        <h2 className="text-lg mt-4 text-zinc-600">Sign up to get started</h2>
        <Input
          id="email"
          name="email"
          label="Email Address"
          placeholder="email@example.com"
          containerClassName="w-full mt-8"
          type="email"
          value={registerForm.email}
          onChange={handleFormChange}
          onBlur={handleBlur}
          errorText={registerError.email}
        />
        <Input
          id="password"
          name="password"
          label="Password"
          placeholder="Enter your password"
          containerClassName="w-full mt-2"
          type="password"
          value={registerForm.password}
          onChange={handleFormChange}
          onBlur={handleBlur}
          errorText={registerError.password}
        />
        <Input
          id="confirmPassword"
          name="confirmPassword"
          label="Confirm Password"
          placeholder="Re-enter your password"
          containerClassName="w-full mt-2"
          type="password"
          value={registerForm.confirmPassword}
          onChange={handleFormChange}
          onBlur={handleBlur}
          errorText={registerError.confirmPassword}
        />
        {registerSubmissionError && <p className="text-red-500 text-lg font-semibold mb-4">{registerSubmissionError}</p>}
        <Button className="w-full mt-4" onClick={handleSubmit} disabled={Object.values(registerError).some(error => error !== "")} isLoading={loading}>Sign Up</Button>
        <p className="mt-4">Already have an account? <Link to="/login" className="text-indigo-600 hover:text-indigo-900">Log in</Link></p>
      </div>
    </div>
  );
};

export default RegisterPage;
