import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ImageGallery = () => {
    const [images, setImages] = useState([]);
    const apiBaseUrl = 'http://localhost:7058/api/User/';
    useEffect(() => {
        const fetchImages = async () => {
            try {
                const response = await axios.get(apiBaseUrl);
                console.log(response.data);
                setImages(response.data);
            } catch (error) {
                console.error('Error fetching images:', error);
            }
        };
        fetchImages();
    }, [apiBaseUrl]);

    return (
        <div>
            <div>
                {images.map((image) => (
                    <div key={image.id} >
                        <table>
                            <tr>
                                <td>
                                    <img src={image.imagePath} alt={image.name} width='300' height='200' />

                                    <h6>{image.name}</h6></td>
                            </tr>
                        </table>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ImageGallery;
