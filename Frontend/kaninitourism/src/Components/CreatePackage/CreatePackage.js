import React, { useState,useEffect } from "react";
import Navbar from "../Navbar/Navbar";
import { Link } from "react-router-dom";
import { BlobServiceClient } from "@azure/storage-blob";



import "./CreatePackage.css"; 

function CreatePackage() {

    const [destinationNames, setDestinationNames] = useState([]);
    const [inclusionNames, setInclusionNames] = useState([]);
    const [exclusionNames, setExclusionNames] = useState([]);
    var [images, setImages] = useState([]);


    useEffect(() => {
        const fetchDestinationNames = async () => {
          try {
            const response = await fetch("http://localhost:5163/api/Destinations");
            if (response.ok) {
              const destinations = await response.json();
              const names = destinations.map((destination) => destination.destinationName);
              setDestinationNames(names);
            } else {
              console.error("Failed to fetch destination names");
            }
          } catch (error) {
            console.error("Error fetching destination names:", error);
          }
        };
    
        fetchDestinationNames();
      }, []);
    
      useEffect(() => {
        const fetchExclusionNames = async () => {
          try {
            const response = await fetch("http://localhost:5163/api/Exclusions");
            if (response.ok) {
              const exclusions = await response.json();
              const names = exclusions.map((exclusion) => exclusion.exclusionDescription);
              setExclusionNames(names);
            } else {
              console.error("Failed to fetch exclusion names");
            }
          } catch (error) {
            console.error("Error fetching exclusion names:", error);
          }
        };
      
        fetchExclusionNames();
      }, []);

useEffect(() => {
  const fetchInclusionNames = async () => {
    try {
      const response = await fetch("http://localhost:5163/api/Inclusions");
      if (response.ok) {
        const inclusions = await response.json();
        const names = inclusions.map((inclusion) => inclusion.inclusionDescriptionn);
        setInclusionNames(names);
      } else {
        console.error("Failed to fetch inclusion names");
      }
    } catch (error) {
      console.error("Error fetching inclusion names:", error);
    }
  };

  fetchInclusionNames();
}, []);



    const [tourDetails, setTourDetails] = useState({
        tourName: "",
        travelAgentId:7,
        tourDescription: "",
        tourTheme: "",
        tourType: "",
        tourPrice: 0,
        noOfDays: 0,
        maxCapacity: 0,
        bookedNoOfSeats: 0,
        availability: true,
        tourImage: "",
        tourDestination: [
          {
            destinationImage: "",
            dayNo: 0,
            eventTime: "",
            activityType: "",
            destinationActivity: "",
            activityName: ""
          }
        ],
        tourDate: [
          {
            departureDate: "",
            returnDate: ""
          }
        ],
        tourInclusion: [],
        tourExclusion: []
      });

  
      const handleSubmit = async (e) => {
        e.preventDefault();

        const AZURITE_BLOB_SERVICE_URL = 'http://localhost:10000';
        const ACCOUNT_NAME = 'devstoreaccount1';
        const ACCOUNT_KEY = 'Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==';
    

        const blobServiceClient = new BlobServiceClient(
          "http://127.0.0.1:10000/devstoreaccount1/tour?sv=2018-03-28&st=2023-08-07T14%3A17%3A59Z&se=2023-08-08T14%3A17%3A59Z&sr=c&sp=racwdl&sig=c%2BTV%2F7%2BdU7R7CwmnuqsRxAR16HBwheT0vZY2%2BmDOSzM%3D",
"sv=2018-03-28&st=2023-08-07T14%3A17%3A59Z&se=2023-08-08T14%3A17%3A59Z&sr=c&sp=racwdl&sig=c%2BTV%2F7%2BdU7R7CwmnuqsRxAR16HBwheT0vZY2%2BmDOSzM%3D"      );
      const containerClient = blobServiceClient.getContainerClient('tour');
      console.log(images, "imgae");
      for (let i = 0; i < images.length; i++) {
//         const selectedImage = images[i];
// console.log(selectedImage); // Check the selected image object
          const blobClient = containerClient.getBlobClient(images[i].name);
          const blockBlobClient = blobClient.getBlockBlobClient();
          const result = blockBlobClient.uploadBrowserData(images[i], {
              blockSize: 4 * 1024 * 1024,
              concurrency: 20,
              onProgress: ev => console.log(ev)
          });
          console.log(result, "result");
      }



        try {
          const response = await fetch("http://localhost:5163/api/TourDetails", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              ...tourDetails,
              travelAgentId: 7,
              bookedNoOfSeats: 0,
              availability: tourDetails.availability === "true",
              maxCapacity: parseInt(tourDetails.maxCapacity, 10),
              noOfDays: parseInt(tourDetails.noOfDays, 10),
              tourPrice: parseFloat(tourDetails.tourPrice),
            }),
          });
      
          console.log("Data to be sent:", {
            ...tourDetails,
            travelAgentId: 7,
            bookedNoOfSeats: 0,
            availability: tourDetails.availability === "true",
            maxCapacity: parseInt(tourDetails.maxCapacity, 10),
            noOfDays: parseInt(tourDetails.noOfDays, 10),
            tourPrice: parseFloat(tourDetails.tourPrice),
          });
      
          console.log("Response:", response);
      
          if (response.ok) {
            const responseBody = await response.json();
            console.log("Tour added successfully!", responseBody);
          } else {
            const responseBody = await response.json();
            console.error("Failed to add tour:", responseBody);
          }
        } catch (error) {
          console.error("Error occurred while adding tour:", error);
        }
      };
      
  

  
  
    const handleInputChange = (event) => {
        const { name, value } = event.target;
      
        // Convert specific fields to numbers if needed
        let updatedValue = value;
        if (name === "tourPrice" || name === "maxCapacity" || name === "noOfDays") {
          updatedValue = parseFloat(value);
        }
      
        setTourDetails((prevDetails) => ({
          ...prevDetails,
          [name]: updatedValue,
        }));
      };
      const handleDestinationChange = (index, field, value) => {
        const updatedDestinations = [...tourDetails.tourDestination];
        
        if (field === "destinationImage") {
          const selectedImage = images[0]; // Assuming you want to use the first selected image for each destination
          updatedDestinations[index].destinationImage = selectedImage.name;
        } else {
          updatedDestinations[index][field] = value;
        }
      
        setTourDetails((prevDetails) => ({
          ...prevDetails,
          tourDestination: updatedDestinations,
        }));
      };
      
      
      

  const handleDateChange = (index, field, value) => {
    const updatedDates = [...tourDetails.tourDate];
    updatedDates[index][field] = value;
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourDate: updatedDates,
    }));
  };

  const addTourDestination = () => {
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourDestination: [
        ...prevDetails.tourDestination,
        {
          destinationId: 0,
          destinationImage: "",
          dayNo: 0,
          eventTime: "",
          activityType: "",
          destinationActivity: "",
          activityName: "",
        },
      ],
    }));
  };

  const addTourDate = () => {
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourDate: [
        ...prevDetails.tourDate,
        {
          departureDate: "",
          returnDate: "",
        },
      ],
    }));
  };
  const addTourInclusion = () => {
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourInclusion: [
        ...prevDetails.tourInclusion,
        {
          inclusionId: 0,
        },
      ],
    }));
  };
  
  const addTourExclusion = () => {
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourExclusion: [
        ...prevDetails.tourExclusion,
        {
          id: 0,
          tourId: 0,
          exclusionId: 0 // Reduce index by 1
        },
      ],
    }));
  };
  
  const handleInclusionChange = (index, field, value) => {
    const updatedInclusions = [...tourDetails.tourInclusion];
    updatedInclusions[index][field] = value;
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourInclusion: updatedInclusions,
    }));
  };
  
  const handleExclusionChange = (index, field, value) => {
    const updatedExclusions = [...tourDetails.tourExclusion];
    updatedExclusions[index][field] = value;
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourExclusion: updatedExclusions,
    }));
  };
  

  return (
    <div className="AddTourPage">
      <div>
        <Navbar></Navbar>
      </div>
     <div className="add-tour-form">
        <h2>Add New Tour</h2>
        <form onSubmit={handleSubmit}>
        <div className="travelagent-tourdetails">

          <label htmlFor="tourName">Tour Name:</label>
          <input
            type="text"
            id="tourName"
            name="tourName"
            value={tourDetails.tourName}
            onChange={handleInputChange}
            required
          />
          <label htmlFor="tourDescription">Tour Description:</label>
          <textarea
            id="tourDescription"
            name="tourDescription"
            value={tourDetails.tourDescription}
            onChange={handleInputChange}
            required
          />
          <label htmlFor="tourTheme">Tour Theme:</label>
          <input
            type="text"
            id="tourTheme"
            name="tourTheme"
            value={tourDetails.tourTheme}
            onChange={handleInputChange}
            required
          />
          <label htmlFor="tourType">Tour Type:</label>
          <input
            type="text"
            id="tourType"
            name="tourType"
            value={tourDetails.tourType}
            onChange={handleInputChange}
            required
          />
         <label htmlFor="tourPrice">Tour Price:</label>
<input
  type="number"
  id="tourPrice"
  name="tourPrice"
  value={tourDetails.tourPrice}
  onChange={handleInputChange}
  required
/>

          <label htmlFor="noOfDays">Number of Days:</label>
          <input
            type="number"
            id="noOfDays"
            name="noOfDays"
            value={tourDetails.noOfDays}
            onChange={handleInputChange}
            required
          />
          <label htmlFor="maxCapacity">Max Capacity:</label>
          <input
            type="number"
            id="maxCapacity"
            name="maxCapacity"
            value={tourDetails.maxCapacity}
            onChange={handleInputChange}
            required
          />
          <label htmlFor="tourImage">Tour Image URL:</label>
          <input
  type="file"
  id="tourImage"
  name="tourImage"
  // value={tourDetails.tourImage}
  variant="outlined"
  multiple
  onChange={(event) => {
    const selectedImages = event.target.files;
    setImages(selectedImages);
    setTourDetails((prevDetails) => ({
      ...prevDetails,
      tourImage: selectedImages[0].name,
    }));
  }}
  required
/>

          </div>
        <div className="travelagent-tourdestination">
 <h3>Tour Destinations</h3>
          {tourDetails.tourDestination.map((destination, index) => (
            <div key={index}>
              <h4>Destination {index + 1}</h4>
              <label htmlFor={`destinationName-${index}`}>Destination Name:</label>
              <select
                id={`destinationName-${index}`}
                name={`destinationName-${index}`}
                value={destination.destinationName}
                onChange={(e) => {
                  const selectedDestinationName = e.target.value;
                  const destinationId = destinationNames.findIndex(
                    (name) => name === selectedDestinationName
                  );
                  handleDestinationChange(index, "destinationId", destinationId+1);
                }}
                required
              >
                <option value="" disabled>Select Destination</option>
                {destinationNames.map((name, idx) => (
                  <option key={idx} value={name}>
                    {name}
                  </option>
                ))}
              </select>





              <label htmlFor={`destinationImage-${index}`}>Destination Image URL:</label>
<input 
  type="file"
  onChange={(event) => {
    const selectedImages = event.target.files;
    setImages(selectedImages);
    handleDestinationChange(index, "destinationImage", selectedImages[0].name);
  }}
  required
/>


              <label htmlFor={`dayNo-${index}`}>Day Number:</label>
              <input
                type="number"
                id={`dayNo-${index}`}
                name={`dayNo-${index}`}
                value={destination.dayNo}
                onChange={(e) => handleDestinationChange(index, "dayNo", e.target.value)}
                required
              />

              <label htmlFor={`eventTime-${index}`}>Event Time:</label>
              <input
                type="text"
                id={`eventTime-${index}`}
                name={`eventTime-${index}`}
                value={destination.eventTime}
                onChange={(e) =>
                  handleDestinationChange(index, "eventTime", e.target.value)
                }
                required
              />

              <label htmlFor={`activityType-${index}`}>Activity Type:</label>
              <input
                type="text"
                id={`activityType-${index}`}
                name={`activityType-${index}`}
                value={destination.activityType}
                onChange={(e) =>
                  handleDestinationChange(index, "activityType", e.target.value)
                }
                required
              />

              <label htmlFor={`destinationActivity-${index}`}>Destination Activity:</label>
              <input
                type="text"
                id={`destinationActivity-${index}`}
                name={`destinationActivity-${index}`}
                value={destination.destinationActivity}
                onChange={(e) =>
                  handleDestinationChange(index, "destinationActivity", e.target.value)
                }
                required
              />

              <label htmlFor={`activityName-${index}`}>Activity Name:</label>
              <input
                type="text"
                id={`activityName-${index}`}
                name={`activityName-${index}`}
                value={destination.activityName}
                onChange={(e) =>
                  handleDestinationChange(index, "activityName", e.target.value)
                }
                required
              />

              {/* Add more input fields for other destination fields */}
            </div>
          ))}

          <button type="button" onClick={addTourDestination}>
            Add Tour Destination
          </button>
</div>

        


              <h3>Tour Dates</h3>
          {tourDetails.tourDate.map((date, index) => (
            <div key={index}>
              <h4>Tour Date {index + 1}</h4>
              <label htmlFor={`departureDate-${index}`}>Departure Date:</label>
              <input
                type="date"
                id={`departureDate-${index}`}
                name={`departureDate-${index}`}
                value={date.departureDate}
                onChange={(e) => handleDateChange(index, "departureDate", e.target.value)}
                required
              />

              <label htmlFor={`returnDate-${index}`}>Return Date:</label>
              <input
                type="date"
                id={`returnDate-${index}`}
                name={`returnDate-${index}`}
                value={date.returnDate}
                onChange={(e) => handleDateChange(index, "returnDate", e.target.value)}
                required
              />

              {/* Add more input fields for other date fields */}
            </div>
          ))}

          <button type="button" onClick={addTourDate}>
            Add Tour Date
          </button>


          <h3>Tour Inclusions</h3>
{tourDetails.tourInclusion.map((inclusion, index) => (
  <div key={index}>
    <h4>Inclusion {index + 1}</h4>
    <label htmlFor={`inclusionName-${index}`}>Inclusion Name:</label>
    <select
      id={`inclusionName-${index}`}
      name={`inclusionName-${index}`}
      value={inclusion.inclusionDescriptionn}
      onChange={(e) => {
        const selectedInclusionName = e.target.value;
        const inclusionId = inclusionNames.findIndex(
          (name) => name === selectedInclusionName
        );
        handleInclusionChange(index, "inclusionId", inclusionId+1);
      }}
      required
    >
      <option value="" disabled>Select Inclusion</option>
      {inclusionNames.map((name, idx) => (
        <option key={idx} value={name}>
          {name}
        </option>
      ))}
    </select>
  </div>
))}


<button type="button" onClick={addTourInclusion}>
  Add Tour Inclusion
</button>

<h3>Tour Exclusions</h3>
{tourDetails.tourExclusion.map((exclusion, index) => (
  <div key={index}>
    <h4>Exclusion {index + 1}</h4>
    <label htmlFor={`exclusionName-${index}`}>Exclusion Name:</label>
    <select
      id={`exclusionName-${index}`}
      name={`exclusionName-${index}`}
      value={exclusion.exclusionName}
      onChange={(e) => {
        const selectedExclusionName = e.target.value;
        const exclusionId = exclusionNames.findIndex(
          (name) => name === selectedExclusionName
        );
        handleExclusionChange(index, "exclusionId", exclusionId+1);
      }}
      required
    >
      <option value="" disabled>Select Exclusion</option>
      {exclusionNames.map((name, idx) => (
        <option key={idx} value={name}>
          {name}
        </option>
      ))}
    </select>
  </div>
))}


<button type="button" onClick={addTourExclusion}>
  Add Tour Exclusion
</button>

          
          <button type="submit">Submit</button>
        </form>
      </div>
    </div>
  );
}

export default CreatePackage;
