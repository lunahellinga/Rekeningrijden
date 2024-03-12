import React, { useRef, useEffect, useState, Component } from 'react';
import mapboxgl from '!mapbox-gl'; // eslint-disable-line import/no-webpack-loader-syntax

mapboxgl.accessToken = 'pk.eyJ1IjoiZHJlYW1yZWFsbSIsImEiOiJjbGlhNnQzejMwMHFnM2VvOWxzam9kMWxhIn0.MqrFR3euIIXySElD3-yc1g';

function RouteData(props) {
    const mapContainer = useRef(null);
    const map = useRef(null);
    // const [lng, setLng] = useState(-73.890557);
    // const [lat, setLat] = useState(40.7682044);
    const [lng, setLng] = useState(5.4623);
    const [lat, setLat] = useState(51.4231);
    const [zoom, setZoom] = useState(9);

    useEffect(() => {
        var data = props.geoData;
        var data = JSON.parse(data);
        console.log(data);
        const map = new mapboxgl.Map({
            container: mapContainer.current,
            style: 'mapbox://styles/mapbox/streets-v12',
            center: data.features[0].geometry.coordinates[0],
            zoom: zoom
        });

        // only want to work with the map after it has fully loaded
        // if you try to add sources and layers before the map has loaded
        // things will not work properly
        map.on("load", () => {
            // data in the geojson format
            map.addSource('route', {
                'type': "geojson",
                'data': data,
            });
            
            //const geojsonSource = map.getSource('route');
            // Update the data after the GeoJSON source was created
            //geojsonSource.setData(data);
            // bus routes - line layer
            // see https://docs.mapbox.com/mapbox-gl-js/style-spec/layers/#line
            map.addLayer({
                id: "route",
                type: "line",
                source: "route",
                paint: {
                    "line-color": "#000000",
                    "line-width": 4,
                },
            })
        });

        return () => map.remove()
    }, []);

    return (
        <div>
            <div ref={mapContainer} style={{ width: "100%", height: "50vh" }} />
        </div>
    );
}
export default RouteData;