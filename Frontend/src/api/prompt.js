const API_URL = "https://localhost:7102/api/ai";

async function post(endpoint, body) {
    const res = await fetch(`${API_URL}${endpoint}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
    });

    if (!res.ok) {
        const errText = await res.text();
        throw new Error(`Error ${res.status}: ${errText}`);
    }

    return res.json();
}

export function createAgenda(inputPrompt) {
    return post("/agenda", { inputPrompt });
}

export function createSummary(inputPrompt) {
    return post("/summarize", { inputPrompt });
}

export function createInvite(inputPrompt) {
    return post("/invitation", { inputPrompt });
}
