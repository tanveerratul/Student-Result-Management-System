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
            {/* Eye icon e click korle Student Profile page e jabe */}
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