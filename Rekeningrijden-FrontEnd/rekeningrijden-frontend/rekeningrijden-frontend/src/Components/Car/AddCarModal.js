import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import FloatingLabel from 'react-bootstrap/esm/FloatingLabel';
import axios from 'axios';
import { useState, useEffect } from 'react';

function AddCarModal() {
    const [show, setShow] = useState(false);
    const reload=()=>window.location.reload();
    
    const handleClose = () => {
        setShow(false)
        reload();
      };
    
    const handleShow = () => setShow(true);
    const [car, setCar] = useState({ id: 0, name: '', description: '', carTypeId: 0});
    const [validated, setValidated] = useState(false);
    const [carTypes, setCarTypes] = useState([]);

    useEffect(() => {
        async function fetchdata(){ 
        const carTypeResponse = await axios.get('http://localhost:3306/api/CarType')
        setCarTypes(carTypeResponse.data);}
        fetchdata();
    }, [])

    const handleSubmit = async (event) => {
        console.log(car)
        const form = event.currentTarget;
        event.preventDefault();

        if (form.checkValidity() === false) {
            event.stopPropagation();
        }

        setValidated(true)

        try {           
            const response = await axios.post('http://localhost:3306/api/Car', car);
            handleClose();           
        }
        catch {
            window.alert("item niet aangemaakt!");
        }
    }

    const updateField = e => {
        setCar({
            ...car,
            [e.target.name]: e.target.value
        });
    };
    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Auto Toevoegen
            </Button>

            <Modal show={show} onHide={handleClose} size="lg">
                <Modal.Header closeButton>
                    <Modal.Title>Nieuwe Auto</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="add-car-form" noValidate validated={validated} onSubmit={handleSubmit}>
                        <Form.Group as={Row} className="mb-3" controlId="formName">
                            <Form.Label column sm="2">
                                Naam:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control type="text" placeholder="auto's naam" name="Name" onChange={updateField} required />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} className="mb-3" controlId="formName">
                            <Form.Label column sm="2">
                                Beschrijving:
                            </Form.Label>
                            <Col sm="10">
                                <Form.Control type="text" placeholder="beschrijving" name="Description" onChange={updateField} required />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Col>
                        </Form.Group>
                        <Form.Group>
                            <Row className="mb-3">
                                <Col sm="7">
                                    <FloatingLabel controlId="CartypeSelect" label="Auto Type">
                                        <Form.Select aria-label="Cartype" name="carTypeId" onChange={updateField} required>
                                            <option value="">Kies type</option>
                                            {
                                                carTypes.map(carType => (
                                                    <option key={carType.id} value={carType.id}>{carType.name}</option>
                                                ))
                                            }
                                        </Form.Select>
                                    </FloatingLabel>
                                </Col>                               
                            </Row>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button form="add-car-form" variant="primary" type="submit">
                        Opslaan
                    </Button>
                    <Button variant="secondary" onClick={handleClose}>
                        Sluiten
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default AddCarModal