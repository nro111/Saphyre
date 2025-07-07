# Saphyre
CRUD application to demonstrate clean code and SOLID principles. This application utilizes a preview version of .Net 10 and is run with Docker. 

## How to run
Send me an email and I will provide the full .env file. Once you have that, you can just run the following command from the root folder:

docker-compose up --build

Keep in mind that the performance is going to be slow and you will see edits/deletes/inserts attempt to return to the 
UI before the actual db write operation finishes.

## Application architecture overview
This application utilizes a modular monolithic architecture with the following layers:

* Modules
  * User
    * Application
    * Contracts
    * DataAccess
    * Domain
    * Infrastructure  
* Shared
* Tests
  * Modules.Tests
  * Web.Tests
* Web
  * API
  * UI

## User Module
The user module utilizes the Application layer expose interfaces for the API, Domain, and DataAccess layers. 

To prevent potential circular dependencies, the Infrastructure layer was built to perform DI bootstrapping for the API Program file to reference during initialization. 

The Contracts layer contains DTOs that limit what is exposed to the frontend. 

The Domain layer contains the business logic and calls the DataAccess layer to perform db operations.

The DataAccess layer utilizes EFCore to perform CRUD operations for users. This layer also contains entities, repositories, and the db context. 

## Shared 
The API layer, DataAccess layer, and the Domain layer all reference the Shared layer for a customized return object. Initially, I just wanted to return the dto after every operation. Some methods returned a list, which made checking for a successful search easy without returning a null. On the other hand, returning a single user object would have had to be a null or an instance with default values since its properties are required. I decided that wrapping the dto would standardize returned data and make it so that there was no variance to account for later on. 

## Tests
This layer contains all the unit tests for the application. It utilizes xUnit with Moq.
