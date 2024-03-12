import React, { useRef, useEffect, useState, Component } from 'react';
import axios from 'axios'
import RouteData from './RouteData';

const RouteMap = (props) => {
    const [data, setGeoData] = useState();
    const [isLoading, setIsLoading] = useState(true);
    const [price, setPrice] = useState();
    const [selectedItem, setSelectedItem] = useState('');
    const [items, setItems] = useState([]);

    useEffect(() => {
        // Simulating fetching items from a database
        const fetchItems = async () => {
            try {
                // Perform an asynchronous operation to fetch the items from the database
                // For example, using fetch or axios
                const response = await fetch('https://lts.oibss.nl/Routes/getRoutes');
                const data = await response.json();
                // Update the state with the retrieved items
                setItems(data);
            } catch (error) {
                console.error('Error fetching items:', error);
            }
        };

        fetchItems();
    }, []);

    async function getData(idtest) {
        setIsLoading(true);
        let data = "";
        await axios.get('https://lts.oibss.nl/Routes/getRoute', { params: { id: idtest } })
            .then(resp => {
                data = resp.data;
            });;
        let geojson = {
            features: [{
                type: "Feature",
                properties: {},
                geometry: {
                    coordinates: [],
                    type: "LineString",
                },
            }],
            "type": "FeatureCollection"
        }

        setPrice(data.priceTotal);

        for (let i = 0; i < data.segments.length; i++) {
            geojson.features[0].geometry.coordinates.push([
                data.segments[i].start.lon,
                data.segments[i].start.lat
            ]);
        }
        let geo = JSON.stringify(geojson, null, 2);
        setGeoData(geo);
        setIsLoading(false);
    }

    /*useEffect(() => {
        if(props.route !== ''){
            getData();
        }
    }, [props])*/

    const shoot = (idtest) => {
        getData(idtest);
    }

    const handleSelectionChange = (selectedValue) => {
        setSelectedItem(selectedValue);
        console.log(selectedValue);
        getData(selectedValue);
    };
        

    return (
        <div>
            <h3>Kaart:</h3>
            
            {isLoading ?
            (
                <h1>Price = 0</h1>
            ) :
            (
                <h1>Price = {price}</h1>
            )}
            {isLoading ?
            (
                "Loading..."
            ) :
            (
                <RouteData geoData={data} />
            )}
            <select value={selectedItem} onChange={(e) => handleSelectionChange(e.target.value)}>
                <option value="">Select an item</option>
                {items.map((item, index) => (
                    <option value={item.id} key={item.id}>{item.id}</option>
                ))}
            </select>
            
        </div>
    );
}

export default RouteMap;