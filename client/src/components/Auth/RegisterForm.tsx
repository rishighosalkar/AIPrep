import { useState } from "react";
import { register } from "../../api/auth";

export default function RegisterForm() {
    const [fullName, setFullName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handlerRegister = async(e: React.FormEvent) => {
        e.preventDefault();

        try{
            await register(fullName, email, password);
            setMessage("Registered successfully. Please login");
        }
        catch(err: any){
            setMessage(err.response?.data?.message || "Registration failed");
        }
    };

    return (
        <form onSubmit={handlerRegister} className="space-y-4">
            <h2 className="text-xl font-semibold">Register</h2>
            {message && <div className="text-blue-500">{message}</div>}
            <input type="fullname" placeholder="Full Name" className="input" value={fullName} onChange={e => setFullName(e.target.value)} />
            <input type="email" placeholder="Email" className="input" value={email} onChange={e => setEmail(e.target.value)} />
            <input type="password" placeholder="Password" className="input" value={password} onChange={e => setPassword(e.target.value)} />
            <button type="submit" className="btn-primary">Register</button>
        </form>
    );
}