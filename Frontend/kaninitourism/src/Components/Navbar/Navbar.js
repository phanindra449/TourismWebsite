import React, { useState, useEffect } from "react";
import './Navbar.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import logo from '../images/yatra-logo.png';
import { Link } from "react-router-dom";
import LoginModal from "../Login/Login";
import SignupModal from "../SignupModal/SignupModal"; // Import your Signup modal component


function Navbar() {
  const isHomePage = window.location.pathname === "/";
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [userRole, setUserRole] = useState("");
  const [isSignupModalOpen, setIsSignupModalOpen] = useState(false); // State for Signup modal

  useEffect(() => {
    const role = localStorage.getItem("role");
    setUserRole(role);
  }, []);

  const openModal = () => {
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
  };
  const openSignupModal = () => {
    setIsSignupModalOpen(true);
  };

  const closeSignupModal = () => {
    setIsSignupModalOpen(false);
  };
  const handleLogout = () => {
    localStorage.removeItem("role");
    setUserRole("");
  };

  return (
    <div className="navbar">
      <div className="header">
        <nav>
          <input type="checkbox" id="show-search" />
          <input type="checkbox" id="show-menu" />
          <label htmlFor="show-menu" className="menu-icon">
            <FontAwesomeIcon icon={faBars} />
          </label>

          <div className="content">
          <div className="circular--landscape">
            <Link to="/">
              <img src={logo} alt="" />
            </Link>
          </div>
          <ul className="links">
            <li><Link to="/">Home</Link></li>
            {isHomePage && userRole !== "TravelAgent" && (
              <>
                <li><a href="#services">Services</a></li>
                <li><a href="#about">About</a></li>
                <li><a href="#reviews">Reviews</a></li>
              </>
            )}
            <li><Link to="/TourPackage">Tour Packages</Link></li>
            {userRole === "TravelAgent" && (
              <>
                <li><Link to="/create-package">Create Package</Link></li>
                <li><Link to="/add-destinations">Add Destinations</Link></li>
                <li><Link to="/add-exclusions">Add Exclusions</Link></li>
                <li><Link to="/add-inclusions">Add Inclusions</Link></li>
              </>
            )}
            {localStorage.getItem('token')==null && (
              <>
                <li><a href="#" onClick={openModal}>Login</a></li>
                <li><a href="#" onClick={openSignupModal}>Sign Up</a></li>
              </>
            )}
            {localStorage.getItem('token')!=null && (
              <li><a href="/" onClick={handleLogout}>Logout</a></li>
            )}
            {userRole === "traveller" && (
              <li><Link to="/my-bookings">My Bookings</Link></li>
            )}
          </ul>
        </div>
        






          <label htmlFor="show-search" className="search-icon"><i className="fas fa-search"></i></label>
          <form action="#" className="search-box">
          </form>
        </nav>
      </div>

      <LoginModal isOpen={isModalOpen} onClose={closeModal} />
      <SignupModal isOpen={isSignupModalOpen} onClose={closeSignupModal} /> {/* Fix isOpen and onClose props */}
    </div>
  )
}

export default Navbar;
