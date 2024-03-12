group "Old Architecture Group 2" {
    og2_system = softwareSystem "Rekeningrijden Group 2" {
        og2_webapp = container "Rekeningrijden WebApp" "WebApp for drivers" "${WEB}" "Web Browser"
        og2_gateway = container "API Gateway"
        # Identity
        og2_user_db = container "UserDB" "User credentials and profiles" "${SQL}" "Database"
        og2_identity_provider = container "Identity Provider" "User registration and authentication" "Keycloak" {
            -> og2_user_db
        }
        # Car Service
        og2_car_db = container "CarDB" "Car data" "${SQL}" "Database"
        og2_car_service = container "Car Service" "" "${NET}"{
            -> og2_car_db
        }
        # Tracking Service
        og2_tracking_db = container "TrackingDB" "Tracking Data" "${SQL}" "Database"
        og2_tracking_service = container "Tracking Service" "" "${NET}"{
            -> og2_tracking_db
        }
        # Route Service
        og2_route_db = container "RouteDB" "Route Data" "${SQL}" "Database"
        og2_route_service = container "Route Service" "" "${NET}"{
            -> og2_route_db
        }
        # Cost Calculation Service
        og2_cost_db = container "CostDB" "Cost Data" "${SQL}" "Database"
        og2_cost_service = container "Cost Calculation Service" "" "${NET}"{
            -> og2_cost_db
        }
        # Invoice Service
        og2_invoice_db = container "InvoiceDB" "Invoice Data" "${SQL}" "Database"
        og2_invoice_service = container "Invoice Service" "" "${NET}"{
            -> og2_invoice_db
        }
        # Broker
        og2_broker = container "Message Broker" "" "RabbitMQ"
    }
}

user -> og2_webapp

og2_webapp -> og2_gateway "Uses"
og2_gateway -> og2_identity_provider "Uses"
og2_gateway -> og2_car_service "Uses"
og2_gateway -> og2_tracking_service "Uses"
og2_gateway -> og2_route_service "Uses"
og2_gateway -> og2_cost_service "Uses"
og2_gateway -> og2_invoice_service "Uses"
og2_identity_provider -> og2_broker "Uses"
og2_car_service -> og2_broker "Uses"
og2_tracking_service -> og2_broker "Uses"
og2_route_service -> og2_broker "Uses"
og2_cost_service -> og2_broker "Uses"
og2_invoice_service -> og2_broker "Uses"
