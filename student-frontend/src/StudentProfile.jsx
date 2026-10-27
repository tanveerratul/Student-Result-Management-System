function StudentProfile({ setActivePage }) {
  return (
    <div style={{ padding: '20px' }}>
      <h2>Student Profile Page (Design is coming...)</h2>
      <button onClick={() => setActivePage('dashboard')}>Go Back</button>
    </div>
  );
}

export default StudentProfile;