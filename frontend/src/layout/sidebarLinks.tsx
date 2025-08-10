import Home from "../assets/icons/Home" 
import BuildingLibrary from "../assets/icons/BuildingLibrary"
import CreditCard from "../assets/icons/CreditCard"

export const sidebarLinks = [{
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