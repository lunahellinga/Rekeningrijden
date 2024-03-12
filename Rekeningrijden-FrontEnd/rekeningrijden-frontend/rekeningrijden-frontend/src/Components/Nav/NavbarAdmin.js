import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function NavbarAdmin() {
  return (
    <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" class="navbar navbar-expand-lg navbar-dark bg-primary">
      <Container>
        <Navbar.Brand href="#home">Rekeningrijden</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="#carsAll">Auto's</Nav.Link>
            <Nav.Link href="#routesAdmin">Ritten</Nav.Link>            
          </Nav>          
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavbarAdmin;