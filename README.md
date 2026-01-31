# WeatherLogger

WeatherLogger App

A web app that allows users to search for city weather, view historical weather data, and track personal search history. Built with ASP.NET Core Web API for the backend and vanilla JavaScript for the frontend.

⚠️ Note: The frontend is served from the wwwroot folder by the backend. You cannot open index.html directly; use the backend to host it.

Features

Search weather by city

Refresh current weather

View historical weather data per city

Track user search history (Google login supported)

Drag-and-drop reorder of weather cards

API Endpoints
Method	Endpoint	Description
GET	/api/weather/{city}	Get current weather for a city
POST	/api/weather/refresh/{city}	Refresh weather & add to history
GET	/api/weather/history/{city}	Get last 10 weather observations
GET	/api/weather/user/history	Get current user search history
POST	/signin-google	Login with Google ID token

Running the App

Clone the repository

git clone <your-repo-url>
cd WeatherLogger


Restore packages

dotnet restore

Run migrations

dotnet ef database update

Run the backend

dotnet run

By default, the app runs on https://localhost:7193.

Open the frontend

Open your browser and navigate to:

https://localhost:7193/index.html


Do not open files directly from wwwroot. The backend serves the JS modules, API endpoints, and static files.
