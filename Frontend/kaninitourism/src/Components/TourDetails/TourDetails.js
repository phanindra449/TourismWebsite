import React, { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import './TourDetails.css';
import Navbar from '../Navbar/Navbar';
import DatePicker from "react-datepicker"; // Import react-datepicker
import "react-datepicker/dist/react-datepicker.css"; //
import Modal from "react-modal"; // Import the Modal component



function TourDetails() {
  const { tourId } = useParams();
  const [tourDetails, setTourDetails] = useState(null);
  const [inclusionDetails, setInclusionDetails] = useState([]);
  const [exclusionDetails, setExclusionDetails] = useState([]);
  const [selectedDestination, setSelectedDestination] = useState(null);
  const [selectedDate, setSelectedDate] = useState(null); // State for selected date
  const [showCalendar, setShowCalendar] = useState(false); // State for modal visibility

  const [showBookingModal, setShowBookingModal] = useState(false);
  const [selectedTourDate, setSelectedTourDate] = useState(null);
  const [numTravelers, setNumTravelers] = useState(1);
  
  useEffect(() => {
    fetch(`http://localhost:5163/api/TourDetails/${tourId}`, {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch tour details");
        }
        const data = await response.json();
        setTourDetails(data);
        console.log("fcgvhbjk")
        console.log(tourDetails.tourDate[0].departureDate)
      })
      .catch((error) => {
        console.error("Error fetching tour details:", error);
      });
  }, [tourId]);

  useEffect(() => {
    if (tourDetails) {
      fetchInclusionDetailsForDebug(tourDetails.tourInclusion);
      fetchExclusionDetailsForDebug(tourDetails.tourExclusion);

      // Fetch and update destination details
      const destinationPromises = tourDetails.tourDestination.map((destination) => {
        return fetchDestinationDetails(destination.destinationId);
      });

      Promise.all(destinationPromises)
        .then((destinationDataArray) => {
          // Update the tour destination details with the fetched destination names
          const updatedTourDestinations = destinationDataArray.map((destinationData, index) => {
            return {
              ...tourDetails.tourDestination[index],
              destinationName: destinationData.destinationName,
              country: destinationData.country,
              city: destinationData.city,
              spotDescription: destinationData.spotDescription,
            };
          });
          setTourDetails({
            ...tourDetails,
            tourDestination: updatedTourDestinations,
          });
        })
        .catch((error) => {
          console.error("Error fetching destination details:", error);
        });
    }
  }, [tourDetails]);


  const scrollToSection = (sectionId) => {
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: "smooth" });
    }
  };
  const handleBookNowClick = () => {
    setShowCalendar(true);
  };
  const handleDateSelect = (date) => {
    if (selectedDate && selectedDate.toISOString().slice(0, 10) === date.toISOString().slice(0, 10)) {
      setShowCalendar(false);
    } else {
      const isGreenDate = tourDetails.tourDate.some(
        (tourDate) =>
          date.toISOString().slice(0, 10) === new Date(tourDate.departureDate).toISOString().slice(0, 10)
      );
  
      if (isGreenDate) {
        handleGreenDateClick(date);
        setShowCalendar(false);

      } else {
        setShowCalendar(true);
      }
    }
  };
  
  
  const handleGreenDateClick = (date) => {
    setSelectedDate(date);
    setSelectedTourDate(date); 
    setShowBookingModal(true);
  };
  

  const fetchInclusionDetailsForDebug = (inclusions) => {
    const inclusionPromises = inclusions.map((inclusion) => {
      return fetchInclusionDetails(inclusion.inclusionId);
    });

    Promise.all(inclusionPromises)
      .then((inclusionDataArray) => {
        setInclusionDetails(inclusionDataArray);
      })
      .catch((error) => {
        console.error("Error fetching inclusion details:", error);
      });
  };

  const fetchExclusionDetailsForDebug = (exclusions) => {
    const exclusionPromises = exclusions.map((exclusion) => {
      return fetchExclusionDetails(exclusion.exclusionId);
    });

    Promise.all(exclusionPromises)
      .then((exclusionDataArray) => {
        setExclusionDetails(exclusionDataArray);
      })
      .catch((error) => {
        console.error("Error fetching exclusion details:", error);
      });
  };

  const fetchInclusionDetails = async (inclusionId) => {
    try {
      const response = await fetch(`http://localhost:5163/api/Inclusions/${inclusionId}`);
      if (!response.ok) {
        throw new Error("Failed to fetch inclusion details");
      }
      const data = await response.json();
      return data.inclusionDescriptionn;
    } catch (error) {
      console.error("Error fetching inclusion details:", error);
      return "Error fetching inclusion details";
    }
  };

  const fetchExclusionDetails = async (exclusionId) => {
    try {
      const response = await fetch(`http://localhost:5163/api/Exclusions/${exclusionId}`);
      if (!response.ok) {
        throw new Error("Failed to fetch exclusion details");
      }
      const data = await response.json();
      return data.exclusionDescription;
    } catch (error) {
      console.error("Error fetching exclusion details:", error);
      return "Error fetching exclusion details";
    }
  };
  const fetchDestinationDetails = async (destinationId) => {
    try {
      const response = await fetch(`http://localhost:5163/api/Destinations/${destinationId}`);
      if (!response.ok) {
        throw new Error("Failed to fetch destination details");
      }
      const data = await response.json();
      return {
        destinationId,
        destinationName: data.destinationName,
        country: data.country,
        city: data.city,
        spotDescription: data.spotDescription,
      };
    } catch (error) {
      console.error("Error fetching destination details:", error);
      return {
        destinationId,
        destinationName: "Error fetching destination details",
        country: "Error",
        city: "Error",
        spotDescription: "Error",
      };
    }
  };


  return (
    <div className="tourdetailspage">
      <div className="tourdetails-flex-container">

        <div className="tourdetail-navbar">      <Navbar></Navbar>
        </div>  {tourDetails && (
          <div className="tour-flex">
            <div className="tourtitlebar">
              <div
                className="tour-header"
                style={{
                  position: 'sticky',
                  top: 0,
                  backgroundColor: 'rgba(255, 255, 255, 0.9)',
                  zIndex: 999,
                  width: '100%', 

                }}
              >
                <h1>{tourDetails.tourName}</h1>
                <div className="booknowbtn">
                  <button className= "book-now-link"onClick={handleBookNowClick}>
                    Book Now
                  </button>
                  <p className="startingfrom">
                    starting from <br />
                    <p className="price">{tourDetails.tourPrice}</p>
                  </p>
                </div>
              </div>
              <div>
                <nav
                  style={{
                    position: 'sticky',
                    top: 0,
                    width: '70%',
                    backgroundColor: '#fff',
                  }}
                >
                  <ul style={{ display: "flex", justifyContent: "space-around", listStyle: "none" }}>
                    <li>
                      <button className="navbtns" onClick={() => scrollToSection("overview")}>Overview</button>
                    </li>
                    <li>
                      <button className="navbtns" onClick={() => scrollToSection("itinerary")}>Day Wise Itinerary</button>
                    </li>
                    <li>
                      <button className="navbtns" onClick={() => scrollToSection("additionalInfo")}>Additional Info</button>
                    </li>
                  </ul>
                </nav>
              </div>
            </div>
            <section id="overview">
              <div className="tourdescription">
                <div> <img src={`http://127.0.0.1:10000/devstoreaccount1/tour/tour/${tourDetails.tourImage}`} alt="tourimage" /></div>
                <div> <p className="packageoverviewtext">Package Overview </p>{tourDetails.tourDescription}</div>
              </div>
              <div className="inclusions-exclusions">
                <div className="info-card">
                  <h2>Inclusions</h2>
                  <ul>
                    {inclusionDetails.length > 0 ? (
                      inclusionDetails.map((inclusion, index) => (
                        <li key={index}>{inclusion}</li>
                      ))
                    ) : (
                      <li>Loading inclusion details...</li>
                    )}
                  </ul>
                </div>
                <div className="info-card">
                  <h2>Exclusions</h2>
                  <ul>
                    {exclusionDetails.length > 0 ? (
                      exclusionDetails.map((exclusion, index) => (
                        <li key={index}>{exclusion}</li>
                      ))
                    ) : (
                      <li>Loading exclusion details...</li>
                    )}
                  </ul>
                </div>
              </div></section>
            <section id="itinerary">
              <h2>Day Wise Itinerary</h2>
              {tourDetails && tourDetails.tourDestination.length > 0 ? (
                tourDetails.tourDestination.map((destination, index) => (
                  <div key={index} className="itinerary-day">
                    <div>        <h3>Day {index + 1}</h3>
                    </div>
                    <div className="destination-details">
                      <div>
                        <img src={`http://127.0.0.1:10000/devstoreaccount1/tour/tour/${destination.destinationImage}`} alt="df"></img>
                        <p>{destination.destinationName}</p>
                        <p>{destination.country}</p>
                        <p>{destination.city}</p>
                      </div>
                      <div className="destination-activities">
                        <div className="activity-types">     <p>Activity :</p> <p>{destination.activityName} </p>
                        </div>

                        <div className="activity-types">     <p>Activity Description :</p>            <p>{destination.destinationActivity}</p></div>
                        <div className="activity-types">         <p>Event Time :</p>            <p>{destination.eventTime}</p></div>
                        <div className="activity-types">     <p>Event Type :</p>            <p>{destination.activityType}</p></div>
                      </div>


                    </div>
                    {/* Render more details about the destination here */}
                  </div>
                ))
              ) : (
                <p>No itinerary details available.</p>
              )}
            </section>

            <section id="additionalInfo">
              <div className="additional-info-header">
                <h2>Additional Info</h2>
                <div className="destination-links">
                  {tourDetails.tourDestination.map((destination, index) => (
                    <button
                      key={index}
                      className={`destination-link ${selectedDestination === index ? 'active' : ''
                        }`}
                      onClick={() => setSelectedDestination(index)}
                    >
                      {destination.destinationName}
                    </button>
                  ))}
                </div>
              </div>
              {selectedDestination !== null && (
                <div className="spot-description">
                  {tourDetails.tourDestination[selectedDestination].spotDescription}
                </div>
              )}
            </section>



          </div>
        )}

{showCalendar && (
  <div className="calendar-modal">

    <DatePicker
    
      selected={selectedDate}
      onChange={handleDateSelect}
      inline // Display calendar as inline modal
      // Customize how to mark the available tour dates
      dayClassName={(date) =>
        tourDetails.tourDate.some(
          (tourDate) =>
            date.toISOString().slice(0, 10) === new Date(tourDate.departureDate).toISOString().slice(0,10)
        )
          ? "green-day"
          :  undefined
      }
    />
            <span className="close-icon" onClick={() => setShowCalendar(false)}>X</span>

  </div>
)}
{showBookingModal && (
  <Modal
    isOpen={showBookingModal}
    onRequestClose={() => setShowBookingModal(false)}
    className="booking-modal"
  >
        <span className="close-icon" onClick={() => setShowBookingModal(false)}>X</span>

    <h2>Book Tour</h2>
    <p>Tour: {tourDetails.tourName}</p>
    <p>Date: {selectedTourDate && selectedTourDate.toDateString()}</p>
    <label htmlFor="numTravelers">Number of Travelers:</label>
    <input
      type="number"
      id="numTravelers"
      value={numTravelers}
      onChange={(e) => setNumTravelers(e.target.value)}
    />


  <Link className= "book-now-link"
    to={`/booking/${tourId}?date=${encodeURIComponent(selectedTourDate.toISOString())}&travelers=${numTravelers}`}
  >
    Proceed to Book
  </Link>

 </Modal>
)}


      </div>
    </div>
  );
}

export default TourDetails;

