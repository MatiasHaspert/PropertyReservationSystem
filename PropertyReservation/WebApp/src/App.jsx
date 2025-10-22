import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import "bootstrap/dist/css/bootstrap.min.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './App.css'
import AppRouter from './routes/AppRouter.jsx';

function App() {
  return <AppRouter />;
}

export default App