# GreenFlux
## Introduction
This document has the objective of explaining the solution for the code challenge of GreenFlux.

## Challenge
### Domain model
Group – has a unique identifier (cannot be changed), name (can be changed), capacity in Amps (can be changed) – value greater than zero. Group can contain multiple charge stations. 
Charge station – has a unique identifier (cannot be changed), name (can be changed), multiple connectors (at least one, but not more than 5). 
Connector – has numerical identifier unique per charge station with (possible range of values from 1 to 5), Max current in Amps (can be changed) – value greater than zero. 

### Functional requirements
1. Group/Charge Station/Connector can be created, updated and removed. 
2. If a Group is removed, all Charge Stations in the Group should be removed as well. 
3. Only one Charge Station can be added/removed to a Group in one call. 
4. The Charge Station can be only in one Group at the same time. 
The Charge Station cannot exist in the domain without Group. 
5. A Connector cannot exist in the domain without a Charge Station. 
6. The Max current in Amps of an existing Connector can be changed (updated). 
7. The capacity in Amps of a Group should always be great or equal to the sum of the Max current in Amps of the Connector of all Charge Stations in the Group. 
8. If the capacity in Amps of a Group is not enough when adding a new Connector, the API should reject the new Connector. 

### Technical requirements: 
1. Create RESTful ASP.NET Core API according to described requirements. 
2. Use any convenient database to store data. In-memory is also an option. 
3. Cover your code by necessary in your opinion unit and/or integration tests. 
4. Think about the performance of the solution. 
5. Nice to have Swagger. 
6. Use a local Git repository for your code. 
7. Provide a ready-to-run solution (Visual Studio or Visual Studio Code) via GitHub or any other public Git repository. 

### Non-technical requirement: 
Create a readme.md file. If your assignment requires some extra operations to run it (create a database for example) detailed instruction must be added. 
We will evaluate if your code 
1. Solve the problem in the most efficient way. 
2. Has no bugs. 
3. Is clean (not over-engineered). 
4. Testable (happy and unhappy flows are covered by tests). 
5. Is maintainable (what if we need to extend your solution in the future). 


## Solution
### Architecture
The following diagram shows the high level architecture of the solution.
![image](https://user-images.githubusercontent.com/2537105/151719651-7ac55c6f-15aa-4726-939e-a6c030332398.png)


### Project Anatomy
The solution is consisted of 9 projects. These projects will be explained in the following sections.

#### GreenFlux.API
This project, is the core project of the solution. In this project we use dependency injection for all the other projects we have in the solution. Also this project contains the controllers that receives the http requests from client side and returns the http responses.
Note: One of the improvements that could be done in this project is to separate controllers from the rest of core project in order to keep all the dependency injection libraries isolated from controller access and we give the access of only ServiceAbstraction project to controllers. For the sake of simplicity this improvement is not done in the solution.

#### GreenFlux.Data
This project is our Database Context where there are models and migrations. Microsoft SQL Server database is used in the project. For communicating with the database Entity Framework Code First is used. 

#### GreenFlux.DTO
This project simply contains all the Data Transform Objects that we send to client or receive  from client side.

#### GreenFlux.GlobalErrorHandling
This is the point where we catch all the handled and unhandled exceptions. This project helps to avoid try and catch structure everywhere in the code.

#### GreenFlux.Repository
This project contains the implementation of repository classes to access the database context for read and write data

#### GreenFlux.RepositoryAbstraction
This project contains the interface classes of repositories. Business logic does not have access to lower level projects but only to the project. This project has access to Repository Project and responsible to deliver the data to higher level.

#### GreenFlux.Service
All the logics of the solution is implemented in this project. It means that this project is responsible for validating DTOs and mapping models to DTOs and communicating with RepositoryAbstraction Project.

#### GreenFlux.ServiceAbstraction
This project is the access point of controllers and contains the interface classes of services.

#### GreenFlux.Test
This project contains unit and integration tests for the solution. For testing the project XUnit is used.





