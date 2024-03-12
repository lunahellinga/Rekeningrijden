import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { NavbarData } from './NavbarData';
import { LinkContainer } from 'react-router-bootstrap';
import { Outlet } from 'react-router-dom';

function NavBar() {
  return (
    <div>
      <Navbar collapseOnSelect expand="lg" className="bg-primary navbar-dark">
        <Container>
          <Navbar.Toggle aria-controls="responsive-navbar-nav" />
          <Navbar.Collapse id="responsive-navbar-nav">
            <Nav className="me-auto">
              {NavbarData.map((val, key) => {
                return (
                  <LinkContainer to={`${val.link}`} >
                    <Nav.Link key={key}>
                      <div id="title">{val.title}</div>
                    </Nav.Link>
                  </LinkContainer>
                )
              })}
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
      <Outlet />
    </div >
  )
}


export default NavBar;