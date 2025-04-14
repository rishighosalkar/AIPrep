import { useEffect, useState } from "react"
import AuthPage from "./pages/AuthPage"
import { Navigate, Route, Routes } from "react-router-dom";
import ProfilePage from "./pages/ProfilePage";

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  useEffect(()=>{
    const token = localStorage.getItem("token");
    setIsAuthenticated(!!token);
  }, []);

  return (
      <Routes>
        <Route path="/auth" element={<AuthPage />}/>
        <Route path="/profile" element={isAuthenticated ? <ProfilePage /> : <Navigate to="/auth" />} />
        <Route path="/" element={<Navigate to={isAuthenticated ? "/profile" : "/auth"} />} />
      </Routes>
  )
}

export default App
