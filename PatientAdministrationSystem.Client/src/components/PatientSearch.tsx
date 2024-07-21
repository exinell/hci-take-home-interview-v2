import React, { useState, useEffect, useCallback } from 'react';
import { getPatients } from '../services/api';
import { TextField, List, ListItem, ListItemText, CircularProgress, Container, Paper, Alert, Button, Typography } from '@mui/material';
import { Patient } from '../types/Patient';
import { ApiResponse } from '../types/ApiResponse';

interface PatientSearchProps {
    onSelectPatient: (patientId: string) => void;
}

const cache: { [key: string]: { data: Patient[]; totalRecords: number } } = {};

const PatientSearch: React.FC<PatientSearchProps> = ({ onSelectPatient }) => {
    const [name, setName] = useState('');
    const [patients, setPatients] = useState<Patient[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [page, setPage] = useState(1);
    const [totalRecords, setTotalRecords] = useState(0);
    const pageSize = 10;

    const searchPatients = useCallback(async () => {
        const cacheKey = `${name}-${page}`;
        if (cache[cacheKey]) {
            const cachedData = cache[cacheKey];
            setPatients((prevPatients) => (page === 1 ? cachedData.data : [...prevPatients, ...cachedData.data]));
            setTotalRecords(cachedData.totalRecords);
            return;
        }

        setLoading(true);
        setError(null); // Reset error state before making a new request
        try {
            const response: ApiResponse<Patient[]> = await getPatients(name, page, pageSize);
            if (page === 1) {
                setPatients(response.data);
            } else {
                setPatients((prevPatients) => [...prevPatients, ...response.data]);
            }
            setTotalRecords(response.totalRecords || 0);
            cache[cacheKey] = {
                data: response.data,
                totalRecords: response.totalRecords || 0,
            };
        } catch (error) {
            setError("Failed to fetch patients. Please try again later.");
        } finally {
            setLoading(false);
        }
    }, [name, page]);

    useEffect(() => {
        const delayDebounceFn = setTimeout(() => {
            if (name) {
                setPage(1);
                searchPatients();
            }
        }, 300);

        return () => clearTimeout(delayDebounceFn);
    }, [name, searchPatients]);

    useEffect(() => {
        if (page > 1) {
            searchPatients();
        }
    }, [page, searchPatients]);

    return (
        <Container>
            <Typography variant="h5" component="h2" gutterBottom>
                Search for a Patient
            </Typography>
            <TextField
                label="Search patient by name"
                variant="outlined"
                fullWidth
                value={name}
                onChange={(e) => setName(e.target.value)}
                margin="normal"
            />
            {loading && page === 1 ? (
                <CircularProgress />
            ) : error ? (
                <Alert severity="error">{error}</Alert>
            ) : (
                <List component={Paper}>
                    {patients.map((patient) => (
                        <ListItem button key={patient.id} onClick={() => onSelectPatient(patient.id)}>
                            <ListItemText primary={`${patient.firstName} ${patient.lastName}`} />
                        </ListItem>
                    ))}
                </List>
            )}
            {page * pageSize < totalRecords && !loading && (
                <Button onClick={() => setPage((prevPage) => prevPage + 1)}>Load More</Button>
            )}
            {loading && page > 1 && <CircularProgress />}
        </Container>
    );
};

export default React.memo(PatientSearch);
