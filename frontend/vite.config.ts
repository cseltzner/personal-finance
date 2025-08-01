import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  // For Dev Docker
  server: {
    watch: {
      usePolling: true,
      interval: 1000,
    },
    host: "0.0.0.0",
    port: 5173,
  },
})
