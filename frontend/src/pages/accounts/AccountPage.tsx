import { Add } from "@mui/icons-material"
import { Box, Button, Icon, IconButton, MenuItem, Select, Typography } from "@mui/material"

const AccountPage = () => {
  return (
    <Box display="flex" flexDirection="column">
        <Box display="flex" justifyContent="space-between" alignItems="center" bgcolor="background.paper" p={6} borderRadius={2} mb={3}>
            <Select
                variant="standard"
                // onChange={() => {}}
                defaultValue="month"
                label="Date Range"
                sx={{
                    px: 3,
                    py: 1
                }}
            >
                <MenuItem value="week">1 week</MenuItem>
                <MenuItem value="month">1 month</MenuItem>
                <MenuItem value="3months">3 months</MenuItem>
                <MenuItem value="year">1 year</MenuItem>
            </Select>
            <Button
                startIcon={<Add />}
                variant="contained"
                color="primary"
                sx={{ textTransform: "none" }}
            >
                Add Account
            </Button>
        </Box>
    </Box>
  )
}

export default AccountPage