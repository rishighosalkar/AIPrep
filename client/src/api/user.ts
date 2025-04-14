import axios from "axios";

const API = axios.create({
    baseURL: 'https://localhost:7180/api',
});

export const getProfile = async(token: string | null) => API.get('/user/profile', {
    headers:{
        Authorization: `Bearer ${token}`
    }
});