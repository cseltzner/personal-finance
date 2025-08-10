import { Link, NavLink, Outlet, useLocation } from "react-router-dom"
import DollarCircle from "../assets/icons/DollarCircle"
import Home from "../assets/icons/Home"
import BuildingLibrary from "../assets/icons/BuildingLibrary"
import CreditCard from "../assets/icons/CreditCard"

const sidebarLinks = [{
    to: '/',
    label: 'Dashboard',
    icon: <Home />
},
{
    to: "/accounts",
    label: "Accounts",
    icon: <BuildingLibrary />
},
{
    to: "/transactions",
    label: "Transactions",
    icon: <CreditCard />
}]

const AppLayout = () => {
  const location = useLocation();
  const activeLink = sidebarLinks.find(link => link.to === location.pathname);
  const currentTitle = activeLink ? activeLink.label : '';

  return (
    <div className="flex h-screen w-screen">
        {/* Sidebar */}
        <aside className="w-64 bg-zinc-200 flex flex-col p-4">
            <Link to="/" className="text-2xl font-semibold font-serif flex gap-1 items-center">
                <DollarCircle className="size-8 text-indigo-600" />
                <span>CMoney</span>
            </Link>
            <nav className="flex flex-col gap-3 mt-4">
                {sidebarLinks.map((link) => (
                    <NavLink key={link.label} to={link.to} className={({ isActive }) => `flex gap-3 items-center text-lg px-3 py-2 rounded font-semibold ${isActive ? 'bg-zinc-300' : 'text-zinc-700 hover:text-indigo-600'}`}>
                        {link.icon}
                        <span>{link.label}</span>
                    </NavLink>
                ))}
            </nav>
        </aside>
        <div className="flex flex-col flex-1">
            {/* Top bar */}
            <header className="h-16 px-6 flex items-center justify-between bg-zinc-200">
                <div className="text-lg font-semibold">{currentTitle}</div>
                <div className="text-sm text-gray-600">Log out!</div>
            </header>

            {/* Main */}
            <main className="flex-1 overflow-auto px-9 py-6">
                <Outlet />
            </main>
        </div>
    </div>
  )
}

export default AppLayout