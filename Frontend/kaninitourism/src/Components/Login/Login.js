import React, { useState } from 'react';
import './Login.css';
import Carousel from '../Login/carousle';
import { useNavigate } from 'react-router-dom';


import { Link } from 'react-router-dom';
const LoginModal = ({ isOpen, onClose }) => {

  const navigate = useNavigate();

  const [errors, setErrors] = useState({});
  var [error, seterror] = useState(null);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [user, setUser] = useState({
    userId: 0,
    email: '',
    password: '',
    role: '',
    token: ''
  });
  
  const submitThis = () => {
    const userData = {
      ...user,
      email: email,
      password: password,
    };

    fetch('http://localhost:5292/api/Login/Login', {
      method: 'POST',
      headers: {
        accept: 'text/plain',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(userData),
    })
      .then(async (data) => {
        console.log(userData.email, userData.password);
        if (data.status === 400) {
          alert('Invalid credentials');
          seterror('invalid');
          console.log('error');
        } else {
          var myData = await data.json();
          console.log(myData);

          setUser({
            ...user,
            email: userData.email,
            password: userData.password,
            role: myData.role,
            token: myData.token,
          });

          localStorage.setItem('token', myData.token);
          localStorage.setItem('role', myData.role);

          if (myData.role === 'TravelAgent') {
            localStorage.setItem('email', userData.email);
            alert('Login successful');
            navigate('/CreatePackage');
          } else if (myData.role === 'Admin') {
            alert('Login successful');
            navigate('/AdminLandingPage');
            onClose();
          } else if (myData.role.toLowerCase() === 'traveller') {
            alert("logged in as traveller")
            localStorage.setItem('userId', myData.userId);
             onClose();
          }
          else{

            onclose();
          }
          sessionStorage.setItem('role', myData.role);

          console.log(myData);
        }
      })
      .catch((err) => {
        console.log(err.error);
      });
  };

  

  return (
    <div className={`modal ${isOpen ? 'show' : ''}`} tabIndex="-1" role="dialog">
      <div className='modal-dialog' role='document'>
        <div className='modal-content'>
          <div className='modal-header text-center'>
            <h4 className='modal-title w-100 font-weight-bold'>Sign in</h4>
            <button type='button' className='close' onClick={onClose} aria-label='Close'>
              <span aria-hidden='true'>&times;</span>
            </button>
          </div>
          <div className='modal-body mx-3'>
            <div className='md-form mb-5'>
              <i className='fas fa-envelope prefix grey-text'></i>
              <input
                type='email'
                id='defaultForm-email'
                className='form-control validate'
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              <label data-error='wrong' data-success='right' htmlFor='defaultForm-email'>
                Your email
              </label>
            </div>

            <div className='md-form mb-4'>
              <i className='fas fa-lock prefix grey-text'></i>
              <input
                type='password'
                id='defaultForm-pass'
                className='form-control validate'
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
              <label data-error='wrong' data-success='right' htmlFor='defaultForm-pass'>
                Your password
              </label>
            </div>
          </div>
          <div className='modal-footer d-flex justify-content-center'>
            <button className='btn btn-default' onClick={submitThis}>
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginModal;
