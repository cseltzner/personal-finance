import { Link, NavLink } from "react-router-dom";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { sidebarLinks } from "../sidebarLinks";
import { Paid } from "@mui/icons-material";

const Sidebar = () => {
  return (
    <Drawer
      variant="permanent"
      sx={{
        width: 240,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: {
          width: 240,
          boxSizing: "border-box",
          backgroundColor: "background.default",
          borderRight: "none",
        },
      }}
    >
      <Link
        to="/"
        className="text-2xl font-semibold font-serif flex gap-2 items-center p-4"
      >
        <Paid sx={{ fontSize: 32, color: "primary.main" }} />
        <span>CMoney</span>
      </Link>
      <List sx={{ mt: 2 }}>
        {sidebarLinks.map((link) => (
          <ListItem key={link.label} disablePadding>
            <ListItemButton
              component={NavLink}
              to={link.to}
              sx={{
                borderRadius: 1,
                "&.active": {
                  backgroundColor: "surface.secondary",
                },
              }}
            >
              <ListItemIcon sx={{ minWidth: 40 }}>{link.icon}</ListItemIcon>
              <ListItemText primary={link.label} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Drawer>
  );
};

export default Sidebar;
