import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router-dom'

function ProfileViewComponet() {
    let { id } = useParams();
    const [skillList, setSkillList] = useState('');
    const [exList, setExList] = useState('');
    const [eduList, setEduList] = useState('');
    const [cList, setCList] = useState('');

    const fetchUserSkills = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Skill/all/${id}`);

            setSkillList(response.data)
        } catch (error) {
            console.error('Error fetching skill details:', error);
            // setError('Error fetching user details');
        }
    };

    const fetchUserExs = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Experience/all/${id}`);

            setExList(response.data)
        } catch (error) {
            console.error('Error fetching experience details:', error);
            // setError('Error fetching user details');
        }
    };

    const fetchUserEdu = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Education/all/${id}`);

            setEduList(response.data)
        } catch (error) {
            console.error('Error fetching education details:', error);
            // setError('Error fetching user details');
        }
    };

    const fetchCDetails = async () => {
        try {
  
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/User/${id}`);
            setCList(response.data);
            console.log("data" + response.data);
        } catch (error) {
            console.error('Error fetching cadidate details:', error);
        }
    };

    useEffect(() => {
        fetchUserSkills()
        fetchUserExs()
        fetchUserEdu()
        fetchCDetails()
    }, []);

  return (
    <div>
        <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
        </div>
        <br/>
        <section className="blog-posts-area section-gap">
            <div className="container">
                <div className="row">
                <div className="col-lg-4 sidebar">
                    <div className="single-widget protfolio-widget">
                    <a href="#"><h4>{cList.firstName} {cList.lastName}</h4></a>
                    <p>
                    {cList.email}
                    </p>
                    <p>
                    {cList.contactNumber}
                    </p>							
                    </div>
                    <div className="card">
                <div className="card-body">
                    <div className="d-flex flex-column align-items-center text-center">
                    <div className="mt-1">
                        <h5>Skills</h5>
                    </div>
                    </div>
                    <hr className="my-4" />
                    {skillList && skillList.map(skill => (
                        <div className='row'>
                            <div className='col-8'>
                            <p>{skill.skill1}</p>
                            </div>
                            
                        </div>
                    ))}
                </div>
                </div>
                </div>
                <div className='col-lg-8'>
                    <div className="card">
                            <div className='card-header'>
                                <div className="row">
                                    <div className="col-sm-9">
                                        <h5 className="d-flex align-items-center mb-3">Experiences</h5>
                                    </div>
                                </div>
                            </div>
                            <div className="card-body">
                                {exList && exList.map(ex => (
                                <div className="single-post d-flex flex-row">
                                    <div className="details">
                                        <div className="title d-flex flex-row justify-content-between">
                                            <div className="titles">
                                                <h5>{ex.title}</h5>
                                                <h6>{ex.companyName}</h6>
                                                <p>{ex.location}</p>		
                                                <p>Start Date : {ex.startDate} <span className='ml-4'>End Date : {ex.endDate}</span></p>			
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                ))}

                            </div>
                    </div>
                    <br/>
                    <div className="card">
                            <div className='card-header'>
                                <div className="row">
                                    <div className="col-sm-9">
                                        <h5 className="d-flex align-items-center mb-3">Education Details</h5>
                                    </div>
                                </div>
                            </div>
                            <div className="card-body">
                                {eduList && eduList.map(ex => (
                                <div className="single-post d-flex flex-row">
                                    <div className="details">
                                        <div className="title d-flex flex-row justify-content-between">
                                            <div className="titles">
                                                <h5>{ex.school}</h5>
                                                <h6>{ex.degree}</h6>
                                                <p>{ex.grade}</p>
                                                <p>{ex.fieldOfStudy}</p>		
                                                <p>Start Date : {ex.startDate} <span className='ml-4'>End Date : {ex.endDate}</span></p>	
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                ))}

                            </div>
                            </div>
                </div>
                </div>
            </div>	
    </section>
    </div>
  )
}

export default ProfileViewComponet
