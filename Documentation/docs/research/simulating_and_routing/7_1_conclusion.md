## Conclusion

**How do we generate and process routes to simulate traffic for Rekeningrijden?**

1. Route Generation: We determined that OpenStreetMap (OSM) is a suitable dataset for generating routes due to its
   availability, comprehensive road information, and familiarity within our team. We utilized Python libraries such as
   osmnx, networkx, and geopandas to work with the OSM dataset effectively.

2. Tracker Data Transmission: As the trackers were not yet designed, we decided to have them send REST requests to our
   API gateway. We specified the necessary data, including vehicle coordinates, vehicle ID, and timestamps. To reduce
   API load, we implemented batching, where coordinates were recorded every three seconds but sent in batches every 15
   seconds. We also introduced "in progress" messages to track active vehicles.

3. Data Format: During discussions with other teams working on different country implementations, we proposed using
   OSM's node-way system to format the final data. This standardized format, based on segments with start and end nodes,
   allows flexibility for data handling and integration with mapping services like MapBox.

4. Transformation Process: To transform coordinate lists back into OSM routes, we employed osmnx and networkx. We
   matched each coordinate to the nearest edge, determined start and end nodes, filled any missing edges, and
   recalculated the shortest route. The resulting route was converted into the agreed-upon Segment(
   startNode, way, endNode) format.

5. Scalability: Initially, we aimed to divide the country into grid segments and utilize an orchestrator service.
   However, due to time and resource limitations, we adjusted our approach. We decided to run three RouterServices
   instead: one for the border region with the Netherlands, one for the border region with Luxembourg, and one for an
   internal region such as Brussels.