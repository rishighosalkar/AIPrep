import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom";
import { getProfile } from "../api/user";
import axios from "axios";

interface User {
    id: string,
    fullName: string,
    email: string
}

export default function ProfilePage() {
    const [user, setUser] = useState<User | null>(null);
    const navigate = useNavigate();

    const fetchProfile = async() => {
        const token = localStorage.getItem("token");
        if (!token) {
            console.error("Token not found");
            navigate("/auth");
            return;
        }
        try {
            
            console.log("Token", token);
            const res = await axios.get("https://localhost:7180/api/user/profile", {
                headers:{
                    Authorization: `Bearer ${token}`
                }
            });
        }
        catch(err: any){
            console.error("Error fetching profile", err)
            navigate("/auth")
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        navigate("/auth");
    }

    useEffect(()=>{
        fetchProfile();
    },[]);

    return(
        <div className="p-6 max-w-md mx-auto bg-white shadow-md rounded">
            <h2 className="text-xl font-semibold mb-4">User Profile</h2>
            {user ? (
                <div>
                    <p><strong>Name:</strong> {user.fullName}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                    {/* Optional: Add edit form here */}
                    <button onClick={logout} className="mt-4 bg-red-500 text-white px-4 py-2 rounded">
                        Logout
                    </button>
                </div>
            ) : (
                <p>Loading profile...</p>
            )}
        </div>
    )
}