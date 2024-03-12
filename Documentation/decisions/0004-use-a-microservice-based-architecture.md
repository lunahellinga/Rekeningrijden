# 4. Use a microservice based architecture

Date: 2023-03-26

## Status

Accepted

## Context

We expect all cars in the netherlands to make use of the RDW system, with constant data streams and large spikes in traffic at certain times.\
In addition, Fontys requires microservices for this project.

## Decision

To ensure sufficient scalability we will use a microservice based architecture design.

## Consequences

Microservices allow us to ensure that all parts of the system can be scaled, as well as increase reliability by ensuring that interruptions in non critical services don't effect the critical ones.
It does complicate our design as we have to account for data consistency and design around the risks of microservices.