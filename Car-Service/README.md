# Sources

The RDW vehicle list with appropriate columns selected can be
found [here](https://opendata.rdw.nl/Voertuigen/Open-Data-RDW-Gekentekende_voertuigen/m9d7-ebf2/explore/query/SELECT%0A%20%20%60kenteken%60%2C%0A%20%20%60europese_voertuigcategorie%60/page/filter)

The RDW vehicle fuel type list with appropriate columns selected can be
found [here](https://opendata.rdw.nl/Voertuigen/Open-Data-RDW-Gekentekende_voertuigen_brandstof/8ys7-d773/explore/query/SELECT%0A%20%20%60kenteken%60%2C%0A%20%20%60brandstof_omschrijving%60/page/filter)

Then combine those, download them as CSV and drop them in the PythonHelper folder.
Then run the following in that folder:

```shell
poetry shell
poetry run python pythonhelper/helper.py
```

# Build

```shell
docker build -t fontyssa/car-service:latest -f Dockerfile .
docker build -t fontyssa/postgres-extended:latest -f pg-Dockerfile .
```

```shell
docker push fontyssa/car-service:latest
```

```shell
docker push fontyssa/postgres-extended:latest
```