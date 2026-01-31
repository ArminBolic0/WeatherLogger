const currentCityEl = document.getElementById("currentCity");
const currentTempEl = document.getElementById("currentTemp");
const nextCityInput = document.getElementById("nextCityInput");
const resultText = document.getElementById("resultText");
const scoreText = document.getElementById("scoreText");

const higherBtn = document.getElementById("higherBtn");
const lowerBtn = document.getElementById("lowerBtn");
const resetBtn = document.getElementById("resetBtn");

let currentCity = null;
let score = 0;

const API_BASE = "https://localhost:7193/api/game";

async function startGame() {
    const res = await fetch(`${API_BASE}/random`);
    if (!res.ok) { alert("Failed to start game"); return; }
    currentCity = await res.json();
    updateDisplay();
    score = 0;
    resultText.textContent = "";
    scoreText.textContent = `Score: ${score}`;
}

function updateDisplay() {
    currentCityEl.textContent = currentCity.cityName;
    currentTempEl.textContent = `${currentCity.averageTemperatureCelsius}°C`;
}

async function makeGuess(isHigher) {
    const nextCityName = nextCityInput.value.trim();
    if (!nextCityName) return alert("Enter a city!");

    const payload = {
        currentCity: currentCity.cityName,
        nextCity: nextCityName,
        isHigherGuess: isHigher
    };

    try {
        const res = await fetch(`${API_BASE}/check`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            const err = await res.json();
            return alert(err.error || "Error checking guess");
        }

        const result = await res.json();
        if (result.correct) {
            resultText.textContent = `✅ Correct! ${result.nextCity} is ${result.nextTemperature}°C`;
            score++;
        } else {
            resultText.textContent = `❌ Wrong! ${result.nextCity} is ${result.nextTemperature}°C`;
        }

        scoreText.textContent = `Score: ${score}`;
        currentCity = { cityName: result.nextCity, averageTemperatureCelsius: result.nextTemperature };
        updateDisplay();
        nextCityInput.value = "";

    } catch(err) {
        alert(err.message);
    }
}

higherBtn.addEventListener("click", () => makeGuess(true));
lowerBtn.addEventListener("click", () => makeGuess(false));
resetBtn.addEventListener("click", startGame);

startGame();
