import React, { useState, useEffect } from 'react';
import axios from 'axios';

function CandidateComponent() {
    const [cList, setCList] = useState([]);
    const [filteredCList, setFilteredCList] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        fetchCDetails();
    }, []);

    const fetchCDetails = async () => {
        try {
            const response = await axios.get('http://localhost:5093/api/User');
            setCList(response.data);
            setFilteredCList(response.data); // Initially, the filtered list is the same as the full list
        } catch (error) {
            console.error('Error fetching candidate details:', error);
        }
    };

    const handleSearch = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(value);

        const filtered = cList.filter(c =>
            `${c.firstName.toLowerCase()} ${c.lastName.toLowerCase()}`.includes(value)
        );
        setFilteredCList(filtered);
    };

    return (
        <div>
            <section className="banner-area relative" id="home">
                <div className="overlay overlay-bg" />
                <div className="container">
                    <div className="row d-flex align-items-center justify-content-center">
                        <div className="about-content col-lg-12">
                            <h1 className="text-white">
                                Candidates
                            </h1>
                        </div>
                    </div>
                </div>
            </section>
            <section className="blog-posts-area section-gap">
                <div className="container">
                  <div className="row justify-content-center d-flex">
                        <div className="col-lg-8">
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Search by first or last name"
                                value={searchTerm}
                                onChange={handleSearch}
                            />
                        </div>
                    </div>
                    <br/>
                    <div className="row">
                        {filteredCList && filteredCList.map(c => (
                            <div className="col-lg-4 sidebar" key={c.id}>
                                <div className="single-widget protfolio-widget">
                                    {c.profilePic ? (
                                        <img src={`http://localhost:5093/files/${c.profilePic}`} alt="Profile" className="img-thumbnail" style={{ width: '150px', height: '150px', borderRadius: '50%' }} />
                                    ) : (
                                        <p>No profile picture uploaded</p>
                                    )}
                                    <a href="#"><h4>{c.firstName} {c.lastName}</h4></a>
                                    <p>
                                        {c.email}
                                    </p>
                                    <p>
                                        {c.contactNumber}
                                    </p>
                                    <ul>
                                        <li><a href={`/profileview/${c.id}`}><i className="fa fa-book" /></a></li>
                                    </ul>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </section>
        </div>
    );
}

export default CandidateComponent;
