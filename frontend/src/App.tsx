import { RouterProvider } from 'react-router-dom'
import './App.css'
import { AuthProvider } from './context/AuthContext'
import { router } from './Router'

const App = () => {
  return (
      <AuthProvider>
        <RouterProvider router={router} />
      </AuthProvider>
  )
}

export default App