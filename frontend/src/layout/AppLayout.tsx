import { Outlet } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import TopBar from "./components/TopBar";

const AppLayout = () => {
  return (
    <div className="flex h-screen w-screen">
      <Sidebar />
      <div className="flex flex-col flex-1">
        <TopBar />
        <main className="flex-1 overflow-auto px-9 py-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AppLayout;
