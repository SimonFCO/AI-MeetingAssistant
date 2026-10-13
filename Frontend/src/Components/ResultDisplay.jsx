// bara en kub som har all ai text

import { useState } from "react";
import { useSelector } from "react-redux";

export default function ResultDisplay() {
  const { result, error, loading } = useSelector((state) => state.meeting);
  const [copied, setCopied] = useState(false);

  const handleCopy = () => {
    navigator.clipboard.writeText(result);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  if (loading) {
    return (
      <div className="status-box">
        ⏳ Genererar svar med AI, vänligen vänta...
      </div>
    );
  }

  if (error) {
    return <div className="status-box error">{error}</div>;
  }

  if (!result) return null;

  return (
    <section className="result-container">
      <div className="result-header">
        <h3>Resultat</h3>
        <button onClick={handleCopy} className="copy-btn">
          {copied ? "Kopierat!" : "Kopiera text"}
        </button>
      </div>
      <div className="result-box">
        <pre>{result}</pre>
      </div>
    </section>
  );
}
