import React, { useState, useEffect } from "react";
import "./TourPackages.css";
import Select from "react-select";
import Navbar from "../Navbar/Navbar";
import { Button } from "bootstrap";
import { Link } from "react-router-dom";

function TourPackages() {
  const [Destinations, setDestinations] = useState([]);
  const [selectedDestinationId, setSelectedDestinationId] = useState(null);
  const [resultTourPackage, setResultTourPackage] = useState([]);
  const [priceRange, setPriceRange] = useState([0, 10000]); // Default price range
  const [activeTravelAgents, setActiveTravelAgents] = useState([]); 

  const sortByPrice = () => {
    const sortedPackages = [...resultTourPackage];
    sortedPackages.sort((a, b) => a.tourPrice - b.tourPrice);
    setResultTourPackage(sortedPackages);
  };

  const sortByDuration = () => {
    const sortedPackages = [...resultTourPackage];
    sortedPackages.sort((a, b) => a.noofdays - b.noofdays);
    setResultTourPackage(sortedPackages);
  };
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
        const agents = await response.json();

        // Filter active agents
        const activeAgents = agents.filter((agent) => agent.isActive);
        setActiveTravelAgents(activeAgents);
        console.log("activea")
        console.log(activeAgents)


      })
      .catch((error) => {
        console.error("Error fetching travel agents:", error);
      });
  }, []);


  useEffect(() => {
    fetch("http://localhost:5163/api/Destinations", {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch destinations");
        }
        const data = await response.json();
        setDestinations(data);
      })
      .catch((error) => {
        console.error("Error fetching destinations:", error);
      });
  }, []);

  const destinationOptions = Destinations.map((destination) => ({
    value: destination.destinationId,
    label: destination.destinationName,
  }));


  useEffect(() => {
    // Fetch all tour details from backend
    fetch("http://localhost:5163/api/TourDetails", {
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
        const tourDetails = await response.json();

        // Fetch all bookings from backend
        fetch("http://localhost:5260/api/Booking/GetAllBookings", {
          method: "GET",
          headers: {
            accept: "application/json",
            "Content-Type": "application/json",
          },
        })
          .then(async (response) => {
            if (!response.ok) {
              throw new Error("Failed to fetch bookings");
            }
            const bookings = await response.json();
            console.log(bookings);

            // Calculate top 5 most booked tour IDs
            const tourIdCounts = {};
            bookings.forEach((booking) => {
              const tourId = booking.tourId;
              tourIdCounts[tourId] = (tourIdCounts[tourId] || 0) + 1;
            });
            const topTourIds = Object.keys(tourIdCounts)
              .sort((a, b) => tourIdCounts[b] - tourIdCounts[a])
              .slice(0, 5);

            // Filter the top 5 tour details
            console.log("Active Agents:", activeTravelAgents);

            console.log("Top Tour IDs:", topTourIds);
            const topTourPackages = tourDetails.filter((tourDetail) =>
            topTourIds.includes(tourDetail.tourId.toString()) &&
            activeTravelAgents.some((agent) => {             console.log("Comparing:", agent.travelAgentId, tourDetail.travelAgentId);
            return agent.travelAgentId === tourDetail.travelAgentId})

            );
          
            console.log("Filtered Tour Packages:", topTourPackages);

            setResultTourPackage(topTourPackages);
          })
          .catch((error) => {
            console.error("Error fetching bookings:", error);
          });
      })
      .catch((error) => {
        console.error("Error fetching tour details:", error);
      });
  }, [activeTravelAgents]);

  useEffect(() => {
    console.log("Updated resultTourPackage:", resultTourPackage);
  }, [resultTourPackage]);

  const handleSearchHolidays = () => {
    // Reset the search fields
    setSelectedDestinationId(null);
    setPriceRange([0, 10000]);
  
    if (selectedDestinationId) {
      fetch(`http://localhost:5163/api/TourDetails?destinationId=${selectedDestinationId}`, {
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
          let filteredTourPackages = data.filter((tourPackage) =>
          tourPackage.tourPrice >= priceRange[0] && tourPackage.tourPrice <= priceRange[1] &&
          Array.isArray(tourPackage.tourDestination) &&
          tourPackage.tourDestination.some(
            (destination) => destination.destinationId === selectedDestinationId
          ) &&
          activeTravelAgents.some((agent) => agent.travelAgentId === tourPackage.travelAgentId)
        );
  
          setResultTourPackage(filteredTourPackages);
        })
        .catch((error) => {
          console.error("Error fetching tour details:", error);
        });
    } else {
      console.log("Please select a destination.");
    }
  };
  

  return (
    <div className="tourpackagesearch">
      <div>
        <Navbar></Navbar>
      </div>
      <div className="container">
        <div className="destinations-container">
          <div className="destinations card" id="destinationcard">
            <div className="card-body">
              <h5 className="card-title">Book Domestic and International Holidays</h5>
              <Select
                className="search"
                placeholder="Select a Destination to Search"
                options={destinationOptions}
                value={destinationOptions.find((option) => option.value === selectedDestinationId)}
                onChange={(selectedOption) => setSelectedDestinationId(selectedOption.value)}
                isSearchable={true}
                autosize={true}
                menuPortalTarget={document.body} // Render the dropdown outside the component's container
              />

              <div className="price-slider">
                <label>Price Range:</label>
                <input
                  type="range"
                  min="0"
                  max={Math.max(...resultTourPackage.map((tourPackage) => tourPackage.tourPrice))}
                  value={priceRange[1]}
                  onChange={(e) => setPriceRange([0, parseInt(e.target.value)])}
                />
                <p>Price: ${0} - ${priceRange[1]}</p>
              </div>

              <button onClick={handleSearchHolidays} className="btn btn-primary mt-2">
                Search Holidays
              </button>
            </div>
          </div>
        </div>
        <div className="tour-packages">
          <div className="sort-header">
            <button onClick={sortByPrice}>
              Sort by Price
            </button>
            <button onClick={sortByDuration}>
              Sort by Duration
            </button>
          </div>

          {resultTourPackage.length === 0 ? (
            <p>No tour packages found.</p>
          ) : (
            resultTourPackage.map((tourPackage) => (
              <div key={tourPackage.id}>
                <div className="card mb-3">
                  <div className="row g-0">
                    {/* First Column: Image */}
                    <div className="col-md-4">
                      <img src={`http://127.0.0.1:10000/devstoreaccount1/tour/tour/${tourPackage.tourImage}`} className="img-fluid rounded-start" alt=".." />
                    </div>
                    {/* Second Column: Text */}
                    <div className="col-md-4">
                      <div className="card-body">
                        <h5 className="card-title">{tourPackage.tourName}</h5>
                        <p className="card-text">
                          {tourPackage.noofdays} Nights / {tourPackage.noofdays + 1} Days
                        </p>
                        <p className="card-text">
                          Destinations:
                          {tourPackage.tourDestination.map((destination, index) => {
                            const destinationData = Destinations.find(
                              (dest) => dest.destinationId === destination.destinationId
                            );
                            const isLastDestination = index === tourPackage.tourDestination.length - 1;

                            return (
                              <span key={destination.id}>
                                {destinationData && destinationData.destinationName}
                                {isLastDestination ? "" : " -> "}
                              </span>
                            );
                          })}
                        </p>
                      </div>
                    </div>
                    {/* Third Column: Price */}
                    <div className="col-md-4">
                      <div className="card-body">
                        <p className="card-text">Price: ${tourPackage.tourPrice}</p>
                        <Link to={`/tour-details/${tourPackage.tourId}`} className="viewdetails no-underline">View Details
                        </Link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            ))
          )}
        </div>
      </div>
    </div>
  );
}

export default TourPackages;
