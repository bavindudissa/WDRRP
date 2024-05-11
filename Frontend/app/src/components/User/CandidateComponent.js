import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'

function CandidateComponent() {

  const [cList, setCList] = useState('');


  const fetchCDetails = async () => {
      try {

          const user = localStorage.getItem('userId');
          console.log('User ID from local storage:', user);
          //setUserId(user)
          const response = await axios.get(`http://localhost:5093/api/User`);
          setCList(response.data);
          console.log("data" + response.data);
      } catch (error) {
          console.error('Error fetching cadidate details:', error);
      }
  };

  useEffect(() => {
    fetchCDetails()
  }, []);


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
        <div className="row">
        {cList && cList.map(c => (
          <div className="col-lg-4 sidebar">
            <div className="single-widget protfolio-widget">
              <a href="#"><h4>{c.firstName} {c.lastName}</h4></a>
              <p>
               {c.email}
              </p>
              <p>
               {c.contactNumber}
              </p>
              <ul>
                <li><a href={"/profileview/"+c.id}><i className="fa fa-book" /></a></li>
              </ul>								
            </div>
          </div>
        ))}
        </div>
      </div>	
    </section>


    </div>
  )
}

export default CandidateComponent
