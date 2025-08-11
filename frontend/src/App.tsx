import { RouterProvider } from 'react-router-dom'
import './App.css'
import { router } from './Router'
import { ThemeProvider } from '@mui/material'
import theme from './theme'

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <RouterProvider router={router} />
    </ThemeProvider>
  )
}

export default App