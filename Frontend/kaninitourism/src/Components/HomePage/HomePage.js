import React from "react";
import './HomePage.css'
import logo from '../images/logo.png'
import b1 from '../images/b1.jpg'
import p1 from '../images/p1.jpg'
import p2 from '../images/p2.png'
import p3 from '../images/p3.png'
import p4 from '../images/p4.png'
import p5 from '../images/p5.png'
import aboutimage from '../images/about-img.png'
import wifi from '../images/wifi.png'
import food from '../images/food.png'
import deluxroom from '../images/deluxroom.png'
import freemedication from '../images/freemedication.png'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { Link } from "react-router-dom";

import Navbar from "../Navbar/Navbar";
function HomePage(){



    return(

        <div className="home">
            <Navbar></Navbar>

<div className="header">

                
   <section class="home" id="home">
        <div class="main-text">
            <h3>Travel The World
                <br/> & Explore The New Destination
            </h3>
        
        <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Porro molestias nemo, harum debitis error itaque?</p>
        <a href='/TourPackage'><button id="home-btn">PACKAGES <i class="fa-solid fa-arrow-down"></i><button id="home-btn2"><i class="fa-sharp fa-solid fa-circle-play"></i></button></button></a>
    </div>
    </section>


    <section class="services" id="services">
        <div class="heading">
            <h3>Services</h3>
        </div>
            <div class="card-content">

                <div class="row">
                    <i class="fas fa-globe-asia"></i>
                    <div class="card-body">
                    <img src={deluxroom} style={{width:"50px",height:'50px'}} alt=""/>

                        <h3>Around The World</h3>
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente vero suscipit eos quam explicabo quas.</p>
                    </div>
                </div>
                <div class="row">
                    <i class="fas fa-plane"></i>
                    <div class="card-body">
                    <img src={food} style={{width:"50px",height:'50px'}} alt=""/>

                        <h3>Fastet Travel</h3>
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente vero suscipit eos quam explicabo quas.</p>

                    </div>
                </div>
                <div class="row">
                    <i class="fas fa-hotel"></i>
                    <div class="card-body">
                    <img src={wifi}  style={{width:"50px",height:'50px'}}alt=""/>

                        <h3>Affordable Hotels</h3>
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente vero suscipit eos quam explicabo quas.</p>

                    </div>
                </div>
                <div class="row">
                    <i class="fas fa-bullhorn"></i>
                    <div class="card-body">
                    <img src={freemedication}style={{width:"50px",height:'50px'}} alt=""/>

                        <h3>Safty Guide</h3>
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente vero suscipit eos quam explicabo quas.</p>

                    </div>
                </div>

            </div>

       
    </section>



    <section class="about" id="about">
        <div class="about-img">
            <img src={aboutimage} alt=""/>
        </div>
        <div class="about-info">
            <h6>About us</h6>
            <h3>How Work This Travel Agency</h3>
            <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Ratione odio vitae tempora nesciunt quibusdam? Delectus voluptatibus neque quod, id cumque blanditiis, suscipit temporibus ipsam aspernatur sunt voluptatem repellat corrupti amet at velit, minus reprehenderit voluptate nobis asperiores maxime deleniti quibusdam. Numquam sequi iusto consequatur obcaecati, incidunt quia accusantium perspiciatis expedita.</p>

            <button class="about-btn">Read More...</button>
        </div>
    </section>




    <section class="gallary" id="gallary">
        <div class="heading">
            <h3>OUR GALLARY <i class="fa-solid fa-arrow-down"></i></h3>
        </div>

        <div class="gallary-card">
            <div class="row">
                <img src={p1} style={{width:"200px",height:'200px'}} alt=""/>
            </div>
            <div class="row">
                <img src={p2}style={{width:"200px",height:'200px'}} alt=""/>
            </div>
            <div class="row">
                <img src={p3} style={{width:"200px",height:'200px'}}alt=""/>
            </div>
            <div class="row">
                <img src={p4}style={{width:"200px",height:'200px'}} alt=""/>
            </div>
            <div class="row">
                <img src={p5} style={{width:"200px",height:'200px'}}alt=""/>
            </div>
           

        </div>
    </section>





  





    
        
        </div>




    <section class="reviews" id="reviews">
        <div class="main-txt">
            <h3>What Custommers Say</h3>
        </div>
        <div class="card-content">
            <div class="row">
                <h5><img src="./images/pic-1.png" alt=""/>Elon Musk</h5>
                <div class="rating">
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                </div>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Veniam a earum commodi exercitationem explicabo amet repudiandae nemo nesciunt beatae omnis.</p>

            </div>
            <div class="row">
                <h5><img src="./images/pic-2.png" alt=""/>Elon Musk</h5>
                <div class="rating">
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                </div>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Veniam a earum commodi exercitationem explicabo amet repudiandae nemo nesciunt beatae omnis.</p>

            </div>
            <div class="row">
                <h5><img src="./images/pic-3.png" alt=""/>Elon Musk</h5>
                <div class="rating">
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                    <i class="fa-solid fa-star"></i>
                </div>
                <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Veniam a earum commodi exercitationem explicabo amet repudiandae nemo nesciunt beatae omnis.</p>

            </div>
        </div>
    </section>










    <footer id="footer">
        <div class="footer-content">
            <div class="row" id="row1">
                <a href="#" id="footer-logo">Travel Agency</a>
                <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Reiciendis ratione voluptatem asperiores minima quidem quibusdam tenetur eligendi eaque incidunt. Laudantium?</p>
                <div class="socail-links">
                    <i class="fa-brands fa-twitter"></i>
                    <i class="fa-brands fa-facebook-f"></i>
                    <i class="fa-brands fa-instagram"></i>
                    <i class="fa-brands fa-youtube"></i>
                    <i class="fa-brands fa-pinterest-p"></i>
                </div>
            </div>
            <div class="row" id="row2">
                <h3>UseFull Links</h3>
                <ul>
                    <li><a href="#">Home</a></li>
                    <li><a href="#">Packages</a></li>
                    <li><a href="#">Book</a></li>
                    <li><a href="#">About us</a></li>
                    <li><a href="#">Contact us</a></li>
                </ul>
            </div>
            <div class="row" id="row3">
                <h3>Other Links</h3>
                <ul>
                    <li><a href="#">Web Design</a></li>
                    <li><a href="#">App Design</a></li>
                    <li><a href="#">Game Design</a></li>
                    <li><a href="#">Term & Condition</a></li>
                    <li><a href="#">Privacy Policy</a></li>
                </ul>
            </div>
            <div class="row" id="row4">
                <h3>Download App</h3>
                <img src="./images/app.png" alt=""/>
            </div>
        </div>
    </footer>






        </div>
    )
}
export default HomePage