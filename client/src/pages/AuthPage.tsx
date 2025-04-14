import { useState } from "react";
import LoginForm from "../components/Auth/LoginForm";
import RegisterForm from "../components/Auth/RegisterForm";

export default function AuthPage(){
    const [isLogin, setIsLogin] = useState(true);
    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            {isLogin ? <LoginForm /> : <RegisterForm />}
            <button onClick={()=> setIsLogin(!isLogin)}>
                {isLogin ? "Don't have an account? Register" : "Already have an account? Login"}
            </button>
        </div>
    )
}