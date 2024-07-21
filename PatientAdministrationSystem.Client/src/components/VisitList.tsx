import React, { useEffect, useState, useCallback } from 'react';
import { getPatientVisits } from '../services/api';
import { List, ListItem, ListItemText, CircularProgress, Container, Paper, Typography, Alert, Button } from '@mui/material';
import { Visit } from '../types/Visit';
import { ApiResponse } from '../types/ApiResponse';

interface VisitListProps {
    patientId: string;
}

const VisitList: React.FC<VisitListProps> = ({ patientId }) => {
    const [visits, setVisits] = useState<Visit[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [page, setPage] = useState(1);
    const [totalRecords, setTotalRecords] = useState(0);
    const pageSize = 10;

    const fetchVisits = useCallback(async () => {
        setLoading(true);
        setError(null); // Reset error state before making a new request
        try {
            const response: ApiResponse<Visit[]> = await getPatientVisits(patientId, page, pageSize);
            if (page === 1) {
                setVisits(response.data);
            } else {
                setVisits((prevVisits) => [...prevVisits, ...response.data]);
            }
            setTotalRecords(response.totalRecords || 0);
            if (response.data.length === 0 && page === 1) {
                setError("No visits found for this patient.");
            }
        } catch (error) {
            setError("Failed to fetch visits. Please try again later.");
        } finally {
            setLoading(false);
        }
    }, [patientId, page]);

    useEffect(() => {
        // Reset visits and page when a new patient is selected
        setVisits([]);
        setPage(1);
    }, [patientId]);

    useEffect(() => {
        fetchVisits();
    }, [fetchVisits]);

    return (
        <Container>
            <Typography variant="h5" component="h3" gutterBottom>
                Visits
            </Typography>
            {loading && page === 1 ? (
                <CircularProgress />
            ) : error ? (
                <Alert severity="error">{error}</Alert>
            ) : (
                <List component={Paper}>
                    {visits.map((visit) => (
                        <ListItem key={visit.id}>
                            <ListItemText primary={new Date(visit.date).toLocaleDateString()} />
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

export default React.memo(VisitList);
