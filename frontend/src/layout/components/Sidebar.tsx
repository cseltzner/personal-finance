import { Link, NavLink } from "react-router-dom";
import DollarCircle from "../../assets/icons/DollarCircle";
import { sidebarLinks } from "../sidebarLinks";

const Sidebar = () => {
  return (
    <aside className="w-64 bg-zinc-200 flex flex-col p-4">
      <Link
        to="/"
        className="text-2xl font-semibold font-serif flex gap-1 items-center"
      >
        <DollarCircle className="size-8 text-indigo-600" />
        <span>CMoney</span>
      </Link>
      <nav className="flex flex-col gap-3 mt-4">
        {sidebarLinks.map((link) => (
          <NavLink
            key={link.label}
            to={link.to}
            className={({ isActive }) =>
              `flex gap-3 items-center text-lg px-3 py-2 rounded font-semibold ${
                isActive ? "bg-zinc-300" : "text-zinc-700 hover:text-indigo-600"
              }`
            }
          >
            {link.icon}
            <span>{link.label}</span>
          </NavLink>
        ))}
      </nav>
    </aside>
  );
};

export default Sidebar;
