import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    // Make API call to authenticate user
    axios.post('http://localhost:5093/api/User/login', { email, password })
      .then(response => {
        // Handle successful login
        localStorage.setItem('userId', response.data.id);
        console.log('Login successful', response.data);
        navigate("/");
      })
      .catch(error => {
        // Handle login error
        console.error('Login error', error);
      });
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
                    <h2 className="fw-bold mb-2 text-uppercase" style={{ color: '#795fff' }}>Login</h2>
                    <p className="text-white-50 mb-3">Please enter your login and password!</p>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="text" className="form-control" name="email" placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
                    </div>
                    <div data-mdb-input-init className="form-outline form-white mb-4">
                      <input type="password" className="form-control" name="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
                    </div>
                    <button type="button" className="btn btn-info" onClick={handleLogin}>Login</button>
                  </div>
                  <div>
                    <p className="mb-0">Don't have an account? <a href="/signup" className="text-white-50 fw-bold" style={{ color: '#795fff' }}>Sign Up</a>
                    </p>
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

export default Login;
