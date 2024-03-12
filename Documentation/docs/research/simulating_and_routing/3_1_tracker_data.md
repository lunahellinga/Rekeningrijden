## Tracker Data

_*\[Field]* What should the trackers send, and how?_

Since the trackers for the project are not yet designed, it is up to us to determine how they should
function. As we don't have specific expertise in designing integrated tracker devices, we have decided to have them send
REST requests to our API gateway for data transmission.

To ensure we capture the relevant data, we need to obtain the coordinates of the vehicle at regular intervals to
determine the roads used. We require the vehicle's ID, timestamp, and coordinates. After analysis, we have concluded
that receiving one coordinate every three seconds would provide us with sufficient data granularity to accurately
determine the roads used by the vehicle.

1. Road Network Precision: Most road networks are designed to accommodate a wide range of vehicle speeds. By capturing
   coordinates every three seconds, we can approximate the vehicle's position along the road network with reasonable
   accuracy. This interval allows us to capture the vehicle's location at multiple points along its route, enabling us
   to infer the roads used.

2. Traffic Dynamics: Traffic conditions and patterns can change rapidly, especially in urban areas. By capturing
   coordinates every three seconds, we can account for dynamic traffic scenarios, such as intersections, traffic
   signals, and congestion. This granularity helps in capturing significant changes in the vehicle's route caused by
   real-time traffic conditions.

3. Route Detection: Determining the precise route taken by a vehicle involves analyzing its sequence of coordinates.
   With a three-second interval, we can identify the general path and sequence of roads used by the vehicle. It allows
   us to connect the dots and infer the route based on the captured coordinate data.

4. Computational Efficiency: While capturing coordinates more frequently might provide even greater precision, it comes
   at the cost of increased computational complexity and data processing requirements. Balancing the need for accuracy
   with computational efficiency, a three-second interval strikes a practical balance for most road pricing simulations.

However, to reduce the load on our API and optimize the data transmission process, we have decided that batching the
coordinates would be a good idea. Instead of sending each coordinate individually, the trackers will record a coordinate
every three seconds but transmit a batch of coordinates every 15 seconds. This approach allows us to make more efficient
use of network resources while still providing the necessary data for route calculation.

```json
{
  "vehicleId": "b344e30e-6a32-4c2a-b2db-beae7f97142d",
  "coordinates": [
    {
      "lat": 5.1756587,
      "lon": 51.5365524,
      "time": "2023-07-21T17:32:28Z"
    },
     {
      "lat": 5.1756801,
      "lon": 51.5367265,
      "time": "2023-07-21T17:32:31Z"
    }
  ]
}
```

In addition to capturing coordinate data, we need a mechanism to determine when a vehicle has completed its journey so
that we can process its route as soon as possible. To address this requirement, we have decided to incorporate a
separate "in progress" message sent by the trackers at regular intervals, typically every few minutes. These "in
progress" messages will be used to track which vehicles are currently driving. When a vehicle sends a "stopped" message
or if no message is received for an extended period, it will trigger the calculation of the vehicle's route.

```json
{
  "vehicleID": "string",
  "status": 0
}
```