import { BarChart, Bar, XAxis, YAxis, Tooltip, PieChart, Pie, Cell, ResponsiveContainer } from 'recharts';

function StudentProfile({ setActivePage }) {
  const barData = [
    { name: 'CSE1101', grade: 4.00 },
    { name: 'CSE1102', grade: 3.50 },
    { name: 'MAT1110', grade: 3.00 },
    { name: 'PHY1112', grade: 3.75 },
    { name: 'CSE1104', grade: 3.75 },
    { name: 'ENG1108', grade: 3.00 },
  ];

  const pieData = [
    { name: 'This student', value: 3.54 },
    { name: 'Class Average', value: 3.18 },
  ];
  const PIE_COLORS = ['#1e223c', '#206a5d'];

  return (
    <div className="profile-container">
      
      <div className="profile-top-bar">
        <span className="back-btn" onClick={() => setActivePage('dashboard')}>
          ← Back to Student List
        </span>
      </div>

      <div className="profile-header-card">
        <div className="student-info">
          <div className="avatar-large">👤</div>
          <div>
            <h2>Tanveer Ratul</h2>
            <p>05202601</p>
            <p>Dhaka - 01841499201</p>
          </div>
        </div>
        <button className="download-btn">📥 Download Transcript(PDF)</button>
      </div>

      <div className="highlight-cards">
        <div className="hl-card dark">
          <p>Total Credit Completed:</p>
          <h2>18</h2>
        </div>
        <div className="hl-card light">
          <p>Avg CGPA</p>
          <h2>3.54</h2>
        </div>
      </div>

      <div className="charts-container">
        
        <div className="chart-box">
          <h4>Subject wise perfomance</h4>
          <p>Grade point earned in each completed course</p>
          <div className="chart-wrapper">
            <ResponsiveContainer width="100%" height={250}>
              <BarChart data={barData} margin={{ top: 20, right: 0, left: -20, bottom: 0 }}>
                <XAxis dataKey="name" tick={{fontSize: 10}} angle={-45} textAnchor="end" />
                <YAxis domain={[2, 4]} tick={{fontSize: 12}} />
                <Tooltip />
                <Bar dataKey="grade" fill="#1e223c" radius={[4, 4, 0, 0]} barSize={30} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>

        <div className="chart-box">
          <h4>CGPA vs Class Average</h4>
          <p>This students CGPA compared to average of other students in the class</p>
          <div className="chart-wrapper">
            <ResponsiveContainer width="100%" height={200}>
              <PieChart>
                <Pie
                  data={pieData}
                  cx="50%"
                  cy="50%"
                  innerRadius={60}
                  outerRadius={90}
                  dataKey="value"
                  stroke="none"
                >
                  {pieData.map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={PIE_COLORS[index % PIE_COLORS.length]} />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </div>
          <div className="pie-legends">
            <span><span className="dot dark-dot"></span> This student</span>
            <span><span className="dot light-dot"></span> Class Average</span>
          </div>
        </div>

      </div>

      <table className="student-table profile-table">
        <thead>
          <tr>
            <th>Course Code</th>
            <th>Course Title</th>
            <th>Course Credit</th>
            <th>Grade Point</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>CSE1101</td>
            <td>Structural Programing Language</td>
            <td>3</td>
            <td>4.00</td>
          </tr>
          <tr>
            <td>CSE1102</td>
            <td>Discrete Mathematics</td>
            <td>3</td>
            <td>3.50</td>
          </tr>
          <tr>
            <td>MAT1110</td>
            <td>Differential Equation and Calculas</td>
            <td>3</td>
            <td>3.00</td>
          </tr>
          <tr className="total-row">
            <td colSpan="2"><strong>Total Credit</strong></td>
            <td><strong>18</strong></td>
            <td><strong>3.54</strong></td>
          </tr>
        </tbody>
      </table>

    </div>
  );
}

export default StudentProfile;