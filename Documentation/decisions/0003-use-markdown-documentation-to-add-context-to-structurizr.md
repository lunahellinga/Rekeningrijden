# 3. Use Markdown documentation to add context to Structurizr

Date: 2023-03-16

## Status

Accepted

## Context

Structurizr graphs do a good job of showing architecture and deployment but lack the specifics of what parts of the architecture do.
Splitting the project's documentation so this information is somewhere else makes finding it difficult.

## Decision

Components of the system will be described in markdown documentation with the following structure:

1. context.md : Describes the context the component lives in via a general description and its interactions with systems outside the project.
2. functional-overview.md : Describes the functionality of the component.
3. quality-attributes.md : Describes the quality attributes of the component.

## Consequences

Having to write this documentation takes time but creates an easy to understand system landscape.
