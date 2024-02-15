# CarRentalApi

## Summary
Simple yet powerful Web API and improved version of my old Api, providing controllers with endpoints to perform CRUD operations on __Vehicles__ and __Rentals__ data tables. Equiped with all recommended patterns and flows to improve stability and maitanance of application this includes Clean Architecture, FluentValidation, CQRS, Mediator Validation Behaviour and more.

## Data
TPC strategy was used to distinguish vehicles and make them inherit common properties from base abstract Vehicle class

## Features
* Authentication using Jwt Token and Server-Side Authorization
* Entity Framework Core with Unit of Work and repository patterns
* User identity with roles and lockout features <!-- /PasswordChange/EmailChange) -->
* Robust validation to ensure new rentals do not conflict with existing ones
* User seeding with assigned roles
* CQRS
* Exception Middleware
* RabbitMQ Messaging Support
* Unit Tests
* Integration Tests
* Documentation
