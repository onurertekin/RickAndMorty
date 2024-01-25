
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Login from "./pages/Login/Login";
import Home from "./pages/Home/Home";
import Episodes from "./pages/Episodes/Episodes";
import Character from "./pages/Character/Character";
import Locations from "./pages/Locations/Locations";
import TopMenu from "./components/TopMenu/TopMenu";

function App() {
  return (
    <Router>
      
      {window.location.pathname !== "/login" && <TopMenu />}
      
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<Home />} />
          <Route path="/episodes" element={<Episodes />} />
          <Route path="/characters/:id" element={<Character />} />
          <Route path="/locations" element={<Locations />} />
        </Routes>
    </Router>
  );
}

export default App;
