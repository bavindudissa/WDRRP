import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'

function JobComponent() {
    const [jobList, setJobList] = useState('');


    const fetchJobDetails = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Job/all`);
            setJobList(response.data);
            //console.log("data" + response.data.email);
        } catch (error) {
            console.error('Error fetching job details:', error);
        }
    };

    useEffect(() => {
        fetchJobDetails()
    }, []);

    
  return (
    <div>
        <section className="banner-area relative" id="home">	
        <div className="overlay overlay-bg" />
        <div className="container">
            <div className="row search-page-top d-flex align-items-center justify-content-center">
            <div className="banner-content col-lg-12">
                <h1 className="text-white">
                Jobs		
                </h1>
            </div>											
            </div>
        </div>
        </section>
        <section className="post-area section-gap">
        <div className="container">
            <div className="row justify-content-center d-flex">
            <div className="col-lg-6 post-list">
            {jobList && jobList.map(job => (
                <div className="single-post d-flex flex-row">
                <div className="details">
                    <div className="title d-flex flex-row justify-content-between">
                    <div className="titles">
                        <a href=""><h4>{job.title}</h4></a>
                        <h6>{job.company} - {job.workplaceType}</h6>
                        <p>{job.location}</p>					
                    </div>
                    </div>
                    <p>
                    {job.description}
                    </p>
                    <h5>Job Nature: {job.jobType}</h5>
                    <p className="address">Send CV : {job.applicantCollectEmail}</p>
                </div>
                </div>
            ))}
            </div>
            </div>
        </div>	
        </section>

    </div>
  )
}

export default JobComponent
