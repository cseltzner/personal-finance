import { useLocation } from "react-router-dom";
import { sidebarLinks } from "../layout/sidebarLinks";

export const useAppLocation = () => {
  const location = useLocation();
  const activeLink = sidebarLinks.find((link) => link.to === location.pathname);
  const currentTitle = activeLink ? activeLink.label : "";
  return { location, currentTitle };
};
