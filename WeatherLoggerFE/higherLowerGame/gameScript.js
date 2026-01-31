const currentCityEl = document.getElementById("currentCity");
const currentTempEl = document.getElementById("currentTemp");
const nextCityEl = document.getElementById("nextCity");
const nextTempEl = document.getElementById("nextTemp");

const resultText = document.getElementById("resultText");
const scoreText = document.getElementById("scoreText");

const higherBtn = document.getElementById("higherBtn");
const lowerBtn = document.getElementById("lowerBtn");
const resetBtn = document.getElementById("resetBtn");

const API_BASE = "https://localhost:7193/api/game";

let currentCity = null;
let nextCity = null;
let score = 0;

async function fetchRandomCity() {
    const res = await fetch(`${API_BASE}/random`);
    if (!res.ok) throw new Error("Failed to fetch city");
    return res.json();
}

async function startGame() {
    score = 0;
    resultText.textContent = "";
    scoreText.textContent = `Score: ${score}`;

    currentCity = await fetchRandomCity();

    do {
        nextCity = await fetchRandomCity();
    } while (nextCity.cityName === currentCity.cityName);

    updateDisplay();
}

function updateDisplay() {
    currentCityEl.textContent = currentCity.cityName;
    currentTempEl.textContent = `${currentCity.averageTemperatureCelsius}°C`;

    nextCityEl.textContent = nextCity.cityName;
    nextTempEl.textContent = "???°C";
    nextTempEl.classList.add("hidden");
}

async function makeGuess(isHigher) {
    const payload = {
        currentCity: currentCity.cityName,
        nextCity: nextCity.cityName,
        isHigherGuess: isHigher
    };

    const res = await fetch(`${API_BASE}/check`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    if (!res.ok) {
        const err = await res.json();
        return alert(err.error || "Error");
    }

    const result = await res.json();

    nextTempEl.textContent = `${result.nextTemperature}°C`;
    nextTempEl.classList.remove("hidden");

    if (result.correct) {
        resultText.textContent = "✅ Correct!";
        score++;
    } else {
        resultText.textContent = "❌ Wrong!";
    }

    scoreText.textContent = `Score: ${score}`;

    // Move forward after reveal
    setTimeout(async () => {
        currentCity = {
            cityName: result.nextCity,
            averageTemperatureCelsius: result.nextTemperature
        };

        do {
            nextCity = await fetchRandomCity();
        } while (nextCity.cityName === currentCity.cityName);

        updateDisplay();
    }, 1200);
}

higherBtn.addEventListener("click", () => makeGuess(true));
lowerBtn.addEventListener("click", () => makeGuess(false));
resetBtn.addEventListener("click", startGame);

startGame();
