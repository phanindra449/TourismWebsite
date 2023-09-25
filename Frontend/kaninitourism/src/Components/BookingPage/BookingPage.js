import React, { useState, useEffect } from "react";
import { useParams, useLocation } from "react-router-dom";
import Navbar from "../Navbar/Navbar";
import Modal from "react-modal";
import html2canvas from "html2canvas"; // Import html2canvas
import html2pdf from "html2pdf.js"
import { useNavigate } from "react-router-dom";

import "./BookingPage.css"; // Make sure to link your CSS file here

function BookingPage() {
  const { tourId } = useParams();
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const selectedTourDate = new Date(searchParams.get("date"));
  const formattedDate = selectedTourDate.toLocaleDateString();
  const [bookingSuccessful, setBookingSuccessful] = useState(false);
  const [isBillModalOpen, setIsBillModalOpen] = useState(false);
  const navigate = useNavigate();


  const numTravelers = parseInt(searchParams.get("travelers"));

  const [istravellerModalOpen, setIsModalOpen] = useState(false);
  const [travelers, setTravelers] = useState([]);
  const [initialModalVisibility, setInitialModalVisibility] = useState(false);

  const [email, setEmail] = useState("");
  const [mobile, setMobile] = useState("");
  const [specialRequests, setSpecialRequests] = useState("");
  const [amount, setAmount] = useState();
  const [tourDetails, setTourDetails] = useState(null); 

  useEffect(() => {
    fetchTourDetails();
console.log(tourId)
  }, []);
  useEffect(() => {
    setInitialModalVisibility(true);
  }, []);
  
  useEffect(() => {
    // Log the updated tour details
    console.log("Updated Tour Details:", tourDetails);
    
    if (tourDetails && tourDetails.price) {
      setAmount(numTravelers * tourDetails.tourPrice);
    }
  }, [tourDetails, numTravelers]);
  const fetchTourDetails = async () => {
    try {
      const response = await fetch(`http://localhost:5163/api/TourDetails/${tourId}`);
      if (response.ok) {
        const tourData = await response.json();
        setTourDetails(tourData);
        console.log(tourDetails)
          setAmount(numTravelers * tourData.tourPrice);
          console.log(amount)
        
      } else {
        console.error("Error fetching tour details:", response.statusText);
      }
    } catch (error) {
      console.error("Error fetching tour details:", error);
    }
  };

  const openModal = (index) => {
    setIsModalOpen(index);
  };

  const closeModal = () => {
    setIsModalOpen(null);
  };

    // ... Other code ...
  
    const handleAddTraveler = (name, age, type) => {
      if (name && age && type) {
        const newTraveler = { name, age, type };
        setTravelers([...travelers, newTraveler]);
        closeModal();
        setInitialModalVisibility(false); // Set initialModalVisibility to false when a traveler is added
      } else {
        alert("Please fill in all the fields.");
      }
    };
  
    const handleEditTraveler = (index, name, age, type) => {
      if (name && age && type) {
        const updatedTravelers = [...travelers];
        updatedTravelers[index] = { name, age, type };
        setTravelers(updatedTravelers);
        closeModal();
        setInitialModalVisibility(false); // Set initialModalVisibility to false when a traveler is edited
      } else {
        alert("Please fill in all the fields.");
      }
    };
    const handleDownloadPDF = () => {
      const input = document.getElementById("bill-generate");
      
      const pdfOptions = {
        margin: [10, 10],
        filename: "my_component.pdf",
        image: { type: "jpeg", quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: "mm", format: "a4", orientation: "portrait" },

      };
  
      html2canvas(input).then((canvas) => {
        const pdf = new html2pdf().from(canvas).set(pdfOptions).save();
      });        setIsBillModalOpen(true);// Open the modal after generating PDF

    }
  
    // ... Rest of your code ...
  
  
  const handleSubmit = async (e) => {
    e.preventDefault();
  
    if(tourDetails.maxCapacity<(tourDetails.bookedNoOfSeats+numTravelers)){
      console.log("there are not enough seats available")
    }
    else{
    try {
      var options = {
        key: "rzp_test_26Y25VcVW1BfhO",
        key_secret: "zGD7EFUQb1FCinhkqWit3V2Q",
        amount: amount * 100,
        currency: "INR",
        name: "STARTUP_PROJECTS",
        description: "for testing purpose",
        handler: function (response) {
          alert(response.razorpay_payment_id);
          try {
            handleBookNow();
          } catch (error) {
            console.error("Error occurred while handling booking:", error);
          }
        },
        prefill: {
          name: "phani",
          email: "phani315@gmail.com.com",
          contact: "7075354285",
        },
        notes: {
          address: "Razorpay Corporate office",
        },
        theme: {
          color: "#3399cc",
        },
      };
      var pay = new window.Razorpay(options);
      pay.open();
    } catch (error) {
      console.error("Error occurred while handling payment:", error);
    }
  }
  };
  

  const handleBookNow = async () => {
    const bookingData = {
      tourId: parseInt(tourId),
      userId: 0, 
      travelAgentId: tourDetails.travelAgentId,
      customerName: travelers[0]?.name || "",
      contactEmail: email,
      contactNumber: mobile,
      numberOfParticipants: numTravelers,
      totalPrice: amount,
      bookingDate: new Date().toISOString(),
      bookingStatus: "success", 
      specialRequests: specialRequests,
      passengers: travelers.map((traveler) => ({
        name: traveler.name,
        age: traveler.age,
        gender: traveler.type === "adult" ? "Male" : "Female"
      })),
    };

    try {
      const response = await fetch("http://localhost:5260/api/Booking/AddBooking", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(bookingData),
        });
    
        if (response.ok) {
          console.log("Booking successful!");
          try {
          updateMaxCapacityAndBookedSeats();
          setBookingSuccessful(true); // Set booking successful state
          setIsBillModalOpen(true)
          } catch (updateError) {
            console.error("Error updating booked seats:", updateError);
          }
          
          try {
            const responseData = await response.json(); // Parse response as JSON
            console.log("Response:", responseData); // Print the parsed JSON response
          } catch (error) {
            console.error("Error parsing JSON response:", error);
          }
        } else {
          // Booking failed, handle error scenario
          console.error("Booking failed!");
          
          try {
            const errorData = await response.json(); // Parse error response as JSON
            console.error("Error Response:", errorData); // Print the parsed JSON error response
            if (errorData.errors) {
              for (const field in errorData.errors) {
                console.error(`${field} validation error: ${errorData.errors[field].join(", ")}`);
              }
            }
         
          }
          
          catch (error) {
            console.error("Error parsing JSON error response:", error);
          }
        }
      } catch (error) {
        console.error("Error occurred while booking:", error);
      }
    };

    const updateMaxCapacityAndBookedSeats = async () => {
      try {
        const requestData = {
          tourId: tourDetails.tourId,
          travellerBookedseats: numTravelers
        };
    
        console.log("Sending Data:", requestData);
    
        const response = await fetch(`http://localhost:5163/api/ManageTourDetails/updateBookedSeats`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestData),
        });
    
        if (!response.ok) {
          throw new Error("Failed to update booked seats");
        }
    
        console.log("Booked seats updated successfully");
    
        return response;
      } catch (error) {
        console.error("Error updating max capacity and booked seats:", error);
        throw error;
      }
    };
    
    
    
    const handleEmailChange = (event) => {
      setEmail(event.target.value);
    };
    
    // Update mobile state when the mobile input changes
    const handleMobileChange = (event) => {
      setMobile(event.target.value);
    };
    
    
    
    
    
    
  return (
    <div className="BookingPage">
      <Navbar />
      <div>
        <h2>Booking Details</h2>
        <h3>{numTravelers} Travellers</h3>

        {[...Array(numTravelers)].map((_, index) => (
          <div key={index} className="traveler-details">
            <p>Traveller {index + 1}</p>
            {index < travelers.length ? (
              <div className="traveller-profile">
                <p>PROFILE COMPLETED</p>
                <p>Name: {travelers[index].name}</p>
                <p>Age: {travelers[index].age}</p>
                <p>Type: {travelers[index].type === 'adult' ? 'Adult' : 'Child'}</p>
                <button
                  className="add-traveler-button"
                  onClick={() => openModal(index)}
                >
                  Edit
                </button>
              </div>
            ) : (
              <button className="add-traveler-button" onClick={() => openModal(index)}>
                {"Add Traveller"}
              </button>
            )}
          </div>
        ))}
    
    <div className="contact-gst-details">
          <h3>Contact Details</h3>
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            required
            value={email}
            onChange={handleEmailChange} // Add this line
          />

          <label htmlFor="mobile">Mobile:</label>
          <input
            type="tel"
            id="mobile"
            value={mobile}
            onChange={handleMobileChange} // Add this line
          />

          <h3>Special Requests</h3>
          <textarea
            id="specialRequests"
            rows="4"
            value={specialRequests}
            onChange={(e) => setSpecialRequests(e.target.value)}
          />
        </div>
 
        {bookingSuccessful ? (
          <div className="booking-successful">
            <h3>Booking Successful!</h3>
            <button className="download-bill-button" onClick={handleDownloadPDF}>
              Download Bill
            </button>
            <Modal
              isOpen={isBillModalOpen}
              onRequestClose={() => {
                setIsBillModalOpen(false);
                navigate('/TourPackage'); 
              }}
              className="bill-modal"
              overlayClassName="react-modal-overlay"
              contentLabel="Bill Modal"
            >
              <span
                className="close-icon"
                onClick={() => {setIsBillModalOpen(false);
                  navigate('/TourPackage'); 

                }}
              >
                X
              </span>
              <div className="bill-content">
                <h2>Bill Details</h2>
                {/* Here you can display the billing information */}
                <p>Total Price: {amount}</p>
                <p>Contact Email: {email}</p>
                <p>Contact Number: {mobile}</p>
                {/* ... Add more relevant billing details */}
                <button className="download-bill-button" onClick={handleDownloadPDF}>
                  Download Bill
                </button>
              </div>
            </Modal>
          </div>
        ) : (
          <button className="book-now-button" onClick={handleBookNow}>
            Book Now
          </button>
        )}


        <div id="bill-generate" className="bill-generate">
          thank you
        </div>
        {initialModalVisibility && (
          <Modal
            isOpen={istravellerModalOpen !== null}
            onRequestClose={closeModal}
            className="addtraveller-modal1"
            overlayClassName="react-modal-overlay"
            contentLabel="Add Traveler Modal"
          >
            <span className="close-icon" onClick={closeModal}>
              X
            </span>
            <h2>{istravellerModalOpen === 0 ? "Edit" : "Add"} Traveler</h2>
            <form onSubmit={(e) => e.preventDefault()} className="modal-form">
              <label htmlFor="name">Name:</label>
              <input type="text" id="name" required />
              <label htmlFor="age">Age:</label>
              <input
                type="number"
                id="age"
                required
                min={1}
                max={100}
              />
              <label htmlFor="type">Type:</label>
              <select id="type">
                <option value="adult">Adult</option>
                <option value="child">Child</option>
              </select>
              <button
                type="submit"
                onClick={(e) => {
                  e.preventDefault();
                  const name = document.getElementById("name").value;
                  const age = document.getElementById("age").value;
                  const type = document.getElementById("type").value;
                  if (istravellerModalOpen === 0) {
                    handleEditTraveler(istravellerModalOpen, name, age, type);
                  } else {
                    handleAddTraveler(name, age, type);
                  }
                }}
              >
                {istravellerModalOpen === 0 ? "Save" : "Add"}
              </button>
              <button onClick={closeModal}>Cancel</button>
            </form>
          </Modal>
        )}


        
      </div>
    </div>
  );
}

export default BookingPage;
