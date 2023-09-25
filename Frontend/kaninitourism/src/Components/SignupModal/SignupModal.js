import React, { useState } from "react";
import "./SignupModal.css";
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Modal from "react-modal"; 
function SignupModal(props) {

  const [role, setRole] = useState(""); // Define role state

  const { isOpen, onClose } = props;

  const [isSignIn, setIsSignIn] = useState(false);
  const navigate = useNavigate();


  const [registrationType, setRegistrationType] = useState("travel_agent");
  const [errors, setErrors] = useState({});
  const [error, setError] = useState(null);

  const [travelAgent, setTravelAgent] = useState({
    "users": {
      "email": ""
    },
    "username": "",
    "agencyName": "",
    "contactNumber": "",
    "country": "",
    "address": "",
    "licenseNumber": "",
    "isActive": true,
    "yearsInBusiness": 0,
    "licenseExpiryDate": "",
    "passwordClear": ""
  });

  const [traveller, setTraveller] = useState({
    "users": {
      "email": ""
    },
    "username": "",
    "phoneNumber": "",
    "gender": "",
    "passwordClear": ""
  });

  const [user, setUser] = useState({
    "userId": 0,
    "email": "",
    "password": "",
    "role": "",
    "token": ""
  }); 
  
  var TravelAgentRegister = () => {
    console.log("Travel Agent Data to be Sent:", travelAgent);
    
    // Parse "Years In Business" value to an integer
    const yearsInBusinessInt = parseInt(travelAgent.yearsInBusiness, 10);
    
    console.log("Years In Business (Parsed):", yearsInBusinessInt);
    
    fetch("http://localhost:5292/api/TravelAgent/RegisterAsTravelAgent", {
      method: "POST",
      headers: {
        "accept": "text/plain",
        "Content-Type": 'application/json'
      },
      body: JSON.stringify({ ...travelAgent, yearsInBusiness: yearsInBusinessInt, "travelAgent": {} })
    })
    .then(async (data) => {
      if (data.status === 200) {
        const myData = await data.json();
        settingLocalStorage(myData);
        navigate("/CreatePackage");
        console.log("Successful Response:", myData);
      } else {
        console.log("Error Response:", data);
      }
    })
    .catch((err) => {
      console.log("Fetch Error:", err);
    });
  };
  
  var TravellerRegister = () => {
    console.log(traveller);
    fetch("http://localhost:5292/api/Traveller/RegisterAsTraveller", {
      "method": "POST",
      headers: {
        "accept": "text/plain",
        "Content-Type": 'application/json'
      },
      "body": JSON.stringify({ ...traveller, "traveller": {} })
    })
    .then(async (data) => {
      if (data.status === 201) {
        alert("201")
        const myData = await data.json(); // Define and set myData
        settingLocalStorage(myData); // Pass myData to settingLocalStorage
        console.log(myData);
        
        onClose(); // Close the modal
      }
    })
    .catch((err) => {
      console.log(err.body);
    });
  };
  


  var settingLocalStorage = (myData) => { // Accept myData as a parameter
    localStorage.setItem("token", myData.token);
    localStorage.setItem("role", myData.role);
    localStorage.setItem("userId", myData.userId);
  }
useEffect(() => {
  const roleofuser = localStorage.getItem("role");
  setRole(roleofuser); 
  console.log(roleofuser);
}, []);

  const handleToggle = () => {
    setIsSignIn(!isSignIn);
  };

  const handleRegistrationTypeChange = () => {
    setRegistrationType(registrationType === "travel_agent" ? "traveller" : "travel_agent");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    onClose();

    if (!isSignIn) {
      if (registrationType === "travel_agent") {
        await TravelAgentRegister(); // Call TravelAgentRegister function for travel agent registration
      } else if (registrationType === "traveller") {
        await TravellerRegister(); // Call TravellerRegister function for traveler registration
      }
    }
  };

  useEffect(() => {
    let ignore = false;

    if (!ignore) removingLocalStorage();
    return () => {
      ignore = true;
    };
  }, []);
  

 
 

  const removingLocalStorage = () => {
    localStorage.clear("token");
    localStorage.clear("role");
    localStorage.clear("userId");
  };

  return (
    <Modal
    isOpen={isOpen}
    onRequestClose={onClose}
    contentLabel="Sign Up Modal"
  >

    <div id="container" className={`container ${isSignIn ? "sign-in" : "sign-up"}`}>
      <div className="row">
        {/* SIGN UP */}
        <div className={`col ${isSignIn ? "hidden" : ""}`}>
        <button className="close-button" onClick={onClose}>Close</button>

          <div className="form-wrapper align-items-center">
            <form className="form sign-up" onSubmit={handleSubmit}>
              <label className="switch">
                <input type="checkbox" checked={registrationType === "travel_agent"} onChange={handleRegistrationTypeChange} />
                <span className="slider round"></span>
              </label>
              <span className="toggle-label">
                {registrationType === "travel_agent" ? "Register as Travel Agent" : "Register as Traveller"}
              </span>
              <div className="input-group">
                <i className="bx bxs-user"></i>
                <input type="text" placeholder="Username" value={registrationType === "travel_agent" ? travelAgent.username : traveller.username} onChange={(e) => registrationType === "travel_agent" ? setTravelAgent({ ...travelAgent, username: e.target.value }) : setTraveller({ ...traveller, username: e.target.value })} />
              </div>
              {registrationType === "traveller" || registrationType === "travel_agent" && (
  <div className="input-group">
    <i className="bx bx-phone"></i>
    <input
      type="tel"
      placeholder="Phone Number"
      value={registrationType === "travel_agent" ? travelAgent["contactNumber"] : traveller.phoneNumber}
      onChange={(e) => {
        if (registrationType === "travel_agent") {
          setTravelAgent({ ...travelAgent, "contactNumber": e.target.value });
        } else {
          setTraveller({ ...traveller, phoneNumber: e.target.value });
        }
      }}
    />
  </div>
)}

              <div className="input-group">
                <i className="bx bx-mail-send"></i>
                <input type="email" placeholder="Email" value={registrationType === "travel_agent" ? travelAgent.users.email : traveller.users.email} onChange={(e) => registrationType === "travel_agent" ? setTravelAgent({ ...travelAgent, users: { ...travelAgent.users, email: e.target.value } }) : setTraveller({ ...traveller, users: { ...traveller.users, email: e.target.value } })} />
              </div>
              <div className="input-group">
                <i className="bx bxs-lock-alt"></i>
                <input type="password" placeholder="Password" value={registrationType === "travel_agent" ? travelAgent.passwordClear : traveller.passwordClear} onChange={(e) => registrationType === "travel_agent" ? setTravelAgent({ ...travelAgent, passwordClear: e.target.value }) : setTraveller({ ...traveller, passwordClear: e.target.value })} />
              </div>
              {registrationType === "travel_agent" && (
                <>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="text" placeholder="AgencyName" value={travelAgent.agencyName} onChange={(e) => setTravelAgent({ ...travelAgent, agencyName: e.target.value })} />
                  </div>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="text" placeholder="Country" value={travelAgent.country} onChange={(e) => setTravelAgent({ ...travelAgent, country: e.target.value })} />
                  </div>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="text" placeholder="Address" value={travelAgent.address} onChange={(e) => setTravelAgent({ ...travelAgent, address: e.target.value })} />
                  </div>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="text" placeholder="License Number" value={travelAgent.licenseNumber} onChange={(e) => setTravelAgent({ ...travelAgent, licenseNumber: e.target.value })} />
                  </div>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="text" placeholder="License Expiry Date" value={travelAgent.licenseExpiryDate} onChange={(e) => setTravelAgent({ ...travelAgent, licenseExpiryDate: e.target.value })} />
                  </div>
                  <div className="input-group">
                    <i className="bx bx-mail-send"></i>
                    <input type="number" placeholder="Years In Business" value={travelAgent.yearsInBusiness}  />
                  </div>
                </>
              )}
              <button type="submit">Sign up</button>
              <p>
                <span>Already have an account?</span>
                <b onClick={handleToggle} className="pointer">
                  Sign in here
                </b>
              </p>
            </form>
          </div>
        </div>
        {/* END SIGN UP */}
        {/* SIGN IN */}
        <div className={`col align-items-center flex-col ${isSignIn ? "" : "hidden"}`}>
          <div className="form-wrapper align-items-center">
            <form className="form sign-in" onSubmit={handleSubmit}>
              <div className="input-group">
                <i className="bx bxs-user"></i>
                <input type="text" placeholder="email" value={user.email || ""} onChange={(e) => setUser({ ...user, email: e.target.value })} />
              </div>
              <div className="input-group">
                <i className="bx bxs-lock-alt"></i>
                <input type="password" placeholder="Password" value={user.password || ""} onChange={(e) => setUser({ ...user, password: e.target.value })} />
              </div>
              <button type="submit">Sign in</button>
              <p>
              </p>
              <p>
                <span>Don't have an account?</span>
               
              </p>
            </form>
          </div>
          <div className="form-wrapper"></div>
        </div>
        {/* END SIGN IN */}
      </div>
      {/* END FORM SECTION */}
      {/* CONTENT SECTION */}
      <div className="row content-row">
        {/* SIGN IN CONTENT */}
        <div className="col align-items-center flex-col">
          <div className="text sign-in">
          </div>
          <div className="img sign-in"></div>
        </div>
        {/* END SIGN IN CONTENT */}
        {/* SIGN UP CONTENT */}
        <div className="col align-items-center flex-col">
          <div className="img sign-up"></div>
          <div className="text sign-up">
          </div>
        </div>
        {/* END SIGN UP CONTENT */}
      </div>
      {/* END CONTENT SECTION */}
    </div>
    </Modal>
  );
}

export default SignupModal;