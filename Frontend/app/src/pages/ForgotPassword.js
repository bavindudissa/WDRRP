import React, { useState } from 'react';
import axios from 'axios'; // Import Axios

function ForgotPassword() {
    const [email, setEmail] = useState('');
    const [error, setError] = useState('');
     // Function to handle form submission
     const handleSubmit = async (e) => {
        if(!email){
            alert('Fill email');
            return;
        }
        e.preventDefault();
        try {
            // API endpoint where the forgot password request is processed
            const response = await axios.post('http://localhost:5093/api/User/forgot-password', { 
                email: email,
                resetUrl: 'http://localhost:3000/resetpassword'
             });
             // Display success message from server
            // Optional: navigate to another route if needed
            // navigate('/some-route');
            alert('Reset link email sent successful.')
        } catch (error) {
            if (error.response) {
                // The request was made and the server responded with a status code
                // that falls out of the range of 2xx
                alert(error.response.data);
            } else if (error.request) {
                // The request was made but no response was received
                alert('No response from server');
            } else {
                // Something happened in setting up the request that triggered an Error
                alert('Error:', error.response.data);
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
                  <h2 className="fw-bold mb-2 text-uppercase" style={{ color: '#795fff' }}>Forgot Password</h2>
                  <p className="text-white-50 mb-3">Please enter your email!</p>
                  <div data-mdb-input-init className="form-outline form-white mb-4">
                    <input
                      type="text"
                      className="form-control"
                      name="email"
                      placeholder="Email"
                      value={email}
                      onChange={e => setEmail(e.target.value)}
                    />
                  </div>
                  <button type="button" className="btn btn-info" onClick={handleSubmit} style={{marginTop:"10px"}}>Get password reset email</button>
                </div>
                <div>
                  <p className="mb-0">Don't have an account? <a href="/signup" className="text-white-50 fw-bold" style={{ color: '#795fff' }}>Sign Up</a></p>
                  {error && <div className="alert alert-danger" role="alert">{error}</div>}
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

export default ForgotPassword