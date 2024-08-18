import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Modal from 'react-modal';

function JobComponent() {
    var userId = localStorage.getItem('userId');
    const [jobList, setJobList] = useState([]);
    const [filteredJobList, setFilteredJobList] = useState([]);
    const [selectedJob, setSelectedJob] = useState(null);
    const [file, setFile] = useState(null);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        fetchJobDetails();
    }, []);

    const fetchJobDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5093/api/Job/all');
            setJobList(response.data);
            setFilteredJobList(response.data); // Initially, the filtered list is the same as the full list
        } catch (error) {
            console.error('Error fetching job details:', error);
        }
    };

    const openModal = (job) => {
        setSelectedJob(job);
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
        setFile(null);
    };

    const handleFileChange = (e) => {
        setFile(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const formData = new FormData();
            formData.append('jobId', selectedJob.id);
            formData.append('userId', localStorage.getItem('userId'));
            formData.append('file', file);

            const response = await axios.post('http://localhost:5093/api/JobApplied', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });

            alert(response.data);
            closeModal();
        } catch (error) {
            if (error.response && error.response.data) {
                alert(error.response.data);  // Display the error message from the backend
            } else {
                alert('Failed to apply for the job');
            }
        }
    };

    const handleSearch = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(value);

        const filtered = jobList.filter(job =>
            job.title.toLowerCase().includes(value)
        );
        setFilteredJobList(filtered);
    };

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
                        <div className="col-lg-8 mb-4">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search job by name"
                                value={searchTerm}
                                onChange={handleSearch}
                            />
                        </div>
                        {filteredJobList && filteredJobList.map(job => (
                            <div className="col-lg-6 post-list" key={job.id}>
                                <div className="single-post d-flex flex-row">
                                    <div className="details">
                                        <div className="title d-flex flex-row justify-content-between">
                                            <div className="titles">
                                                <h4>{job.title}</h4>
                                                <h6>Company: {job.company}</h6>
                                            </div>
                                        </div>
                                        <p className="card-text">Work Place Scale: {job.workplaceType}</p>
                                        <p className="card-text">Location: {job.location}</p>
                                        <p className="card-text">Job Type: {job.jobType}</p>
                                        <p className="card-text">Send CV : {job.applicantCollectEmail}</p>
                                        <p className="card-text">Description: {job.description}</p>
                                        <p className="card-text">Posted By: <a href={`/profileview/${job.userId}`}>{job.userName}</a></p>
                                        {userId &&<button type="button" className="btn btn-primary" onClick={() => openModal(job)}>
                                            Apply Job
                                        </button>}
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </section>

            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Apply for Job"
                ariaHideApp={false}
                style={{
                    content: {
                        top: '50%',
                        left: '50%',
                        right: 'auto',
                        bottom: 'auto',
                        marginRight: '-50%',
                        transform: 'translate(-50%, -50%)',
                        width: '50%',
                        padding: '20px',
                    },
                }}
            >
                <h3>Apply for {selectedJob?.title}</h3>
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Upload CV</label>
                        <br/>
                        <input type="file" onChange={handleFileChange} required />
                    </div>
                    <button type="submit" className="btn btn-success" style={{marginRight:"10px"}}>Submit Application</button>
                    <button type="button" className="btn btn-secondary" onClick={closeModal}>Cancel</button>
                </form>
            </Modal>
        </div>
    );
}

export default JobComponent;
