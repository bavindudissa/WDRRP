import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Register() {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [dob, setDOB] = useState('');
  const [gender, setGender] = useState('');
  const [contactNumber, setContactNumber] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  const handleSignUp = () => {
    if (!firstName || !lastName || !dob || !gender || !contactNumber || !email || !password) {
      alert("Please fill out all required fields.");
      return;
    }
    const formData = {
        FirstName: firstName,
        LastName: lastName,
        DateOfBirth: dob,
        Gender: gender,
        ContactNumber: contactNumber,
        Email: email,
        Password: password
    };

    const customData = {
        UserStatusId: 1,
        UserTypeId: 1
    };

    const requestData = {
      ...formData,
      ...customData
    };

    axios.post('http://localhost:5093/api/User', requestData)
      .then(response => {
        console.log('Registration successful');
        navigate("/login");
      })
      .catch(error => {
        console.error('Registration failed:', error);
      });
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div>
      <section style={{ backgroundColor: '#795fff', height: '100vh' }}>
        <div className="container py-5 h-100">
          <div className="row d-flex justify-content-center align-items-center h-100">
            <div className="col-12 col-md-8 col-lg-6 col-xl-5">
              <div className="card bg-dark text-white" style={{ borderRadius: '1rem' }}>
                <div className="card-body p-5 text-center">
                  <div className="mb-md-2 mt-md-2 pb-2">
                    <h2 className="fw-bold mb-2 text-uppercase" style={{ color: '#795fff' }}>Sign Up</h2>
                    <p className="text-white-50 mb-3">Please enter your details to create an account!</p>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="text" className="form-control" name="firstName" placeholder="First Name" onChange={(e) => setFirstName(e.target.value)} />
                    </div>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="text" className="form-control" name="lastName" placeholder="Last Name" onChange={(e) => setLastName(e.target.value)} />
                    </div>
                    <div className='row'>
                      <div className='col-6'>
                        <div data-mdb-input-init className="form-outline form-white mb-4">
                          <input type="date" className="form-control" name="dob" placeholder="Date of Birth" onChange={(e) => setDOB(e.target.value)} />
                        </div>
                      </div>
                      <div className='col-6'>
                        <div data-mdb-input-init className="form-outline form-white mb-4">
                          <select className="form-control" aria-label="Gender" onChange={(e) => setGender(e.target.value)}>
                            <option selected disabled>Select Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                          </select>
                        </div>
                      </div>
                    </div>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="text" className="form-control" name="contactNumber" placeholder="Contact Number" onChange={(e) => setContactNumber(e.target.value)} />
                    </div>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="text" className="form-control" name="email" placeholder="Email" onChange={(e) => setEmail(e.target.value)} />
                    </div>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <div className="input-group">
                        <input
                          type={showPassword ? "text" : "password"}
                          className="form-control"
                          name="password"
                          placeholder="Password"
                          onChange={(e) => setPassword(e.target.value)}
                        />
                        <div className="input-group-append">
                          <button
                            className="btn btn-outline-secondary"
                            type="button"
                            onClick={toggleShowPassword}
                          >
                            {showPassword ? "Hide" : "Show"}
                          </button>
                        </div>
                      </div>
                    </div>
                    <button type="button" className="btn btn-info" onClick={handleSignUp}>Sign Up</button>
                  </div>
                  <div>
                    <p className="mb-0">Do you have an account? <a href="/login" className="text-white-50 fw-bold" style={{ color: '#795fff' }}>Login</a></p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Register;
