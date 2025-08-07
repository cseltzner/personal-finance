import { Link, NavLink } from "react-router-dom"
import DollarCircle from "../assets/icons/DollarCircle"

const AppLayout = () => {
  return (
    <div className="flex h-screen w-screen">
        {/* Sidebar */}
        <aside className="w-64 bg-zinc-100 flex flex-col p-4">
            <Link to="/" className="text-2xl font-semibold font-serif flex gap-1 items-center">
                <DollarCircle className="size-8 text-indigo-600" />
                <span>CMoney</span>
            </Link>
            <nav className="flex flex-col gap-3 mt-4">
                <NavLink to="/" className={({ isActive }) => `flex gap-3 items-center text-lg px-3 py-2 rounded font-semibold ${isActive ? 'bg-zinc-200' : 'text-zinc-700 hover:text-indigo-600'}`}>
                    <DollarCircle className="size-6" />
                    <span>Dashboard</span>
                </NavLink>
            </nav>
        </aside>
    </div>
  )
}

export default AppLayout