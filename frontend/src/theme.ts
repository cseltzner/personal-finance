import { createTheme } from "@mui/material";

const theme = createTheme({
    spacing: 4, // 1 spacing unit = 4px (same as Tailwind's defaults)
    typography: {
        fontFamily: "Libre Franklin, Arial, sans-serif",
    },
    // Using Tailwind CSS color palette
    palette: {
    primary: {
      light: '#a5b4fc', // indigo-300
      main:  '#6366f1', // indigo-500
      dark:  '#4338ca', // indigo-700
      contrastText: '#ffffff',
    },
    secondary: {
      light: '#6ee7b7', // emerald-300
      main:  '#10b981', // emerald-500
      dark:  '#047857', // emerald-700
      contrastText: '#ffffff',
    },
    error: {
      light: '#fca5a5', // red-300
      main:  '#ef4444', // red-500
      dark:  '#b91c1c', // red-700
      contrastText: '#ffffff',
    },
    warning: {
      light: '#fcd34d', // amber-300
      main:  '#f59e0b', // amber-500
      dark:  '#b45309', // amber-700
      contrastText: '#000000',
    },
    info: {
      light: '#93c5fd', // blue-300
      main:  '#3b82f6', // blue-500
      dark:  '#1d4ed8', // blue-700
      contrastText: '#ffffff',
    },
    success: {
      light: '#86efac', // green-300
      main:  '#22c55e', // green-500
      dark:  '#15803d', // green-700
      contrastText: '#ffffff',
    },
    background: {
    //   default: '#f9fafb', // gray-50
      default: '#f3f4f6', // gray-100
      paper:   '#f9fafb', // gray-50
    },
    text: {
      primary:   '#111827', // gray-900
      secondary: '#374151', // gray-700
    },
    // @ts-ignore
    surface: {
        main: "#e5e7eb", // gray-200
        secondary: "#d1d5db", // gray-300
    }
  },
})

export default theme;