import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'

function JobPostComponents() {

    const [jobList, setJobList] = useState('');
    const [jobId, setJobId] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [filteredJobList, setFilteredJobList] = useState([]);


    const [formData, setFormData] = useState({
        title: '',
        company: '',
        location: '',
        workplaceType: '',
        location: '',
        jobType: '',
        description: '',
        applicantCollectEmail: '',
        userId: ''
    });

    const [error, setError] = useState('');
    const [sucess, setSucess] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const fetchJobDetails = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Job/all/${user}`);
            setJobList(response.data);
            setFilteredJobList(response.data);
            //console.log("data" + response.data.email);
        } catch (error) {
            console.error('Error fetching job details:', error);
        }
    };

    useEffect(() => {
        fetchJobDetails()
    }, []);

    const handleCancel = () => {
        setError('')
        setSucess('')
        fetchJobDetails()
      };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const user = localStorage.getItem('userId');
        console.log('User ID from local storage:', user);

        setFormData({
            ...formData,
            userId:user
        });

        // Check if all fields are filled
        for (const key in formData) {
            if (!formData[key]) {
                setError(`Please fill out ${key}`);
                setSucess('')
                return;
            }
        }
        try {   
            if(jobId){
                const customData = {
                    id: jobId
                  };
                setFormData({
                    ...formData,
                    id:jobId,
                    ...customData
                });
                const response = await axios.put(`http://localhost:5093/api/Job/${jobId}`, formData);
                console.log(response.data); // Assuming the response contains success message or token
                // Redirect or show success message
                setError('')
                setSucess('Update job', sucess);
                fetchJobDetails()
            }
            else{
                setFormData({
                    ...formData,
                    userId:user
                });
                const response = await axios.post(`http://localhost:5093/api/Job`, formData);
                console.log(response.data); // Assuming the response contains success message or token
                // Redirect or show success message
                setError('')
                setSucess('Create job', sucess);
                setFormData({
                    ...formData,
                    title: '',
                    company: '',
                    location: '',
                    workplaceType: '',
                    location: '',
                    jobType: '',
                    description: '',
                    applicantCollectEmail: '',
                    userId: ''
                });
                fetchJobDetails()
            }
        } catch (err) {
            if (err.response && err.response.data && err.response.data.message) {
                setError(err.response.data.message);
                setSucess('')
            } else {
                setError('Something went wrong. Please try again.');
                setSucess('')
            }
        }
    };


    const handleDelete = async (Id) => {
        try {
            await axios.delete(`http://localhost:5093/api/Job/${Id}`);
            fetchJobDetails()
        } catch (error) {
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
       <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
        </div>
        <section className="banner-area relative" id="home">
                <div className="overlay overlay-bg" />
                <div className="container">
                    <div className="row d-flex align-items-center justify-content-center">
                        <div className="about-content col-lg-12">
                            <h1 className="text-white">
                                Post Job
                            </h1>
                        </div>
                    </div>
                </div>
            </section>
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
                        <input type="text" className="form-control" name='title' value={formData.title} onChange={handleChange}/>
                    </div>
                    <div className="col-sm-2">
                        <h6 className="mb-0">Company</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='company' value={formData.company} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">Work Place Scale</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <select className="form-control" aria-label="Gender" name='workplaceType' value={formData.workplaceType} onChange={handleChange}>
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
                        <input type="text" className="form-control" name='location' value={formData.location} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">Job Type</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <select className="form-control" aria-label="Gender" name='jobType' value={formData.jobType} onChange={handleChange}>
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
                        <textarea type="text" className="form-control" name='description' value={formData.description} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">Email</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='applicantCollectEmail' value={formData.applicantCollectEmail} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row">
                    <div className="col-sm-6">
              
                    </div>
                    <div className="col-sm-6 text-secondary">
                    <input type="button" className="btn btn-danger px-4 ml-2 float-right" defaultValue="Cancel" onClick={handleCancel}/>

                        <input type="button" className="btn btn-primary px-4 float-right" defaultValue="Save Changes" onClick={handleSubmit} />
                    </div>
                    {error && <p className="text-danger">{error}</p>}
                    {sucess && <p className="text-primary">{sucess}</p>}

                    </div>
                </div>
                </div>
                <br/>
                <br/>
                <br/>
                <br/>
            </div>
            </div>
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
            <div className="col-lg-12 post-list">
            {filteredJobList && filteredJobList.map(job => (
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
                     <a class="text-primary text-xs mr-4"
                     style={{cursor: 'pointer'}}
                     href={`/job/apply/${job.id}`}>
                        View Job Applies
                     </a>
                    <a class="text-primary text-xs mr-4"
                        style={{cursor: 'pointer'}}
                        onClick={() => {
                            setFormData({
                                ...formData,
                                title: job.title,
                                company: job.company,
                                location: job.location,
                                workplaceType: job.workplaceType,
                                jobType: job.jobType,
                                description: job.description,
                                applicantCollectEmail: job.applicantCollectEmail,
                                userId: job.userId
                            });
                            setJobId(job.id);
                        }}>Edit</a>
                    <a class="text-danger text-xs"
                         style={{cursor: 'pointer'}}
                                     onClick={() => handleDelete(job.id)}>Delete</a>
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

export default JobPostComponents
