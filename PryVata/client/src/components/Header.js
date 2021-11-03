import React, { useEffect, useState } from 'react';
import { NavLink as RRNavLink } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink
} from 'reactstrap';
import { logout } from '../modules/authManager';
import { getCurrentUser } from '../modules/userManager';
import pryvataLogo from "./image/pryvataLogo.png"

export default function Header({ isLoggedIn}) {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  const [ user, setUser] = useState({})

  useEffect(() => {
    if(isLoggedIn){
      getCurrentUser().then((user) => setUser(user))
    }
  }, [isLoggedIn]);

  console.log(user, "us")

  return (
    <div>
      <Navbar color="#f9faf4" light expand="md">
        <NavbarBrand tag={RRNavLink} to="/">
      <img  className="logo" src={pryvataLogo} alt="Pryvata"/></NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            { /* When isLoggedIn === true, we will render the Home link */}
            {isLoggedIn &&
              <NavItem>
                <NavLink tag={RRNavLink} to="/">Home</NavLink>
              </NavItem>              
            }
            
            {user.userTypeId === 1 ? <>
            {isLoggedIn &&
            <NavItem>
            <NavLink tag={RRNavLink} to="/incident">Incident List</NavLink>
          </NavItem>}
          {isLoggedIn &&
            <NavItem>
            <NavLink tag={RRNavLink} to="/myIncidents">My Incidents</NavLink>
          </NavItem>} </> : <> {isLoggedIn && <>
            <NavItem>
            <NavLink tag={RRNavLink} to="/myIncidents">My Incidents</NavLink>
          </NavItem></>}</>}
          {isLoggedIn &&
            <NavItem>
            <NavLink tag={RRNavLink} to="/incident/add">New Incident</NavLink>
          </NavItem>}
          </Nav>


          <Nav navbar>
            {isLoggedIn &&
              <>
                <NavItem>
                  <a aria-current="page" className="nav-link"
                    style={{ cursor: "pointer" }} onClick={logout}>Logout</a>
                </NavItem>
              </>
            }
            {!isLoggedIn &&
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/login">Login</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/register">Register</NavLink>
                </NavItem>
              </>
            }
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}
