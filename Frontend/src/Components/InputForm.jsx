// Detta är formen man skriver in vad man vill skicka till ai
// Jag tänker att man kommer kunna skriva in titel, datum, sen basic information

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

  const handleSubmit = (e) => {
    e.preventDefault();
    dispatch(generateAiContent());
  };

  return (
    <form onSubmit={handleSubmit} className="meeting-form">
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
