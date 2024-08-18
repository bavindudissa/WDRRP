import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Modal from 'react-modal'; // Import react-modal or another modal library
import { useParams } from 'react-router-dom';

function JobPostApplyComponent() {
    let { id } = useParams();
    const [jobApplyList, setJobApplyList] = useState([]);
    const [job, setJob] = useState(null);
    const [filteredJobApplyList, setFilteredJobApplyList] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [selectedApplication, setSelectedApplication] = useState(null);
    const [status, setStatus] = useState('');
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const [formData, setFormData] = useState({
        title: '',
        company: '',
        location: '',
        workplaceType: '',
        jobType: '',
        description: '',
        applicantCollectEmail: '',
        userId: ''
    });


    useEffect(() => {
        fetchJobApplyDetails();
        fetchJobDetails()
    }, []);

    const fetchJobDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5093/api/Job/' + id);
            setJob(response.data);
            setFormData({
                title: response.data.title,
                company: response.data.company,
                location: response.data.location,
                workplaceType: response.data.workplaceType,
                jobType: response.data.jobType,
                description: response.data.description,
                applicantCollectEmail: response.data.applicantCollectEmail,
                userId: response.data.userId
            });
        } catch (error) {
            console.error('Error fetching job details:', error);
        }
    };

    const fetchJobApplyDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5093/api/JobApplied/job/' + id);
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
            `${apply.userDto.firstName.toLowerCase()} ${apply.userDto.lastName.toLowerCase()}`.includes(value)
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

    const handleStatusUpdate = async () => {
        if (!selectedApplication) return;

        try {
            const response = await axios.put(`http://localhost:5093/api/JobApplied/${selectedApplication.id}`, {
                id: selectedApplication.id,
                status: status
            });

            alert(response.data);
            closeModal();
            fetchJobApplyDetails(); // Refresh the job applications list
        } catch (error) {
            console.error('Error updating job application status:', error);
            alert('Failed to update status');
        }
    };

    return (
        <div>
            <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
            </div>
            <br/>
            <div className="container">
                <div className="main-body">
                <div className="row">
                        <div className="col-lg-12">
                            <div className="card">
                                <div className="card-body">
                                    <h5 className="d-flex align-items-center mb-3">Job Details</h5>
                                    <hr className="my-4" />
                                    <div className="row mb-3">
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Title</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <input type="text" className="form-control" name='title' value={formData.title} disabled />
                                        </div>
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Company</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <input type="text" className="form-control" name='company' value={formData.company} disabled />
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Work Place Scale</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <select className="form-control" name='workplaceType' value={formData.workplaceType} disabled>
                                                <option selected>Select option</option>
                                                <option value="<10">0-10</option>
                                                <option value="10-20">10-20</option>
                                                <option value="20-50">20-50</option>
                                                <option value="50-100">50-100</option>
                                                <option value=">100">More than 100</option>
                                            </select>
                                        </div>
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Location</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <input type="text" className="form-control" name='location' value={formData.location} disabled />
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Job Type</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <select className="form-control" name='jobType' value={formData.jobType} disabled>
                                                <option selected>Select option</option>
                                                <option value="fulltime">Full time</option>
                                                <option value="contract">Contract</option>
                                                <option value="parttime">Part time</option>
                                            </select>
                                        </div>
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Description</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <textarea className="form-control" name='description' value={formData.description} disabled />
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <div className="col-sm-2">
                                            <h6 className="mb-0">Email</h6>
                                        </div>
                                        <div className="col-sm-4 text-secondary">
                                            <input type="text" className="form-control" name='applicantCollectEmail' value={formData.applicantCollectEmail} disabled />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div className="row justify-content-center d-flex">
                        <div className="col-lg-8 mb-4">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search by first or last name"
                                value={searchTerm}
                                onChange={handleSearch}
                            />
                        </div>
                        <div className="col-lg-12 post-list">
                            {filteredJobApplyList && filteredJobApplyList.map(apply => (
                                <div key={apply.id} className="card mb-3">
                                    <div className="row no-gutters">
                                        <div className="col-md-2">
                                            {apply.userDto.profilePic ? (
                                                <img src={`http://localhost:5093/files/${apply.userDto.profilePic}`} className="card-img" alt="Profile" style={{ width: '150px', height: '150px'}} />
                                            ) : (
                                                <p>No profile picture uploaded</p>
                                            )}
                                        </div>
                                        <div className="col-md-4">
                                            <div className="card-body">
                                                <h5 className="card-title">{apply.userDto.firstName} {apply.userDto.lastName}</h5>
                                                <p className="card-text">Email: {apply.userDto.email}</p>
                                                <p className="card-text">Contact Number: {apply.userDto.contactNumber}</p>
                                                <p className="card-text">Status: <strong>{apply.status}</strong></p>
                                            </div>
                                        </div>
                                        <div className="col-md-4">
                                            <div className="card-body">
                                                <h5 className="card-title">Bio</h5>
                                                <p className="card-text">Gender: {apply.userDto.gender}</p>
                                                <p className="card-text">Date of Birth: {apply.userDto.dateOfBirth}</p>
                                                <p className="card-text"><small className="text-muted">CV: <a href={`http://localhost:5093/files/${apply.url}`} target="_blank" rel="noopener noreferrer">{apply.url}</a></small></p>
                                            </div>
                                        </div>
                                        <div className="col-md-2">
                                            <div className="card-body">
                                            <p className="card-text">Matching: <strong>{apply.matchPercentage + "%"}</strong></p>
                                                <a href={`/profileview/${apply.userId}`}>
                                                    <button type="button" className="btn btn-primary">
                                                        View Profile
                                                    </button>
                                                </a>
                                                <button 
                                                    type="button" 
                                                    className="btn btn-success" 
                                                    style={{marginTop:"10px"}}
                                                    onClick={() => openModal(apply)}
                                                >
                                                    Update Status
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>

            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Update Status"
                ariaHideApp={false}
                style={{
                    content: {
                        top: '50%',
                        left: '50%',
                        right: 'auto',
                        bottom: 'auto',
                        marginRight: '-50%',
                        transform: 'translate(-50%, -50%)',
                        width: '400px',
                        padding: '20px',
                    },
                }}
            >
                <h3>Update Status</h3>
                <div>
                    <label>Status</label>
                    <select 
                        className="form-control" 
                        value={status} 
                        onChange={(e) => setStatus(e.target.value)}
                    >
                        <option value="Applied">Applied</option>
                        <option value="Sort Listed">Sort Listed</option>
                        <option value="In Review">In Review</option>
                        <option value="Interviewed">Interviewed</option>
                        <option value="Offered">Offered</option>
                        <option value="Rejected">Rejected</option>
                    </select>
                </div>
                <br />
                <button className="btn btn-primary" onClick={handleStatusUpdate}>Update</button>
                <button className="btn btn-secondary" onClick={closeModal} style={{ marginLeft: '10px' }}>Cancel</button>
            </Modal>
        </div>
    )
}

export default JobPostApplyComponent;
