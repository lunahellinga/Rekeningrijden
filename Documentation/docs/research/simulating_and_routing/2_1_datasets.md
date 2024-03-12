## Datasets

_*\[Library]* What datasets are available for generating routes?_

To simulate traffic and generate routes for the project, we require relevant datasets that provide information about road networks their details. We looked into the following datasets:

1. OpenStreetMap (OSM):
    - Upsides: OSM is a widely used crowd-sourced mapping platform that provides comprehensive road network data for
      many European countries. It offers a vast and continuously updated dataset that includes road geometries,
      classifications (e.g., motorways, highways, residential streets), and additional attributes (e.g., speed limits,
      one-way streets). OSM data is free, openly licensed, and accessible for download in various formats.
    - Downsides: While OSM provides extensive coverage, the accuracy and completeness of the data can vary depending on
      the region and the level of community contributions. Some rural or less-populated areas might have less detailed
      road information. Additionally, the quality of attribute data (e.g., speed limits) may be inconsistent, requiring
      careful validation. [OpenStreetMap. (n.d.).]
   
2. European Data Portal (EDP):
    - Upsides: The European Data Portal aggregates open data from various European countries, including geospatial
      datasets. It offers a centralized platform to access data provided by national governments, regional authorities,
      and public institutions. These datasets may include road networks and related attributes for specific countries.
    - Downsides: The availability and quality of geospatial datasets can vary across different European countries within
      the EDP. Some datasets might have limited coverage or be available only at a regional or local level.
      Additionally, the licensing terms and data formats can differ between countries and datasets. [Data.Europa.Eu. (n.d.).]
   
3. National Mapping Agencies and Government Sources:
    - Upsides: Many European countries have their national mapping agencies or government bodies responsible for
      maintaining authoritative geospatial datasets. These datasets often provide highly accurate and detailed road
      network information, including attributes like road types, traffic regulations, and administrative boundaries.
      They are typically reliable and trusted sources of geospatial data.
    - Downsides: Access to national mapping agency datasets might require specific permissions or agreements, and some
      datasets may have associated costs. Additionally, different countries may have varying levels of openness and
      accessibility to their geospatial data. Data formats and standards can also differ between countries, requiring
      additional effort for data integration and harmonization.
   
4. Commercial Geospatial Data Providers:
    - Upsides: Several commercial providers offer premium geospatial datasets that include detailed road networks for
      European countries. These datasets often provide high-quality data, regularly updated road information, and
      additional attributes for advanced analysis and simulation purposes. They may also offer dedicated APIs or tools
      for data integration and processing.
    - Downsides: Commercial datasets typically come with associated costs, which may vary based on the data coverage,
      resolution, and licensing terms. Depending on the provider, there might be restrictions on data redistribution or
      limitations on the usage and access rights.