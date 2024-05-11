import React from 'react'
import { Routes, Route,  useLocation, useNavigate  } from 'react-router-dom'

import Home from './pages/Home'
import Login from './pages/Login'
import Register from './pages/Register'
import Profile from './pages/Profile'
import Job from './pages/Job'
import JobPost from './pages/JobPost'
import Candidate from './pages/Candidate'
import ProfileView from './pages/ProfileView'

function SiteRoute() {
  return (
    <div>
        <Routes>
            <Route path='/' exact element={<Home/>} />
            <Route path='/login' exact element={<Login/>} />
            <Route path='/signup' exact element={<Register/>} />
            <Route path='/profile' exact element={<Profile/>} />
            <Route path='/job' exact element={<Job/>} />
            <Route path='/jobpost' exact element={<JobPost/>} />
            <Route path='/candidate' exact element={<Candidate/>} />
            <Route path='/profileview/:id' exact element={<ProfileView/>} />

         </Routes>
    </div>
  )
}

export default SiteRoute
