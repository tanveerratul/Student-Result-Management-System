import BangladeshMap from './BangladeshMap';

function Dashboard({ setActivePage }) {
  return (
    <>
      <div className="topbar">
        <h2>Welcome, Tanveer Ratul</h2>
        <button className="profile-btn">Tanveer 👤</button>
      </div>

      <div className="stats-container">
        <div className="card">
          <div className="icon">👥</div>
          <div className="info">
            <p>Total Students</p>
            <h3>143</h3>
          </div>
        </div>
        <div className="card">
          <div className="icon">📄</div>
          <div className="info">
            <p>Total Courses</p>
            <h3>45</h3>
          </div>
        </div>
        <div className="card">
          <div className="icon">📈</div>
          <div className="info">
            <p>Avg. CGPA</p>
            <h3>3.23</h3>
          </div>
        </div>
      </div>

    {/* Map Section */}
      <div className="map-section" style={{ marginTop: '20px', marginBottom: '30px', background: 'white', padding: '20px', borderRadius: '8px', border: '1px solid #e2e8f0' }}>
        <h3 style={{ marginBottom: '10px', color: '#1e223c' }}>📍 Students Distribution</h3>
        <p style={{ fontSize: '12px', color: '#555', marginBottom: '15px' }}>Interactive map showing registered students across Bangladesh</p>
        <BangladeshMap />
      //</div> 


      <div className="action-bar"></div>

      <div className="action-bar">
        <div className="search-box">
          🔍 <input type="text" placeholder="Search..." />
        </div>
        {/* Button e click korle Add Student page e jabe */}
        <button className="add-btn" onClick={() => setActivePage('addStudent')}>
          + Add Student
        </button>
      </div>

      <table className="student-table">
        <thead>
          <tr>
            <th>Student ID</th>
            <th>Student Name</th>
            <th>Phone No.</th>
            <th>Division</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>05202601</td>
            <td>Tanveer Ratul</td>
            <td>01841499201</td>
            <td>Dhaka</td>
            <td className="actions">
              <span onClick={() => setActivePage('studentProfile')}>👁️</span> ✏️ 🗑️
            </td>
          </tr>

          <tr>
            <td>05202602</td>
            <td>Hasibul Hasan</td>
            <td>01623894546</td>
            <td>Comilla</td>
            <td className="actions">
              <span onClick={() => setActivePage('studentProfile')}>👁️</span> ✏️ 🗑️
            </td>
          </tr>

          <tr>
            <td>05202603</td>
            <td>Mubasshir Jawad</td>
            <td>01815536976</td>
            <td>Bogura</td>
            <td className="actions">
              <span onClick={() => setActivePage('studentProfile')}>👁️</span> ✏️ 🗑️
            </td>
          </tr>

        </tbody>
      </table>
    </>
  );
}

export default Dashboard;