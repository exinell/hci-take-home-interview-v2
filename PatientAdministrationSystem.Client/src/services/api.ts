import axios, { AxiosResponse } from 'axios';
import { ApiResponse } from '../types/ApiResponse';
import { Patient } from '../types/Patient';
import { Visit } from '../types/Visit';

const api = axios.create({
    baseURL: 'https://patientadminsystem.azurewebsites.net/api',
});

const handleResponse = <T>(response: AxiosResponse<ApiResponse<T>>): ApiResponse<T> => {
    if (response.data.success) {
        return response.data;
    } else {
        throw new Error(response.data.message);
    }
};

const apiVersion = 'v1';

export const getPatients = (name: string, page: number, pageSize: number): Promise<ApiResponse<Patient[]>> => {
    return api.get(`/${apiVersion}/patients/getpatients`, { params: { name, page, pageSize } })
        .then(response => handleResponse<Patient[]>(response));
};

export const getPatientVisits = (patientId: string, page: number, pageSize: number): Promise<ApiResponse<Visit[]>> => {
    return api.get(`/${apiVersion}/patients/getpatientvisits`, { params: { patientId, page, pageSize } })
        .then(response => handleResponse<Visit[]>(response));
};
