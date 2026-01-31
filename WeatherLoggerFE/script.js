const app = document.getElementById("app");
const searchBtn = document.getElementById("searchBtn");
const cityInput = document.getElementById("cityInput");
const weatherCards = document.getElementById("weatherCards");

let draggedCard = null;

function getWeatherClass(desc = "") {
    desc = desc.toLowerCase();

    if (desc.includes("sun") || desc.includes("clear")) return "sunny";
    if (desc.includes("cloud")) return "cloudy";
    if (desc.includes("rain")) return "rainy";
    if (desc.includes("snow")) return "snowy";
    if (desc.includes("storm") || desc.includes("thunder")) return "stormy";
    return "default";
}

searchBtn.addEventListener("click", () => addCity());
cityInput.addEventListener("keyup", e => {
    if (e.key === "Enter") addCity();
});
document.getElementById("gameBtn").addEventListener("click", () => {
    window.location.href = "higherLowerGame/game.html";
});

async function addCity() {
    const city = cityInput.value.trim();
    if (!city) return;

    try {
        let data = await fetchGET(city);
        createCard(data);
        cityInput.value = "";
    } catch {
        await fetchPOST(city);
        const data = await fetchGET(city);
        createCard(data);
    }
}

async function fetchGET(city) {
    const res = await fetch(`https://localhost:7193/api/weather/${city}`);
    if (!res.ok) throw new Error();
    return res.json();
}

async function fetchPOST(city) {
    const res = await fetch(`https://localhost:7193/api/weather/refresh/${city}`, {
        method: "POST"
    });
    if (!res.ok) throw new Error("Refresh failed");
    return res.json();
}

function createCard(data) {
    if (document.querySelector(`[data-city="${data.cityName}"]`)) return;

    const card = document.createElement("div");
    const weatherClass = getWeatherClass(data.weatherDescription);

    card.className = `weather-card ${weatherClass}`;
    card.setAttribute("data-city", data.cityName);
    card.draggable = true;

    card.innerHTML = `
        <div class="card-header">
            <h2>${data.cityName}, ${data.countryName}</h2>
            <div class="card-actions">
                <button class="refresh">🔄</button>
                <button class="delete">🗑</button>
            </div>
        </div>

        <div class="temperature">${Math.round(data.temperatureCelsius)}°C</div>
        <div class="description">${data.weatherDescription}</div>

        <div class="meta">
            <div>Humidity: ${data.humidity}%</div>
            <div>Wind: ${data.windSpeed} m/s</div>
            <div>${new Date(data.observationTime).toLocaleTimeString()}</div>
        </div>
    `;

    card.addEventListener("mouseenter", () => {
        app.style.background = getComputedStyle(card).background;
    });
    card.addEventListener("mouseleave", () => {
        app.style.background = "";
    });

    card.querySelector(".refresh").onclick = async () => {
        const updated = await fetchPOST(data.cityName);
        card.remove();
        createCard(updated);
    };

    card.querySelector(".delete").onclick = () => card.remove();

    card.addEventListener("dragstart", () => draggedCard = card);
    card.addEventListener("dragover", e => e.preventDefault());
    card.addEventListener("drop", () => {
        if (draggedCard && draggedCard !== card) {
            weatherCards.insertBefore(draggedCard, card);
        }
    });

    weatherCards.prepend(card);
}
