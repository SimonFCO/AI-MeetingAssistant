import "./App.css";
import Header from "./Components/Header";
import TabNavigation from "./Components/TabNavigation";
import MeetingForm from "./Components/InputForm";
import ResultDisplay from "./Components/ResultDisplay";
import Footer from "./Components/Bottom";

function App() {
  return (
    <main className="app-container">
      <Header />
      <TabNavigation />
      <section className="card">
        <MeetingForm />
        <ResultDisplay />
      </section>
      <Footer />
    </main>
  );
}

export default App;
