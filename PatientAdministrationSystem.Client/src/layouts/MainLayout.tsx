import React from 'react';
import { CssBaseline, AppBar, Toolbar, Typography, Box, Container } from '@mui/material';

interface MainLayoutProps {
    children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => (
    <div style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
        <CssBaseline />
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" style={{ flexGrow: 1 }}>
                    HCI - Patient Administration System
                </Typography>
            </Toolbar>
        </AppBar>
        <Container style={{ flex: '1 0 auto', marginTop: '20px', marginBottom: '20px' }}>
            {children}
        </Container>
        <footer style={{ flexShrink: 0 }}>
            <Box mt={5} py={3} textAlign="center" bgcolor="primary.main" color="white">
                <Typography variant="body1">
                    &copy; {new Date().getFullYear()} HCI - Patient Administration System. All rights reserved.
                </Typography>
            </Box>
        </footer>
    </div>
);

export default MainLayout;
