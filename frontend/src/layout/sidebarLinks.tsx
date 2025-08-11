import { AccountBalance, CreditCard, Dashboard } from "@mui/icons-material"

export const sidebarLinks = [{
    to: '/',
    label: 'Dashboard',
    icon: <Dashboard />
},
{
    to: "/accounts",
    label: "Accounts",
    icon: <AccountBalance />
},
{
    to: "/transactions",
    label: "Transactions",
    icon: <CreditCard />
}]