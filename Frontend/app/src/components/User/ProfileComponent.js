import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'


function ProfileComponent() {
    const navigate = useNavigate();
    const customData = {
        userType: 1,
        userStatusId: 1
      };
    const customDataEx = {
        employmentTypeId: 1,
        industry: 'IT',
        description: 'des'
    };
    const [formData, setFormData] = useState({
        firstname: '',
        lastname: '',
        email: '',
        password: '',
        contactNumber: '',
        dateOfBirth: '',
        gender: '',
        id : null,
        ...customData
    });
    const [formDataEx, setFormDataEx] = useState({
        title: '',
        companyName: '',
        location: '',
        startDate: '',
        endDate: '',
        userId: '',
        ...customDataEx
    });
    const [formDataEdu, setFormDataEdu] = useState({
        school: '',
        degree: '',
        fieldOfStudy: '',
        startDate: '',
        endDate: '',
        grade: '',
        userId: ''
    });
    const [error, setError] = useState('');
    const [sucess, setSucess] = useState('');
    const [skillList, setSkillList] = useState('');
    const [exList, setExList] = useState('');
    const [eduList, setEduList] = useState('');
    const [skillName, setSkillName] = useState('');

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleChangeEx = (e) => {
        console.log(e)
        setFormDataEx({ ...formDataEx, [e.target.name]: e.target.value });
    };

    const handleChangeEdu = (e) => {
        console.log(e)
        setFormDataEdu({ ...formDataEdu, [e.target.name]: e.target.value });
    };

    const handleSkillNameChange = (e) => {
        setSkillName(e.target.value);
      };

    const fetchUserDetails = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/User/${user}`);
            //setUserDetails(response.data.data);
            console.log("data" + response.data.email);

            setFormData({
                ...formData,
                firstname: response.data.firstName || '',
                lastname: response.data.lastName || '',
                email: response.data.email || '',
                contactNumber : response.data.contactNumber || '',
                dateOfBirth : response.data.dateOfBirth || '',
                gender : response.data.gender || '',
                password : response.data.password || '',
                id : response.data.id || null,
            });
        } catch (error) {
            console.error('Error fetching user details:', error);
            setSucess('')
            // setError('Error fetching user details');
        }
    };

    const fetchUserSkills = async () => {
        try {

            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            //setUserId(user)
            const response = await axios.get(`http://localhost:5093/api/Skill/all/${user}`);

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
            const response = await axios.get(`http://localhost:5093/api/Experience/all/${user}`);

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
            const response = await axios.get(`http://localhost:5093/api/Education/all/${user}`);

            setEduList(response.data)
        } catch (error) {
            console.error('Error fetching education details:', error);
            // setError('Error fetching user details');
        }
    };

    useEffect(() => {
        fetchUserDetails()
        fetchUserSkills()
        fetchUserExs()
        fetchUserEdu()
    }, []);

    const handleCancel = () => {
        setError('')
        setSucess('')
        fetchUserDetails()
      };

    const handleSubmit = async (e) => {
        e.preventDefault();
        // Check if all fields are filled
        for (const key in formData) {
            if (!formData[key]) {
                setError(`Please fill out ${key}`);
                setSucess('')
                return;
            }
        }
        try {
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            const response = await axios.put(`http://localhost:5093/api/User/${user}`, formData);
            console.log(response.data); // Assuming the response contains success message or token
            // Redirect or show success message
            setError('')
            setSucess('Update user details', sucess);
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

    const handleSubmitSkill = async (e) => {
        e.preventDefault();
        try {
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            const skill = {
                skill1 : skillName,
                userId : user
            }
            const response = await axios.post(`http://localhost:5093/api/Skill`, skill);
            console.log(response.data); // Assuming the response contains success message or token 
            setSkillName('')  
            fetchUserSkills()     
           
        } catch (err) {
        }
    };

    const handleSubmitEx = async (e) => {
        e.preventDefault();
        try {
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', formDataEx);
            setFormDataEx({
                ...formDataEx,
                userId:user
            });
            const response = await axios.post(`http://localhost:5093/api/Experience`, formDataEx);
            console.log(response.data); // Assuming the response contains success message or token 
            fetchUserExs()     
            setFormDataEx({
                ...formDataEx,
                title: '',
                companyName: '',
                location: '',
                startDate: '',
                endDate: '',
                userId: ''
            });
        } catch (err) {
        }
    };


    const handleSubmitEdu = async (e) => {
        e.preventDefault();
        try {
            const user = localStorage.getItem('userId');
            console.log('User ID from local storage:', user);
            setFormDataEdu({
                ...formDataEdu,
                userId:user
            });
            const response = await axios.post(`http://localhost:5093/api/Education`, formDataEdu);
            console.log(response.data); // Assuming the response contains success message or token 
            fetchUserEdu()     
            setFormDataEdu({
                ...formDataEdu,
                school: '',
                degree: '',
                fieldOfStudy: '',
                startDate: '',
                endDate: '',
                grade: '',
                userId: ''
            });
        } catch (err) {
        }
    };

    const handleDeleteSkill = async (Id) => {
        try {
            await axios.delete(`http://localhost:5093/api/Skill/${Id}`);
            fetchUserSkills();
        } catch (error) {
        }
      };

      const handleDeleteEx = async (Id) => {
        try {
            await axios.delete(`http://localhost:5093/api/Experience/${Id}`);
            fetchUserExs();
        } catch (error) {
        }
      };

      const handleDeleteEdu = async (Id) => {
        try {
            await axios.delete(`http://localhost:5093/api/Education/${Id}`);
            fetchUserEdu();
        } catch (error) {
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
            <div className="col-lg-4">
                <div className="card">
                <div className="card-body">
                    <div className="d-flex flex-column align-items-center text-center">
                    <div className="mt-1">
                        <h4>Skills</h4>
                    </div>
                    </div>
                    <hr className="my-4" />
                    <div className="row mb-3">
                        <div className="col-sm-8">
                        <input type="text" className="form-control" value={skillName} onChange={handleSkillNameChange}/>
                        </div>
                        <div className="col-sm-4 text-secondary">
                            <input type="button" className="btn btn-primary px-4 mr-2" defaultValue="+" onClick={handleSubmitSkill}/>
                        </div>
                    </div>
                    <hr className="my-4" />
                    {skillList && skillList.map(skill => (
                        <div className='row'>
                            <div className='col-8'>
                            <p>{skill.skill1}</p>
                            </div>
                            <div className='col-4'>
                            <p class="text-danger text-xs"
                            onClick={() => handleDeleteSkill(skill.id)}>Delete</p>
                            </div>
                           
                        </div>
                    ))}
                </div>
                </div>
            </div>
            <div className="col-lg-8">
                <div className="card">
                <div className="card-body">
                    <h5 className="d-flex align-items-center mb-3">Personal Details</h5>
                    <hr className="my-4" />
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">First Name</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='firstname' value={formData.firstname} onChange={handleChange}/>
                    </div>
                    <div className="col-sm-2">
                        <h6 className="mb-0">Last Name</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='lastname' value={formData.lastname} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">Date of Birth</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="date" className="form-control" name='dateOfBirth'  value={formData.dateOfBirth} onChange={handleChange}/>
                    </div>
                    <div className="col-sm-2">
                        <h6 className="mb-0">Gender</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <select className="form-control" aria-label="Gender" name='gender' value={formData.gender} onChange={handleChange}>
                            <option selected disabled>Select Gender</option>
                            <option value="Male">Male</option>
                            <option value="Female">Female</option>
                        </select>
                    </div>
                    </div>
                    <div className="row mb-3">
                    <div className="col-sm-2">
                        <h6 className="mb-0">Email</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='email' value={formData.email} onChange={handleChange}/>
                    </div>
                    <div className="col-sm-2">
                        <h6 className="mb-0">Mobile</h6>
                    </div>
                    <div className="col-sm-4 text-secondary">
                        <input type="text" className="form-control" name='contactNumbers' value={formData.contactNumber} onChange={handleChange}/>
                    </div>
                    </div>
                    <div className="row">
                    <div className="col-sm-6">
              
                    </div>
                    <div className="col-sm-6 text-secondary">
                    <input type="button" className="btn btn-danger px-4 mr-2" defaultValue="Cancel" onClick={handleCancel}/>

                        <input type="button" className="btn btn-primary px-4" defaultValue="Save Changes" onClick={handleSubmit} />
                    </div>
                    {error && <p className="text-danger">{error}</p>}
                    {sucess && <p className="text-primary">{sucess}</p>}

                    </div>
                </div>
                </div>
                <br/>
                <div className="row">
                <div className="col-sm-12">
                    <div className="card">
                    <div className='card-header'>
                        <div className="row">
                            <div className="col-sm-9">
                                <h5 className="d-flex align-items-center mb-3">Experience</h5>
                            </div>
                            <div className="col-sm-3">
                                <input type="button" className="btn btn-primary btn-sm px-4 mr-2" defaultValue="Add New" 
                                onClick={handleSubmitEx}/>
                            </div>
                        </div>
                    </div>
                    <div className="card-body">
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Title</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="title" value={formDataEx.title} onChange={handleChangeEx}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Company</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="companyName" value={formDataEx.companyName} onChange={handleChangeEx}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Location</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="location" value={formDataEx.location} onChange={handleChangeEx}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Start Date</h6>
                            </div>
                            <div className='col-sm-4'>
                                <input type="date" className="form-control" name="startDate" value={formDataEx.startDate} onChange={handleChangeEx}/>
                            </div>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">End Date</h6>
                            </div>
                            <div className='col-sm-4'>
                                <input type="date" className="form-control" name="endDate" value={formDataEx.endDate} onChange={handleChangeEx}/>
                            </div>
                        </div>
                        <hr className="my-4" />
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
                                    <p class="text-danger text-xs"
                                     style={{cursor: 'pointer'}}
                                     onClick={() => handleDeleteEx(ex.id)}>Delete</p>
                                </div>
                            </div>
                        </div>
                        ))}

                    </div>
                    </div>
                </div>
                </div>
                <br/>
                <div className="row">
                <div className="col-sm-12">
                    <div className="card">
                    <div className='card-header'>
                        <div className="row">
                            <div className="col-sm-9">
                                <h5 className="d-flex align-items-center mb-3">Education</h5>
                            </div>
                            <div className="col-sm-3">
                                <input type="button" className="btn btn-primary btn-sm px-4 mr-2" defaultValue="Add New" 
                                onClick={handleSubmitEdu}/>
                            </div>
                        </div>
                    </div>
                    <div className="card-body">
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">School</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="school" value={formDataEdu.school} onChange={handleChangeEdu}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Degree</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="degree" value={formDataEdu.degree} onChange={handleChangeEdu}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Field Of Study</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="fieldOfStudy" value={formDataEdu.fieldOfStudy} onChange={handleChangeEdu}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Grade</h6>
                            </div>
                            <div className='col-sm-6'>
                                <input type="text" className="form-control" name="grade" value={formDataEdu.grade} onChange={handleChangeEdu}/>
                            </div>
                        </div>
                        <div className='row mb-3'>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">Start Date</h6>
                            </div>
                            <div className='col-sm-4'>
                                <input type="date" className="form-control" name="startDate" value={formDataEdu.startDate} onChange={handleChangeEdu}/>
                            </div>
                            <div className='col-sm-2'>
                                <h6 className="mb-0">End Date</h6>
                            </div>
                            <div className='col-sm-4'>
                                <input type="date" className="form-control" name="endDate" value={formDataEdu.endDate} onChange={handleChangeEdu}/>
                            </div>
                        </div>
                        <hr className="my-4" />
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
                                    <p class="text-danger text-xs"
                                     onClick={() => handleDeleteEdu(ex.id)}>Delete</p>
                                </div>
                            </div>
                        </div>
                        ))}

                    </div>
                    </div>
                </div>
                </div>
                <br/>
                <br/>
            </div>
            </div>
        </div>
        </div>
    </div>
  )
}

export default ProfileComponent
