import Table from 'react-bootstrap/Table'
import React, { useState, useEffect } from 'react'
import axios from 'axios'
import Button from 'react-bootstrap/Button';
import Loading from '../Loading';

const CarTable = () => {
    const [cars, setCars] = useState([])
    const [render, setRender] = useState(<tr></tr>)
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchdata(){
        setLoading(true);      
        const response = await axios.get(`http://localhost:3306/api/Car`)
        setCars(response.data)
        setLoading(false);}
        fetchdata();
    }, [])

    useEffect(() => {
        setRender(cars.map(RenderCar));
    }, [cars]);

    const deleteCar = async (e) => {
        await axios.delete(`http://localhost:3306/api/Car?id=` + e.target.value)
        setCars(cars.filter(car => car.id != e.target.value))
    }
    
    const RenderCar = (car, index) => {
        return (
            <tr key={index}>
                <td>{car.name}</td>
                <td>{car.description}</td>
                <td>{car.carType.name}</td>
                <td>
                    <Button value={car.id} name={index} variant="danger" onClick={deleteCar}>Delete</Button>
                </td>
            </tr>
        )
    }
    return (loading ? <Loading /> :
        <Table className="table table-hover" striped bordered>
            <thead>
                <tr>
                    <th>Auto</th>
                    <th>Auto Beschrijving</th>
                    <th>Auto Type</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {render}
            </tbody>
        </Table>
    )
}

export default CarTable