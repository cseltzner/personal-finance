import { Outlet } from "react-router-dom";
import Sidebar from "./components/Sidebar";
import TopBar from "./components/TopBar";
import { Box, Paper, useTheme } from "@mui/material";

const AppLayout = () => {
  const theme = useTheme();
  return (
    <Box
      display="flex"
      height="100vh"
      width="100vw"
      sx={{ backgroundColor: theme.palette.background.default }}
    >
      <Sidebar />
      <Box display="flex" flexDirection="column" flex={1}>
        <TopBar />
        <Box
          component="main"
          sx={{
            flex: 1,
            overflow: "auto",
            px: 9,
            py: 6,
            borderRadius: 2,
          }}
        >
          <Outlet />
        </Box>
      </Box>
    </Box>
  );
};

export default AppLayout;