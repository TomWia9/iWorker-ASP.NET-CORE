# iWorker - API
This repository conatins API for [iWorker](https://github.com/TomWia9/iWorker) - Web application for workers management.

## Description
This project is an **ASP.NET Core 3.1** implemented Web API for communication with [iWorker](https://github.com/TomWia9/iWorker) - Angular 9 application.

**iWorker - API** enables communication with the [frontend](https://github.com/TomWia9/iWorker) application consisting of sending and receiving data regarding statistics of work, messages between employer and employees, raports of work sent by employees etc.

It uses **Entity Framework Core** to communicate with a **Sql** database, which contains required data tables like:
* Users - where informations about users and their login data are stored 
* Messages - where messages send by employees are stored 
* Raports -  where reports from work done by employees are stored
* Plans - where plans of work created by employer are stored  


This API uses also basic authentication provided by **JwtBearer**.

## Installation
Make sure you have the **.NET Core 3.1 SDK** installed on your machine. Then do:  
>`git clone https://github.com/TomWia9/iWorker-ASP.NET-CORE.git`  
`cd iWorker-ASP.NET-CORE\IWorker\`  
`dotnet run`

## Configuration
This will need to be perfored before running the application for the first time
1. You have to change ConnectionString in **appsettings.json** for ConnectionString that allow you to connect with database in your computer.
2. Issue the Entity Framework command to update the database  
`dotnet ef database update`
 
## License
[MIT](https://choosealicense.com/licenses/mit/)
