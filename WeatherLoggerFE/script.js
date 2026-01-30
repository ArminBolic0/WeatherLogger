const searchBtn = document.getElementById("searchBtn");
const cityInput = document.getElementById("cityInput");
const weatherCards = document.getElementById("weatherCards");
let isCelsius = true;

// Weather icons mapping
const weatherIcons = {
    "clear sky": "☀️",
    "few clouds": "🌤️",
    "scattered clouds": "🌥️",
    "broken clouds": "☁️",
    "shower rain": "🌧️",
    "rain": "🌦️",
    "thunderstorm": "⛈️",
    "snow": "❄️",
    "mist": "🌫️"
};

// Weather classes for background
function getWeatherClass(description) {
    description = description.toLowerCase();
    if (description.includes("rain") || description.includes("drizzle")) return "weather-rain";
    if (description.includes("cloud")) return "weather-cloudy";
    if (description.includes("snow")) return "weather-snow";
    if (description.includes("thunder")) return "weather-thunderstorm";
    return "weather-sunny";
}

// Event listeners
searchBtn.addEventListener("click", () => addCityCard(cityInput.value));
cityInput.addEventListener("keyup", e => { if (e.key === "Enter") addCityCard(cityInput.value); });

// Toggle °C / °F
document.getElementById("toggleUnit").addEventListener("click", () => {
    isCelsius = !isCelsius;
    document.querySelectorAll(".weather-card").forEach(card => {
        const tempElem = card.querySelector(".temperature");
        const temp = parseFloat(tempElem.dataset.tempC);
        tempElem.textContent = isCelsius
            ? `${Math.round(temp)}°C ${card.dataset.icon}`
            : `${Math.round(temp * 9/5 + 32)}°F ${card.dataset.icon}`;
    });
});

// Add city card (GET)
async function addCityCard(cityName) {
    const city = cityName.trim() || "Sarajevo";

    try {
        const data = await fetchWeatherGET(city);
        createOrUpdateCard(data);
        cityInput.value = "";
    } catch (err) {
        alert(err.message);
    }
}

// GET from backend
async function fetchWeatherGET(city) {
    const response = await fetch(`https://localhost:7193/api/weather/${city}`);
    if (!response.ok) {
        const postData = await fetchWeatherPOST(city);
        return postData;
    }
    return await response.json();
}

// POST for refresh
async function fetchWeatherPOST(city) {
    const response = await fetch(`https://localhost:7193/api/weather/refresh/${city}`, { method: "POST" });
    if (!response.ok) throw new Error(`Could not refresh weather for ${city}`);
    return await response.json();
}

// Create or update card
function createOrUpdateCard(data) {
    let card = document.querySelector(`.weather-card[data-city="${data.cityName}"]`);
    const icon = weatherIcons[data.weatherDescription.toLowerCase()] || "🌈";
    const weatherClass = getWeatherClass(data.weatherDescription);

    if (!card) {
        card = document.createElement("div");
        card.classList.add("weather-card", weatherClass);
        card.setAttribute("data-city", data.cityName);
        card.dataset.icon = icon;

// Refresh button
        const refreshBtn = document.createElement("button");
        refreshBtn.textContent = "🔄";
        refreshBtn.classList.add("card-refresh-btn");
        refreshBtn.addEventListener("click", async () => {
            const updatedData = await fetchWeatherPOST(data.cityName);
            updateCardContent(card, updatedData);
        });

// Delete button
        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "❌";
        deleteBtn.classList.add("card-delete-btn");
        deleteBtn.addEventListener("click", () => card.remove());

// Create a container for buttons
        const btnContainer = document.createElement("div");
        btnContainer.classList.add("card-buttons");
        btnContainer.appendChild(refreshBtn);
        btnContainer.appendChild(deleteBtn);

// Add the container to the card
        card.appendChild(btnContainer);

        weatherCards.prepend(card);
    }

    updateCardContent(card, data);
}

// Update card
function updateCardContent(card, data) {
    const icon = weatherIcons[data.weatherDescription.toLowerCase()] || "🌈";
    const temp = data.temperatureCelsius;
    card.dataset.tempC = temp;
    const displayTemp = isCelsius ? `${Math.round(temp)}°C ${icon}` : `${Math.round(temp * 9/5 + 32)}°F ${icon}`;
    const weatherClass = getWeatherClass(data.weatherDescription);

    // Preserve buttons
    const refreshBtn = card.querySelector(".card-refresh-btn");
    const deleteBtn = card.querySelector(".card-delete-btn");

    card.className = `weather-card ${weatherClass}`;
    card.dataset.city = data.cityName;
    card.dataset.icon = icon;

    card.innerHTML = `
    <h2>${data.cityName}, ${data.countryName}</h2>
    <div class="temperature" data-tempC="${temp}">${displayTemp}</div>
    <p>Humidity: ${data.humidity}%</p>
    <p>Wind: ${data.windSpeed} m/s</p>
    <p class="description">${data.weatherDescription}</p>
    <p class="time">Observed at: ${new Date(data.observationTime).toLocaleTimeString()}</p>
`;

    // Re-add buttons
    card.appendChild(refreshBtn);
    card.appendChild(deleteBtn);
}
