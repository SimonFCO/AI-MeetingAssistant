// Detta är formen man skriver in vad man vill skicka till ai
// Jag tänker att man kommer kunna skriva in titel, datum, sen basic information
import { useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { setInputPrompt, generateAiContent } from "../store/meetingSlice";

const PLACEHOLDERS = {
  summarize: "Klistra in dina mötesanteckningar här för sammanfattning...",
  agenda: "Beskriv mötets mål, punkter och önskad tidsram här...",
  invitation:
    "Skriv tid, plats och syfte för mötet så skapas en formell inbjudan...",
};

const BUTTON_TEXTS = {
  summarize: "Sammanfatta anteckningar",
  agenda: "Generera agenda",
  invitation: "Skapa inbjudan",
};

export default function MeetingForm() {
  const dispatch = useDispatch();
  const { activeTab, inputPrompt, loading } = useSelector(
    (state) => state.meeting,
  );

  // för datum och titel
  const [titel, setTitel] = useState("");
  const [datum, setDatum] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    dispatch(generateAiContent({ titel, datum }));
  };

  return (
    <form onSubmit={handleSubmit} className="meeting-form">
      <textarea
        className="input-textarea"
        rows="1"
        value={titel}
        onChange={(e) => setTitel(e.target.value)}
        placeholder={"Titel"}
        disabled={loading}
      />
      <textarea
        className="input-textarea"
        rows="1"
        value={datum}
        onChange={(e) => setDatum(e.target.value)}
        placeholder={"Datum"}
        disabled={loading}
      />
      <textarea
        className="input-textarea"
        rows="6"
        value={inputPrompt}
        onChange={(e) => dispatch(setInputPrompt(e.target.value))}
        placeholder={PLACEHOLDERS[activeTab]}
        disabled={loading}
      />
      <button type="submit" className="primary-btn" disabled={loading}>
        {loading ? "AI tänker..." : BUTTON_TEXTS[activeTab]}
      </button>
    </form>
  );
}
