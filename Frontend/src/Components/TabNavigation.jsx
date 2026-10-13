// ska bara innehåla

import { useSelector, useDispatch } from "react-redux";
import { setActiveTab } from "../store/meetingSlice";

export default function TabNavigation() {
  const dispatch = useDispatch();
  const activeTab = useSelector((state) => state.meeting.activeTab);

  const tabs = [
    { id: "summarize", label: "Sammanfatta" },
    { id: "agenda", label: "Agenda" },
    { id: "invitation", label: "Inbjudan" },
  ];

  return (
    <nav className="tabs">
      {tabs.map((tab) => (
        <button
          key={tab.id}
          className={`tab-btn ${activeTab === tab.id ? "active" : ""}`}
          onClick={() => dispatch(setActiveTab(tab.id))}
        >
          {tab.label}
        </button>
      ))}
    </nav>
  );
}
