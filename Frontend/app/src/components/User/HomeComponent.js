import React from 'react'

function HomeComponent() {
  return (
    <div>
        <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
        </div>
        <section className="banner-area relative" id="home">	
            <div className="overlay overlay-bg" />
            <div className="container">
                <div className="row fullscreen d-flex align-items-center justify-content-center" style={{height: 813}}>
                <div className="banner-content col-lg-12">
                    <h1 className="text-white">
                    <span>1500+</span> Jobs posted last week				
                    </h1>	
                    {/* <form action="search.html" className="serach-form-area">
                    <div className="row justify-content-center form-wrap">
                        <div className="col-lg-4 form-cols">
                        <input type="text" className="form-control" name="search" placeholder="what are you looging for?" />
                        </div>
                        <div className="col-lg-3 form-cols">
                        <div className="default-select" id="default-selects" >
                            <select style={{display: 'none'}}>
                            <option value={1}>Select area</option>
                            <option value={2}>Dhaka</option>
                            <option value={3}>Rajshahi</option>
                            <option value={4}>Barishal</option>
                            <option value={5}>Noakhali</option>
                            </select><div className="nice-select" tabIndex={0}><span className="current">Select area</span><ul className="list"><li data-value={1} className="option selected">Select area</li><li data-value={2} className="option">Dhaka</li><li data-value={3} className="option">Rajshahi</li><li data-value={4} className="option">Barishal</li><li data-value={5} className="option">Noakhali</li></ul></div>
                        </div>
                        </div>
                        <div className="col-lg-3 form-cols">
                        <div className="default-select" id="default-selects2">
                            <select style={{display: 'none'}}>
                            <option value={1}>All Category</option>
                            <option value={2}>Medical</option>
                            <option value={3}>Technology</option>
                            <option value={4}>Goverment</option>
                            <option value={5}>Development</option>
                            </select><div className="nice-select" tabIndex={0}><span className="current">All Category</span><ul className="list"><li data-value={1} className="option selected">All Category</li><li data-value={2} className="option">Medical</li><li data-value={3} className="option">Technology</li><li data-value={4} className="option">Goverment</li><li data-value={5} className="option">Development</li></ul></div>
                        </div>										
                        </div>
                        <div className="col-lg-2 form-cols">
                        <button type="button" className="btn btn-info">
                            <span className="lnr lnr-magnifier" /> Search
                        </button>
                        </div>								
                    </div>
                    </form>	 */}
                    <p className="text-white"> <span>Search by tags:</span> Tecnology, Business, Consulting, IT Company, Design, Development</p>
                </div>											
                </div>
            </div>
        </section>
        <section class="features-area">
				<div class="container">
					<div class="row">
						<div class="col-lg-3 col-md-6">
							<div class="single-feature">
								<h4>Searching</h4>
								<p>
									Lorem ipsum dolor sit amet, consectetur adipisicing.
								</p>
							</div>
						</div>
						<div class="col-lg-3 col-md-6">
							<div class="single-feature">
								<h4>Applying</h4>
								<p>
									Lorem ipsum dolor sit amet, consectetur adipisicing.
								</p>
							</div>
						</div>
						<div class="col-lg-3 col-md-6">
							<div class="single-feature">
								<h4>Security</h4>
								<p>
									Lorem ipsum dolor sit amet, consectetur adipisicing.
								</p>
							</div>
						</div>
						<div class="col-lg-3 col-md-6">
							<div class="single-feature">
								<h4>Notifications</h4>
								<p>
									Lorem ipsum dolor sit amet, consectetur adipisicing.
								</p>
							</div>
						</div>																		
					</div>
				</div>	
		</section>
        <section className="feature-cat-area pt-100" id="category">
        <div className="container">
            <div className="row d-flex justify-content-center">
            <div className="menu-content pb-60 col-lg-10">
                <div className="title text-center">
                <h1 className="mb-10">Featured Job Categories</h1>
                <p>Who are in extremely love with eco friendly system.</p>
                </div>
            </div>
            </div>						
            <div className="row">
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o1.png" alt />
                </a>
                <p>Accounting</p>
                </div>
            </div>
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o2.png" alt />
                </a>
                <p>Development</p>
                </div>
            </div>
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o3.png" alt />
                </a>
                <p>Technology</p>
                </div>
            </div>
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o4.png" alt />
                </a>
                <p>Media &amp; News</p>
                </div>
            </div>
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o5.png" alt />
                </a>
                <p>Medical</p>
                </div>
            </div>
            <div className="col-lg-2 col-md-4 col-sm-6">
                <div className="single-fcat">
                <a href="category.html">
                    <img src="img/o6.png" alt />
                </a>
                <p>Goverment</p>
                </div>			
            </div>																											
            </div>
        </div>	
        </section>

    </div>  
  )
}

export default HomeComponent
