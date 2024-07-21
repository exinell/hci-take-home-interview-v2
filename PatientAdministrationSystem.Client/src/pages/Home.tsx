import React, { useState, lazy, Suspense } from 'react';
import { Grid, Typography, Paper, CircularProgress } from '@mui/material';
import MainLayout from '../layouts/MainLayout';

const PatientSearch = lazy(() => import('../components/PatientSearch'));
const VisitList = lazy(() => import('../components/VisitList'));

const Home: React.FC = () => {
    const [selectedPatientId, setSelectedPatientId] = useState<string | null>(null);

    return (
        <MainLayout>
            <Typography variant="h4" align="center" gutterBottom>
                Hospital Visit Search
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12} md={6}>
                    <Paper elevation={3} style={{ padding: '16px' }}>
                        <Suspense fallback={<CircularProgress />}>
                            <PatientSearch onSelectPatient={setSelectedPatientId} />
                        </Suspense>
                    </Paper>
                </Grid>
                <Grid item xs={12} md={6}>
                    {selectedPatientId && (
                        <Paper elevation={3} style={{ padding: '16px' }}>
                            <Suspense fallback={<CircularProgress />}>
                                <VisitList patientId={selectedPatientId} />
                            </Suspense>
                        </Paper>
                    )}
                </Grid>
            </Grid>
        </MainLayout>
    );
};

export default Home;
