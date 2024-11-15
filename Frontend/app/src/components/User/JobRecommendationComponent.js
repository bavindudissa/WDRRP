import React, { useState, useEffect } from 'react';
import axios from 'axios';

function JobRecommendationComponent() {
    const [currentPositionJobs, setCurrentPositionJobs] = useState([]);
    const [alternative1Jobs, setAlternative1Jobs] = useState([]);
    const [alternative2Jobs, setAlternative2Jobs] = useState([]);
    const [searchTerm, setSearchTerm] = useState({ current: '', alternative1: '', alternative2: '' });

    useEffect(() => {
        fetchJobRecommendations();
    }, []);

    const fetchJobRecommendations = async () => {
        try {
            const userId = localStorage.getItem('userId');
            const response = await axios.get(`http://localhost:5093/api/Job/recommend/${userId}`);
            const { "Current Position": currentJobs, "Alternative 1": alt1Jobs, "Alternative 2": alt2Jobs } = response.data;

            setCurrentPositionJobs(currentJobs);
            setAlternative1Jobs(alt1Jobs);
            setAlternative2Jobs(alt2Jobs);
        } catch (error) {
            console.error('Error fetching job recommendations:', error);
        }
    };

    const handleSearch = (e, category) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(prev => ({ ...prev, [category]: value }));
    };

    const filterJobs = (jobs, search) => {
        return jobs.filter(job => job.title.toLowerCase().includes(search));
    };

    return (
        <div>
            <section className="banner-area relative" id="home">
                <div className="overlay overlay-bg" />
                <div className="container">
                    <div className="row search-page-top d-flex align-items-center justify-content-center">
                        <div className="banner-content col-lg-12">
                            <h1 className="text-white">Job Recommendations</h1>
                        </div>
                    </div>
                </div>
            </section>

            <section className="post-area section-gap">
                <div className="container">
                    <center>
                        <h2>Best Matchs</h2>
                        <br/>
                        <div className="col-lg-8 mb-4">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search best matches"
                                value={searchTerm.current}
                                onChange={(e) => handleSearch(e, 'current')}
                            />
                        </div>
                    </center>
                    <div className="row">
                        {filterJobs(currentPositionJobs, searchTerm.current).map(job => (
                            <JobCard key={job.id} job={job} />
                        ))}
                    </div>
                    <center>
                        <h2>Alternative 1</h2>
                        <div className="col-lg-8 mb-4">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search Alternative 1 jobs"
                                value={searchTerm.alternative1}
                                onChange={(e) => handleSearch(e, 'alternative1')}
                            />
                        </div>
                    </center>
                    <div className="row">
                        {filterJobs(alternative1Jobs, searchTerm.alternative1).map(job => (
                            <JobCard key={job.id} job={job} />
                        ))}
                    </div>
                    <center>
                        <h2>Alternative 2</h2>
                        <div className="col-lg-8 mb-4">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search Alternative 2 jobs"
                                value={searchTerm.alternative2}
                                onChange={(e) => handleSearch(e, 'alternative2')}
                            />
                        </div>
                    </center>
                    <div className="row">
                        {filterJobs(alternative2Jobs, searchTerm.alternative2).map(job => (
                            <JobCard key={job.id} job={job} />
                        ))}
                    </div>
                </div>
            </section>
        </div>
    );
}

function JobCard({ job }) {
    return (
        <div className="col-lg-6 post-list">
            <div className="single-post d-flex flex-row">
                <div className="details">
                    <div className="title d-flex flex-row justify-content-between">
                        <div className="titles">
                            <h4>{job.title}</h4>
                            <h6>Company: {job.company}</h6>
                        </div>
                    </div>
                    <p className="card-text">Workplace Type: {job.workplaceType}</p>
                    <p className="card-text">Location: {job.location}</p>
                    <p className="card-text">Job Type: {job.jobType}</p>
                    <p className="card-text">Send CV: {job.applicantCollectEmail}</p>
                    <p className="card-text">Description: {job.description}</p>
                    <p className="card-text">Posted By: <a href={`/profileview/${job.userId}`}>{job.userName}</a></p>
                    <button type="button" className="btn btn-primary">
                        Apply Job
                    </button>
                </div>
            </div>
        </div>
    );
}

export default JobRecommendationComponent;
