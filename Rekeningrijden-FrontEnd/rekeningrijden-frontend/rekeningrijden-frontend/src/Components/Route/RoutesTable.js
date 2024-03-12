import Table from 'react-bootstrap/Table'
import Button from 'react-bootstrap/Button';
import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import axios from 'axios'
import Loading from '../Loading';

const RoutesTable = () => {
    /*const [routes, setRoutes] = useState([])
    const [render, setRender] = useState(<tr></tr>)
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();


    useEffect(() => {
        async function fetchdata(){
        setLoading(true);      
        //const response = await axios.get(`http://localhost:5099/getAllVehicleId's`)
        //setRoutes(response.data)
        setLoading(false);}
        fetchdata();
    }, [])

    useEffect(() => {
        setRender(routes.map(RenderRoute));
    }, [routes]);
    
    const RenderRoute = (route, index) => {
        return (
            <tr key={index}>
                <td>{route.priceTotal}</td> 
                <td>
                    <Button onClick={() => navigate("./" + route.id)}>Details</Button>                   
                </td>             
            </tr>
        )
    }
    return (loading ? <Loading /> :
        <Table className="table table-hover" striped bordered>
            <thead>
                <tr>
                    <th>Kosten</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {render}
            </tbody>
        </Table>
    )*/
    return "";
}

export default RoutesTable