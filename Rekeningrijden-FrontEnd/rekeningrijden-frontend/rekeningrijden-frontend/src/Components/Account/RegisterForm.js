import { Button, Form, Row, Col } from 'react-bootstrap';

function RegisterForm() {
    return (
        <Form>
            <Row className="align-items-center">
                <Col xs="auto">
                    <Form.Label htmlFor="inlineFormInput" visuallyHidden>
                        Name
                    </Form.Label>
                    <Form.Control
                        className="mb-2"
                        id="inlineFormInput"
                        placeholder="Jane Doe"
                    />
                </Col>
                <Col xs="auto">
                    <Form.Label htmlFor="inlineFormInputGroup" visuallyHidden>
                        Username
                    </Form.Label>
                    <InputGroup className="mb-2">
                        <InputGroup.Text>@</InputGroup.Text>
                        <Form.Control id="inlineFormInputGroup" placeholder="Username" />
                    </InputGroup>
                </Col>
            </Row>
            <Row className="mb-3">
                <Form.Group as={Col} controlId="formGridEmail">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" />
                </Form.Group>
            </Row>

            <Form.Group className="mb-3" controlId="formGridAddress1">
                <Form.Label>Address</Form.Label>
                <Form.Control placeholder="1234 Main St" />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formGridAddress2">
                <Form.Label>Address 2</Form.Label>
                <Form.Control placeholder="Apartment, studio, or floor" />
            </Form.Group>

            <Row className="mb-3">
                <Form.Group as={Col} controlId="formGridCity">
                    <Form.Label>City</Form.Label>
                    <Form.Control />
                </Form.Group>

                <Form.Group as={Col} controlId="formGridState">
                    <Form.Label>State</Form.Label>
                    <Form.Select defaultValue="Choose...">
                        <option>Choose...</option>
                        <option>...</option>
                    </Form.Select>
                </Form.Group>

                <Form.Group as={Col} controlId="formGridZip">
                    <Form.Label>Zip</Form.Label>
                    <Form.Control />
                </Form.Group>
            </Row>

            <Button variant="primary" type="submit">
                Submit
            </Button>
        </Form>
    );
}

export default RegisterForm;