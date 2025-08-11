import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import { useAuth } from "../../context/AuthContext";
import { useAppLocation } from "../../hooks/useAppLocation";

const TopBar = () => {
  const { logout } = useAuth();
  const { currentTitle } = useAppLocation();

  return (
    <AppBar position="static" color="default" elevation={0} sx={{ backgroundColor: "background.default", height: 64, justifyContent: "center", px: 5 }}>
      <Toolbar sx={{ minHeight: 64, display: "flex", justifyContent: "space-between" }}>
        <Typography variant="body1" fontWeight={600}>
          {currentTitle}
        </Typography>
        <Box>
          <Button variant="outlined" onClick={logout}>
            Log out
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default TopBar;