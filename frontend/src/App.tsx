import { RouterProvider } from 'react-router-dom'
import './App.css'
import { router } from './Router'
import { ThemeProvider } from '@mui/material'
import theme from './theme'
import { ModalProvider } from './context/ModalContext'

const App = () => {
  return (
    <ThemeProvider theme={theme}>
      <ModalProvider>
        <RouterProvider router={router} />
      </ModalProvider>
    </ThemeProvider>
  )
}

export default App