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

<img width="2556" height="1252" alt="Screenshot_9" src="https://github.com/user-attachments/assets/ae04adf9-b04f-4a96-b244-3372ce9ca562" />
<img width="2551" height="1230" alt="Screenshot_3" src="https://github.com/user-attachments/assets/30070aeb-d9a1-4ac3-8798-f7d3e6b359d6" />
<img width="2524" height="1232" alt="Screenshot_2" src="https://github.com/user-attachments/assets/af3e538d-dc47-47b0-853e-f01632b43043" />
<img width="2534" height="1243" alt="Screenshot_1" src="https://github.com/user-attachments/assets/3bbc0684-d1fb-48a2-8f1f-b3dcf4fc5fb2" />




