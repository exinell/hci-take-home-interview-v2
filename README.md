# HCI - Patient Administration System

## Overview

HCI - Patient Administration System is a web application designed to manage patient data and their hospital visits. It provides features such as searching for patients, viewing their visit history, and efficient pagination for handling large datasets. The application leverages React on the frontend and a .NET backend with Entity Framework for data management.

## Features

- Search for patients by name
- View patient visit history
- Pagination support for large datasets
- Lazy loading of components
- Caching to improve performance
- Consistent response structure with error handling

## Technologies Used

### Frontend

- React
- TypeScript
- Material-UI
- Axios

### Backend

- .NET
- Entity Framework Core
- SQL Server


## Setup Instructions

## Prerequisites
Node.js (version 22.x)
.NET SDK (version 5.0 or later)

## Frontend Setup

-Clone the repository
-Navigate to the frontend directory: PatientAdministrationSystem.Client
-npm install
-npm start

## Backend Setup
-Navigate to the API: PatientAdministrationSystem.API
-dotnet run

## API Documentation
The backend API provides endpoints for searching patients and fetching their visit history. The following endpoints are available:

GET /api/v{apiVersion}/patients/getpatients - Search for patients by name.
GET /api/v{apiVersion}/patients/getpatientvisits - Get visit history for a specific patient.

