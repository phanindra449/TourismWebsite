import React, { useState, useEffect } from "react";
import "./AdminLandingPage.css";
import logo from '../images/yatra-logo.png'
import { Link } from "react-router-dom";

function AdminLandingPage() {
  const [travelAgents, setTravelAgents] = useState([]);
  const [bookingCount, setBookingCount] = useState(0);
  const [totalRegisteredAgents, setTotalRegisteredAgents] = useState(0);
  const [totalActiveTravelAgents, setTotalActiveTravelAgents] = useState(0);
  

  useEffect(() => {
    fetch("http://localhost:5292/api/TravelAgent/GetAllTravelAgents", {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch travel agents");
        }
        const data = await response.json();
        setTravelAgents(data);
        
        // Calculate the total registered travel agents
        const totalRegisteredAgents = data.length;
        setTotalRegisteredAgents(totalRegisteredAgents);
        
        // Calculate the total active travel agents
        const activeTravelAgents = data.filter(agent => agent.isActive);
        const totalActiveTravelAgents = activeTravelAgents.length;
        setTotalActiveTravelAgents(totalActiveTravelAgents);
      })
      .catch((error) => {
        console.error("Error fetching travel agents:", error);
      });
  }, []);
  

  const handleChangeStatus = (travelAgentId) => {
    const updatedTravelAgents = travelAgents.map((agent) => {
      if (agent.travelAgentId === travelAgentId) {
        return {
          ...agent,
          isActive: !agent.isActive, // Toggle the status
        };
      }
      return agent;
    });
  
    // Find the updated agent AFTER mapping
    const updatedAgent = updatedTravelAgents.find((agent) => agent.travelAgentId === travelAgentId);
  
    fetch(`http://localhost:5292/api/TravelAgent/UpdateTravelAgentStatus`, {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ travelAgentId, isActive: updatedAgent.isActive }), // Use the updated status
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Failed to update travel agent status");
        }
        setTravelAgents(updatedTravelAgents);
  
        // Update the total active travel agents count
        const newTotalActiveTravelAgents = updatedTravelAgents.filter(agent => agent.isActive).length;
        setTotalActiveTravelAgents(newTotalActiveTravelAgents);
      })
      .catch((error) => {
        console.error("Error updating travel agent status:", error);
      });
  };
  

  
  return (
    <div className="admin-landing-page">
       <nav className="navbar">
        <div className="navbar-logo">
          <img src={logo} alt="Logo" />
        </div>
        <div className="navbar-actions">
        <li><Link to="/" className="logout-button">Logout</Link></li>
        </div>
      </nav>
        <div className="dashboard"> 
<div className="col-md-4">
  <div className="card">
    <div className="card-body">
      <div className="card-icon">
        <i className="fa fa-user"></i>
      </div>
      <h5 className="card-title">Registered Travel Agents</h5>
      <p className="card-text">{totalRegisteredAgents}</p>
    </div>
  </div>
</div>
<div className="col-md-4">
  <div className="card">
    <div className="card-body">
      <div className="card-icon">
        <i className="fa fa-user"></i>
      </div>
      <h5 className="card-title">Total No of bookings</h5>
      <p className="card-text">{bookingCount}</p>
    </div>
  </div>
</div>
<div className="col-md-4">
  <div className="card">
    <div className="card-body">
      <div className="card-icon">
        <i className="fa fa-users"></i>
      </div>
      <h5 className="card-title">Active Travel Agents</h5>
      <p className="card-text">{totalActiveTravelAgents}</p>
    </div>
  </div>
</div>
</div>

        

      <div className="card-container">
        {/* ... Create Package card ... */}
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">Registered Travel Agents</h5>
            <div className="table-responsive">
              <table className="table">
                <thead>
                  <tr>
                    <th>Email</th>
                    <th>Username</th>
                    <th>Agency Name</th>
                    <th>Contact Number</th>
                    <th>Country</th>
                    <th>Status</th>
                    <th>Change Status</th>

                  </tr>
                </thead>
                <tbody>
                  {travelAgents.map((agent) => (
                    <tr key={agent.email}>
                      <td>{agent.email}</td>
                      <td>{agent.username}</td>
                      <td>{agent.agencyName}</td>
                      <td>{agent.contactNumber}</td>
                      <td>{agent.country}</td>
                      <td> {agent.isActive ? "Approved" : 'DisApproved'}</td>
                      <td>
                        <button
                          className={`btn ${agent.isActive ? "btn-danger" : "btn-success"}`}
                          onClick={() => handleChangeStatus(agent.travelAgentId)}
                        >
                          {agent.isActive ? "Deactivate" : "Activate"}
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminLandingPage;
