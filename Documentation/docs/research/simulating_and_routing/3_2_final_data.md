## Final Data

_*\[Field]* What format should the final data have?_

While we were in the process of designing the structure of the final data, specifically considering the requirements for route price calculations and user display, the client organized a meeting involving all teams working on different components of the project.

During the workshop, we proposed a solution based on OpenStreetMap's (OSM) `way` and `node` system, which is widely adopted and publicly available. We suggested that the final data should be structured in a format that leverages OSM's existing standards and conventions. Specifically, we recommended representing routes as segments, each consisting of a start node, way, and end node.

By adopting this approach, all teams working on different country-specific systems would have the flexibility to handle and generate the data in their preferred manner, while still adhering to a standardized format. This standardized format based on OSM would ensure compatibility and interoperability across the various implementations of road pricing systems.

Utilizing OSM's way and node system as the foundation for the final data format offers several advantages. First, it allows teams to leverage OSM's rich road network data, including attributes such as road types, classifications, and additional information which can be used for price calculations. Second, it provides a publicly available and widely recognized standard, enabling seamless integration with other mapping and geospatial services such as MapBox.

By adopting a standardized format based on OSM, teams can generate and process the final data according to their specific requirements while ensuring consistency and compatibility across different systems. This approach facilitates the exchange of data between teams working on road pricing systems for different countries, promoting collaboration and interoperability throughout the Rekeningrijden project and beyond.

After considering the requirements and discussions from the workshop, we proceeded to finalize the format of the end result data. To achieve this, we created OpenAPI specifications for the international API, incorporating the object types that were extensively discussed and agreed upon during the workshop.

```json
{
  "id": "b344e30e-6a32-4c2a-b2db-beae7f97142d",
  "priceTotal": 64.3,
  "segments": [
    {
      "time": "2023-07-21T17:32:28Z",
      "price": 64.3,
      "start": {
        "id": 0,
        "lat": 5.1756587,
        "lon": 51.5365524
      },
      "way": {
        "id": 0
      },
      "end": {
        "id": 0,
        "lat": 5.1756587,
        "lon": 51.5365524
      }
    }
  ]
}
```