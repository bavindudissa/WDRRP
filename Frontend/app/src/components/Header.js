import { React, useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom';

function Header() {
    var userId = localStorage.getItem('userId');
    const navigate = useNavigate();

    const handleLogout = () => {
        // Clear localStorage
        localStorage.clear();
    
        // Navigate to another page (replace '/login' with your desired path)
        navigate("/");
      };

      const handleChat = () => {
        // Navigate to another page (replace '/login' with your desired path)
        navigate("/chatbot");
      };


    return (
        <div style={{ backgroundColor: 'black' }}>
            <header id="header">
                <div className="container">
                    <div className="row align-items-center justify-content-between d-flex">
                        <div id="logo">
                            <h3 style={{color:"white"}}>Jobzen</h3>
                        </div>
                        <nav id="nav-menu-container">
                            <ul className="nav-menu">
                                <li className="menu-active"><a href="/">Home</a></li>
                                <li><a href="/job">Job</a></li>
                                <li><a href="/candidate">Candidates</a></li>
                                {userId &&<li><a href="/profile">Profile</a></li>}
                                {userId &&<li><a href="/jobpost">Job Post</a></li>}
                                {userId &&<li><a href="/jobrecommendation">Job Recommendations</a></li>}
                                {userId &&<li><a href="/myjob">My Job</a></li>}
                                {userId && <li><a className="ticker-btn" style={{backgroundColor: 'limegreen', color: 'white', cursor: 'pointer' }} onClick={handleChat}>Chat bot</a></li>}
                                {!userId && <li><a className="ticker-btn" href="/signup" style={{cursor: 'pointer' }}>Signup</a></li>}
                                {!userId && <li><a className="ticker-btn" href="/login" style={{cursor: 'pointer' }}>Login</a></li>}
                                {userId && <li><a className="ticker-btn" onClick={handleLogout} style={{cursor: 'pointer' }}>Logout</a></li>}
                            </ul>
                        </nav>
                    </div>
                </div>
            </header>
        </div>
    );
}

export default Header;
