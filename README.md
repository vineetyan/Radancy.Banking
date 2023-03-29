# Radancy Banking System

## Overview
Radancy Banking System is API for listing accounts, creating accounts, deleting accounts , withdrawing money and depositing money

## Tech Stack Used
Dot net 6.0
Visual Studio 2022
MSTest for unit test cases
Bogus library for faking objects
Fluent Assertions for easy assertions in test cases
Entity Framework

## Key Notes
1. These apis work as an admin by default. Because if it has to be exposed to end user, we would have to add authentication mechanism which is beyond te scope of this poc
2. To add an account, we must already have that user in the system
3. Right now there are two users in the in memory database of application. They have client ids as "1" and "2" respectively. Hence to create an account, input client id should either be "1" or "2"
4. First run get accounts api to understand what data we already have
5. Once the application is stopped, it will loose its state as we are using in memory database

## How to run
1. Clone the repository
2. Open solution in Visual Studio 2022
3. Restore packages
4. Build Solution
5. Run Radancy.BankingSystem project
6. It will open swagger UI by default. You ca try any api from swagger UI itself
![alt text](https://github.com/vineetyan/Radancy.Banking/blob/main/swagger.png)


## Scope For Improvements
1. Exception handling can be done with exception handling middleware. With this controllers can avoid try catch 
2. Test cases can be written for apis/controllers as well
3. The transaction validation rules can be segregated in a separate class or project
