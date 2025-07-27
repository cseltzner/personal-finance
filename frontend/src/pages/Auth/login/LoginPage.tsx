import Input from "../../../components/forms/Input"
import Button from "../../../components/Button"
import DollarCircle from "../../../assets/icons/DollarCircle"
import { Link, useNavigate } from "react-router-dom"
import { useState } from "react"
import { api } from "./api"

const LoginPage = () => {
  const navigate = useNavigate();

  const [loginForm, setLoginForm] = useState({
      email: "",
      password: "",
  })

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
            password: loginForm.password
        });

        navigate("/");
    } catch (error) {
        setLoginError("Login failed. Please check your credentials and try again.");
    } finally {
        setLoading(false);
    }
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <div className="flex flex-col items-center w-full max-w-sm">
        <div className="flex gap-1 items-center">
          <DollarCircle className="size-10 text-indigo-600" />
          <h1 className="text-3xl tracking-wide font-serif font-bold text-zinc-800">CMoney</h1>
        </div>
        <h2 className="text-lg mt-4 text-zinc-600">Sign in to continue</h2>
        <form className="w-full" onSubmit={(e) => { e.preventDefault(); handleSubmit(); }}>
            <Input
            id="email"
            name="email"
            label="Email Address"
            placeholder="email@example.com"
            containerClassName="w-full mt-8"
            type="email"
            value={loginForm.email}
            onChange={handleFormChange}
            />
            <Input
            id="password"
            name="password"
            label="Password"
            placeholder="Enter your password"
            containerClassName="w-full mt-2"
            type="password"
            value={loginForm.password}
            onChange={handleFormChange}
            />
            {loginError && <p className="text-red-500 text-lg font-semibold mb-4">{loginError}</p>}
            <Button className="w-full mt-4" type="submit" isLoading={loading}>Sign In</Button>
        </form>
        <p className="mt-4">Don't have an account? <Link to="/register" className="text-indigo-600 hover:text-indigo-900">Sign up</Link></p>
      </div>
    </div>
  )
}

export default LoginPage