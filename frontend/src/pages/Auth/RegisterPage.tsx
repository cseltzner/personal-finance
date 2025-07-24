import { Link } from "react-router-dom";
import DollarCircle from "../../assets/icons/DollarCircle";
import Button from "../../components/Button";
import Input from "../../components/forms/Input";

const RegisterPage = () => {
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
        />
        <Input
          id="password"
          name="password"
          label="Password"
          placeholder="Enter your password"
          containerClassName="w-full mt-2"
        />
        <Input
          id="confirm-password"
          name="confirm-password"
          label="Confirm Password"
          placeholder="Re-enter your password"
          containerClassName="w-full mt-2"
        />
        <Button className="w-full mt-4" onClick={() => console.log('Sign Up clicked')}>Sign Up</Button>
        <p className="mt-4">Already have an account? <Link to="/login" className="text-indigo-600 hover:text-indigo-900">Log in</Link></p>
      </div>
    </div>
  );
};

export default RegisterPage;
