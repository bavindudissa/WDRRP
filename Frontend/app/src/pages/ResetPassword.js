import React, { useState, useEffect } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import axios from 'axios'; // Import Axios

function ResetPassword() {
    const [password, setPassword] = useState('');
    const [passwordConfirm, setPasswordConfirm] = useState('');
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);
    const [showPasswordConfirm, setShowPasswordConfirm] = useState(false);
    const [email, setEmail] = useState('');

    const location = useLocation();

    useEffect(() => {
        // Extract the email from the query parameters
        const queryParams = new URLSearchParams(location.search);
        const emailFromQuery = queryParams.get('ref');
        if (emailFromQuery) {
            setEmail(emailFromQuery);
        }
    }, [location]);

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    const togglePasswordVisibilityConfirm = () => {
        setShowPasswordConfirm(!showPasswordConfirm);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (password !== passwordConfirm) {
            alert("Passwords do not match.");
            return;
        }

        try {
            const response = await axios.post(`http://localhost:5093/api/User/reset-password`, { 
                password: password,
                email: email
            });
            alert('Password has been reset successfully.');
            navigate('/login'); // Redirect to login page after successful reset
        } catch (error) {
            if (error.response) {
                alert(error.response.data);
            } else {
                alert('An error occurred. Please try again.');
            }
        }
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
                  <h2 className="fw-bold mb-2 text-uppercase" style={{ color: '#795fff' }}>Reset Password</h2>
                  <p className="text-white-50 mb-3">Please enter your new password!</p>
                  <div data-mdb-input-init className="form-outline form-white mb-4">
                    <div className="input-group">
                      <input
                        type={showPassword ? "text" : "password"}
                        className="form-control"
                        name="password"
                        placeholder="New Password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                      />
                      <div className="input-group-append">
                        <button
                          className="btn btn-outline-secondary"
                          type="button"
                          onClick={togglePasswordVisibility}
                        >
                          {showPassword ? "Hide" : "Show"}
                        </button>
                      </div>
                    </div>
                  </div>
                  <div data-mdb-input-init className="form-outline form-white mb-4">
                    <div className="input-group">
                      <input
                        type={showPasswordConfirm ? "text" : "password"}
                        className="form-control"
                        name="password"
                        placeholder="Confirm New Password"
                        value={passwordConfirm}
                        onChange={e => setPasswordConfirm(e.target.value)}
                      />
                      <div className="input-group-append">
                        <button
                          className="btn btn-outline-secondary"
                          type="button"
                          onClick={togglePasswordVisibilityConfirm}
                        >
                          {showPasswordConfirm ? "Hide" : "Show"}
                        </button>
                      </div>
                    </div>
                  </div>
                  <button type="button" className="btn btn-info" onClick={handleSubmit} style={{marginTop:"10px"}}>Reset Password</button>
                </div>
                <div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
  )
}

export default ResetPassword