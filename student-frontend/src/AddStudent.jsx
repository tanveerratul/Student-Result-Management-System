function AddStudent() {
  return (
    <div className="add-student-container">
      <h2 className="page-title">Register a New Student</h2>
      
      {/* Basic Details Section */}
      <div className="form-section">
        <h3>Basic Details</h3>
        <div className="input-grid">
          <div className="input-group">
            <label>Full Name:</label>
            <input type="text" placeholder="e.g. Tanveer Ratul" />
          </div>
          <div className="input-group">
            <label>Student ID:</label>
            <input type="text" placeholder="e.g. 05202601" />
          </div>
          <div className="input-group">
            <label>Contact No.</label>
            <input type="text" placeholder="+880 1****-*****" />
          </div>
          <div className="input-group">
            <label>Address:</label>
            <input type="text" placeholder="Street, City" />
          </div>
          <div className="input-group">
            <label>Division:</label>
            <select>
              <option>Select Your Division</option>
              <option>Dhaka</option>
              <option>Chattogram</option>
              <option>Rajshahi</option>
              <option>Khulna</option>
              <option>Barisal</option>
              <option>Sylhet</option>
              <option>Rangpur</option>
              <option>Mymensingh</option>
            </select>
          </div>
        </div>
      </div>

      {/* Academic Results Section */}
      <div className="form-section">
        <h3>Academic Results</h3>
        <div className="course-row">
          <div className="input-group">
            <label>Course Title</label>
            <input type="text" placeholder="e.g. Structural Programming Language" />
          </div>
        </div>
        <div className="course-row multi-input">
          <div className="input-group">
            <label>Course Code:</label>
            <input type="text" placeholder="CSE-1101" />
          </div>
          <div className="input-group">
            <label>Course Credit:</label>
            <input type="text" placeholder="3" />
          </div>
          <div className="input-group">
            <label>Grade Point:</label>
            <input type="text" placeholder="4.00" />
          </div>
          <button className="delete-btn">🗑️</button>
        </div>
        
        <button className="add-course-btn">+ Add Another Course</button>
      </div>

      {/* Form Actions */}
      <div className="form-actions">
        <button className="cancel-btn">Cancel</button>
        <button className="save-btn">Save Student</button>
      </div>
    </div>
  );
}

export default AddStudent;