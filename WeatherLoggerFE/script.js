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
cityInput.addEventListener("keyup", e => { if (e.key === "Enter") addCity(); });
document.getElementById("gameBtn").addEventListener("click", () => {
    window.location.href = "/higherLowerGame/game.html";
});
document.getElementById("loginBtn").addEventListener("click", () => {
    window.location.href = "/auth/login";
});

async function addCity() {
    const city = cityInput.value.trim();
    if (!city) return;

    try {
        const data = await fetchGet(city);
        if (!data) {
            data = await fetchPost(city);
        }
        createCard(data);
        await showUserHistory();
        cityInput.value = "";
    } catch (err) {
        console.error("Error adding city:", err);
    }
}

async function fetchGet(city)
{
    const res = await fetch(`https://localhost:7193/api/weather/${city}`, {
        method: "GET",
        credentials: "include"
    });
    if (!res.ok) throw new Error();
    return res.json();
}

async function fetchPOST(city) {
    const res = await fetch(`https://localhost:7193/api/weather/refresh/${city}`, {
        method: "POST",
        credentials: "include"
    });
    if (!res.ok) throw new Error();
    return res.json();
}

async function fetchHistory(city) {
    const res = await fetch(`https://localhost:7193/api/weather/history/${city}?count=10`, {
        credentials: "include"
    });
    if (!res.ok) throw new Error();
    return res.json();
}
async function fetchUserHistory() {
    try {
        const res = await fetch(`https://localhost:7193/api/weather/user/history`, {
            credentials: "include"
        });
        if (!res.ok) return { histories: [] };
        return await res.json();
    } catch {
        return { histories: [] };
    }
}

async function showUserHistory() {
    const data = await fetchUserHistory();
    const listEl = document.getElementById("historyList");
    listEl.innerHTML = "";

    const histories = data?.histories ?? [];
    if (!histories.length) {
        listEl.innerHTML = "<li>No history yet.</li>";
        return;
    }

    histories.forEach(h => {
        const li = document.createElement("li");
        li.textContent = `${h.cityName} - ${new Date(h.observationTime).toLocaleString()}`;
        listEl.appendChild(li);
    });
}

function handleCredentialResponse(response) {
    fetch("https://localhost:7193/signin-google", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ idToken: response.credential }),
        credentials: "include"
    })
        .then(res => {
            if (!res.ok) throw new Error("Failed to log in");
            return res.json();
        })
        .then(async user => {
            console.log("Logged in user:", user);
            await showUserHistory();
        })
        .catch(err => console.error(err));
}

function createCard(data) {
    if (document.querySelector(`[data-city="${data.cityName}"]`)) return;

    const card = document.createElement("div");
    card.className = `weather-card ${getWeatherClass(data.weatherDescription)}`;
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
        <div class="expand-btn">⬇</div>
        <div class="history"></div>
    `;

    const historyDiv = card.querySelector(".history");
    const expandBtn = card.querySelector(".expand-btn");

    expandBtn.onclick = async () => {
        if (historyDiv.classList.contains("open")) {
            historyDiv.classList.remove("open");
            expandBtn.textContent = "⬇";
            return;
        }
        expandBtn.textContent = "⏳";
        const history = await fetchHistory(data.cityName);
        historyDiv.innerHTML = history.map(h => `
            <div class="history-item">
                <span>${new Date(h.observationTime).toLocaleTimeString()}</span>
                <strong>${Math.round(h.temperatureCelsius)}°C</strong>
            </div>
        `).join("");
        historyDiv.classList.add("open");
        expandBtn.textContent = "⬆";
    };

    card.querySelector(".refresh").onclick = async () => {
        const updated = await fetchPOST(data.cityName);
        card.remove();
        createCard(updated);
        await showUserHistory();
    };

    card.querySelector(".delete").onclick = () => card.remove();

    card.addEventListener("mouseenter", () => app.style.background = getComputedStyle(card).background);
    card.addEventListener("mouseleave", () => app.style.background = "");
    card.addEventListener("dragstart", () => draggedCard = card);
    card.addEventListener("dragover", e => e.preventDefault());
    card.addEventListener("drop", () => {
        if (draggedCard && draggedCard !== card) weatherCards.insertBefore(draggedCard, card);
    });

    weatherCards.prepend(card);
}

document.addEventListener("DOMContentLoaded", () => {
    showUserHistory();
});
