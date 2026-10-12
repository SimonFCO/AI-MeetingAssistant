import { useState } from 'react'
import heroImg from './assets/hero.png'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import './App.css'
import { createAgenda } from './api/prompt.js'

function App() {
  const [message, setMessage] = useState("Hello World!");

  const handleClick = async () => {
    try {
      const data = await createAgenda("hi");
      // If your API returns an object (e.g. { message: "..." }), use data.message
      setMessage(typeof data === 'string' ? data : JSON.stringify(data));
    } catch (err) {
      console.error("API error:", err);
      setMessage("Failed to load data");
    }
  };

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>{message}</h1>
      <button onClick={handleClick}>
        Click Me!
      </button>
    </div>
  );
}

export default App; App;
