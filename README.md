# Chicago 311 Service Request Tracker

## Overview
A full-stack application that displays real Chicago 311 service request data. An Angular frontend calls an ASP.NET Core REST API, which reads service requests stored in a MySQL database. Users can view, search, and filter the most recent requests.

## Tech Stack
- Angular
- ASP.NET Core / C#
- MySQL
- Entity Framework Core
- REST API
- Chicago Data Portal 311 dataset

## Architecture
```
Angular Frontend
      ↓
ASP.NET Core REST API
      ↓
MySQL Database
      ↓
Chicago 311 Data
```

The Angular frontend requests data from the ASP.NET Core API. The API uses Entity Framework Core to read the stored Chicago 311 records from MySQL and returns them as JSON. A separate Python script (`importer/`) loads records from the Chicago Data Portal into MySQL.

## Features
- View recent Chicago 311 service requests
- Search by request number, address, or request type
- Filter by Open/Closed status
- Summary cards for total, open, and closed requests
- Responsive table UI

**Note on status:** the Chicago 311 dataset returns statuses such as `Open` and `Completed`. The application treats every non-`Open` request as closed for the UI filter and summary cards.

## Data
The data comes from the [Chicago 311 Service Requests dataset](https://data.cityofchicago.org/Service-Requests/311-Service-Requests/v6vf-nfxy) on the City of Chicago Data Portal.

The application does not contain all Chicago 311 records. The importer fetches up to 1,000 of the most recent records per run, and the API (`GET /api/ServiceRequests`) returns the 100 most recent records stored in the database.

## Project Structure
```
Chicago311Api/   ASP.NET Core Web API (.NET 10)
frontend/        Angular application
importer/        Python script that imports data into MySQL
database/        MySQL schema
```

## Running Locally

Prerequisites: MySQL, .NET 10 SDK, Node.js, and Python 3.

**1. Start MySQL and create the database**
```bash
brew services start mysql
mysql -u root < database/schema.sql
```
The API connects as `root` with no password (see `Chicago311Api/appsettings.json`). Change the connection string if your local MySQL is set up differently.

**2. Import data**
```bash
pip install requests mysql-connector-python
python3 importer/import_data.py
```

**3. Start the API** (runs at http://localhost:5181)
```bash
cd Chicago311Api
dotnet run
```
Swagger UI is available at http://localhost:5181/swagger.

**4. Start the Angular frontend** (runs at http://localhost:4200)
```bash
cd frontend
npm install
npx ng serve
```
