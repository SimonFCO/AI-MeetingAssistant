import "./App.css";
import Header from "./Components/Header";
import TabNavigation from "./Components/TabNavigation";
import MeetingForm from "./Components/InputForm";
import ResultDisplay from "./Components/ResultDisplay";

function App() {
  return (
    <main className="app-container">
      <Header />
      <TabNavigation />
      <section className="card">
        <MeetingForm />
        <ResultDisplay />
      </section>
    </main>
  );
}

export default App;
