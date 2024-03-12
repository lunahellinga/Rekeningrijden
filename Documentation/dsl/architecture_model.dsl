# Systems

trackers = softwareSystem "Simulated Tracker"
international_services = softwareSystem "International Services"

rekeningrijden = softwareSystem "Rekeningrijden" {
    dashboard  = container "Rekeningrijden Web" "Primary webapp for users" "${WEB}" "Web Browser"
    broker = container "RabbitMQ Broker" "Message bus" "RabbitMQ" "Broker"

    
    
    car_db = container "Car DB" "Stores cleaned RDW vehicle data" "${SQL2}" "Database"
    car_service = container "Car Service" "Provides vehicle information to other services" "${PYTHON}"{
        -> car_db
    }

    coordinate_db = container "Coordinate DB" "Stores raw coordinate data for unfinished routes" "${NOSQL}" "NoSQL"
    coordinate_service = container "Coordinate Service" "Processes raw coordiantes and status messages" "${PYTHON}"{
        -> coordinate_db
    }

    data_db = container "Data DB" "Stores processed routes" "${NOSQL}" "NoSQL"
    data_service = container "Data Service" "Stores and provides completed routes" "${NET}"{
        -> data_db
    }
    
    payment_db = container "Payment DB" "Pricing data" "${SQL!}" "Database"
    payment_service = container "Payment Service" "Stores and provides pricing data" "${NET}"{
        -> payment_db
    }
    
    international_api = container "International API" "Provides other countries with route calculations within belgian borders" "${NET}"
    router_api = container "Router API" "Receives tracker data and puts it on the bus" "${NET}"
    router_service_be = container "Router Service Internal" "Calculates routes from raw coordinates for internal routes. Adds prices with vehicle and pricing data."
    router_service_nl = container "Router Service Netherlands" "Calculates routes from raw coordinates, as provided by the Netherlands. Adds prices with vehicle and pricing data."
    router_service_lu = container "Router Service Luxembourg" "Calculates routes from raw coordinates, as provided by Luxembourg. Adds prices with vehicle and pricing data."

}
# Top level
user -> dashboard "Uses"

# Gateway usage
dashboard -> data_service "Retrieves route data"
international_services -> international_api "Send route processing requests"
international_services -> international_api "Send completed routes"
trackers -> router_api "Sends coordinate batches and status updates"

router_api -> broker "Publish coordinate batches and statuses"
broker -> coordinate_service "Consume coordinate batches and statuses"
coordinate_service -> broker "Publish raw routes"
broker -> router_service_be "Consume raw routes"
router_service_be -> car_service "Get vehicle information"
router_service_be -> payment_service "Get pricing information"
router_service_be -> broker "Publish calculated routes"
broker -> data_service "Consume calculated routes"

international_api -> broker "Publish international routing requests"
broker -> router_service_lu "Consume requests from LU"
broker -> router_service_nl "Consume requests from NL"
router_service_lu -> payment_service "Get pricing information"
router_service_nl -> payment_service "Get pricing information"
router_service_lu -> broker "Publish calculated LU routes"
router_service_nl -> broker "Publish calculated NL routes"
broker -> international_api "Consume calculated international routes"

international_api -> broker "Publish calculated routes"
international_api -> international_services "Send calculated routes"