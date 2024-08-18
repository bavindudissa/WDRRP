import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Modal from 'react-modal'; // Import react-modal or another modal library
import { useParams } from 'react-router-dom';

function MyJobComponent() {
    const [jobApplyList, setJobApplyList] = useState([]);
    const [filteredJobApplyList, setFilteredJobApplyList] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [selectedApplication, setSelectedApplication] = useState(null);
    const [status, setStatus] = useState('');
    const [modalIsOpen, setModalIsOpen] = useState(false);

    useEffect(() => {
        fetchJobApplyDetails();
    }, []);

    const fetchJobApplyDetails = async () => {
        try {
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            const response = await axios.get('http://localhost:5093/api/JobApplied/user/' + user);
            setJobApplyList(response.data);
            setFilteredJobApplyList(response.data); 
        } catch (error) {
            console.error('Error fetching job apply details:', error);
        }
    };

    const handleSearch = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(value);

        const filtered = jobApplyList.filter(apply =>
            `${apply.jobDto.title.toLowerCase()}`.includes(value)
        );
        setFilteredJobApplyList(filtered);
    };

    const openModal = (apply) => {
        setSelectedApplication(apply);
        setStatus(apply.status);
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
        setSelectedApplication(null);
        setStatus('');
    };
  return (
    <div>
    <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
    </div>
    <section className="banner-area relative" id="home">
                <div className="overlay overlay-bg" />
                <div className="container">
                    <div className="row d-flex align-items-center justify-content-center">
                        <div className="about-content col-lg-12">
                            <h1 className="text-white">
                                My Job
                            </h1>
                        </div>
                    </div>
                </div>
            </section>
    <br/>
    <div className="container">
        <div className="main-body">
            <div className="row justify-content-center d-flex">
                <div className="col-lg-8 mb-4">
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Search by job name"
                        value={searchTerm}
                        onChange={handleSearch}
                    />
                </div>
                <div className="col-lg-12 post-list">
                    {filteredJobApplyList && filteredJobApplyList.map(apply => (
                        <div key={apply.id} className="card mb-3">
                            <div className="row no-gutters">
                                <div className="col-md-6">
                                    <div className="card-body">
                                        <h5 className="card-title">{apply.jobDto.title}</h5>
                                        <p className="card-text">Job Type: {apply.jobDto.jobType}</p>
                                        <p className="card-text">Company: {apply.jobDto.company}</p>
                                        <p className="card-text">Work Place Scale: {apply.jobDto.workplaceType}</p>
                                        <p className="card-text">Description: {apply.jobDto.description}</p>
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <div className="card-body">
                                        <h5 className="card-title">Contact Details</h5>
                                        <p className="card-text">Email: {apply.jobDto.applicantCollectEmail}</p>
                                        <p className="card-text">Location: {apply.jobDto.location}</p>
                                        <p className="card-text">Posted By: <a href={`/profileview/${apply.jobDto.userId}`}>{apply.jobDto.userName}</a></p>
                                        <h6 className="card-title">Status: {apply.status}</h6>
                                        <p className="card-text"><small className="text-muted">My CV: <a href={`http://localhost:5093/files/${apply.url}`} target="_blank" rel="noopener noreferrer">{apply.url}</a></small></p>
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
  )
}

export default MyJobComponent