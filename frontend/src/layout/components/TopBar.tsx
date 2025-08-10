import Button from "../../components/Button";
import { useAuth } from "../../context/AuthContext";
import { useAppLocation } from "../../hooks/useAppLocation";

const TopBar = () => {
  const { logout } = useAuth();

  const { currentTitle } = useAppLocation();
  return (
    <header className="h-16 px-6 flex items-center justify-between bg-zinc-200">
      <div className="text-lg font-semibold">{currentTitle}</div>
      <Button className="text-sm" onClick={logout}>Log out</Button>
    </header>
  );
};

export default TopBar;
