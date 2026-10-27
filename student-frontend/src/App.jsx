import { useState } from 'react';
import Dashboard from './Dashboard';
import AddStudent from './AddStudent';
import StudentProfile from './StudentProfile';
import './App.css';

function App() {
  // state diye amra track rakhbo ekhon kon page ta active ache
  const [activePage, setActivePage] = useState('dashboard');

  return (
    <div className="dashboard-wrapper">
      
      {/* === Left Sidebar (Fixed) === */}
      <div className="sidebar">
        <div className="logo">
          <h2>📖 Edu-Portal</h2>
        </div>
        <ul className="menu">
          <li 
            className={activePage === 'dashboard' ? 'active' : ''} 
            onClick={() => setActivePage('dashboard')}
          >
            🎛️ Dashboard
          </li>
          <li 
            className={activePage === 'addStudent' ? 'active' : ''} 
            onClick={() => setActivePage('addStudent')}
          >
            👤+ Add Student
          </li>
        </ul>
      </div>

      {/* === Right Main Content (Dynamic) === */}
      <div className="main-content">
        {activePage === 'dashboard' && <Dashboard setActivePage={setActivePage} />}
        {activePage === 'addStudent' && <AddStudent />}
        {activePage === 'studentProfile' && <StudentProfile setActivePage={setActivePage} />}
      </div>

    </div>
  );
}

export default App;