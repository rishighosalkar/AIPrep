import axios from "axios";

const API = axios.create({
    baseURL: 'https://localhost:7180/api',
});

export const login = async(email: string, password: string) => API.post('/auth/login', {email, password});

export const register = async(fullname: string, email: string, password: string) => API.post('/auth/register', 
    {fullname, email, password}
);