import { Button, Form } from 'react-bootstrap';
import axios from 'axios';
import { useState, useEffect } from 'react';

function LoginForm() {

  const [car, setCar] = useState({ id: 0, name: '', description: '', carTypeId: 0 });
  const [validated, setValidated] = useState(false);

  const handleSubmit = async (event) => {
    console.log(user)
    const form = event.currentTarget;
    event.preventDefault();

    if (form.checkValidity() === false) {
      event.stopPropagation();
    }

    setValidated(true)

    try {
      const response = await axios.post('http://localhost:61309/api/Authentication', user);
    }
    catch {
      window.alert("item niet aangemaakt!");
    }
  }

  return (
    <Form id="login" noValidate validated={validated} onSubmit={handleSubmit}>
      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="email" placeholder="Enter email" />
        <Form.Text className="text-muted">
          We'll never share your email with anyone else.
        </Form.Text>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Password" />
      </Form.Group>

      <Button form="login" variant="primary" type="submit">
        Opslaan
      </Button>
    </Form>
  );
}

export default LoginForm;