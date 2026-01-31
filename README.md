# WeatherLogger

WeatherLogger is a web app that allows users to search for city weather, view historical weather data, and track personal search history. Built with **ASP.NET Core Web API** for the backend and **vanilla JavaScript** for the frontend.

> ⚠️ **Note:** The frontend is served from the `wwwroot` folder by the backend. You **cannot** open `index.html` directly; use the backend to host it.

---

## Features

- Search weather by city  
- Refresh current weather  
- View historical weather data per city  
- Track user search history (Google login supported)  
- Drag-and-drop reorder of weather cards  

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET    | `/api/weather/{city}` | Get current weather for a city |
| POST   | `/api/weather/refresh/{city}` | Refresh weather & add to history |
| GET    | `/api/weather/history/{city}` | Get last 10 weather observations |
| GET    | `/api/weather/user/history` | Get current user search history |
| POST   | `/signin-google` | Login with Google ID token |

---

## Running the App

1. **Clone the repository**  

```bash
git clone <repo-url>
cd WeatherLogger
Restore packages

dotnet restore
Run migrations

dotnet ef database update
Run the backend

dotnet run
By default, the app runs on https://localhost:7193

Open the frontend

Open your browser and navigate to:

https://localhost:7193/index.html
Do not open files directly from wwwroot. The backend serves the JS modules, API endpoints, and static files.
```


<img width="2556" height="1252" alt="Screenshot_9" src="https://github.com/user-attachments/assets/5fece8ce-9885-4d53-8128-ec6f3254fe65" />
<img width="2534" height="1243" alt="Screenshot_1" src="https://github.com/user-attachments/assets/299d3e3c-0e22-4e80-b0a9-1bb3eb0a095c" />
<img width="2551" height="1230" alt="Screenshot_3" src="https://github.com/user-attachments/assets/d5231ea2-2ae8-4ab5-beb8-f5c400262272" />
<img width="2524" height="1232" alt="Screenshot_2" src="https://github.com/user-attachments/assets/09d35117-7e3f-475b-b697-c3a54938cf45" />

