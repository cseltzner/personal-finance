import { useAppLocation } from "../../hooks/useAppLocation";

const TopBar = () => {
  const { currentTitle } = useAppLocation();
  return (
    <header className="h-16 px-6 flex items-center justify-between bg-zinc-200">
      <div className="text-lg font-semibold">{currentTitle}</div>
      <div className="text-sm text-gray-600">Log out!</div>
    </header>
  );
};

export default TopBar;
