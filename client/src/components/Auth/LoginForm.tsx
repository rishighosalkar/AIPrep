import { useState } from "react";
import { login } from "../../api/auth";

export default function LoginForm() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async(e: React.FormEvent) => {
        e.preventDefault();

        try{
            const res = await login(email, password);
            localStorage.setItem("token", res.data.token);
            alert("Login successful.");
        }
        catch(err: any) {
            setError(err.response?.data?.message || "Login failed.");
        }
    };

    return (
        <form onSubmit={handleLogin} className="space-y-4">
            <h2 className="text-xl font-semibold">Login</h2>
            {error && <div className="text-red-500">{error}</div>}
            <input type="email" placeholder="Email" className="input" value={email} onChange={e => setEmail(e.target.value)} />
            <input type="password" placeholder="Password" className="input" value={password} onChange={e => setPassword(e.target.value)} />
            <button type="submit" className="btn-primary">Login</button>
        </form>
    );
}