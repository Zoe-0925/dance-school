# Dance School
### ASP.NET Core, Entity Framework Core, SQL Server, React.js, SPA, Azure, Redis

An ASP.NET core web application designed for a small business that runs a dance school. <br />
It includes an admin web portal for the business owner to view a dashboard and conduct CRUD operations.<br />
It also includes a student protal for students to book classes and subscribe to memberships. <br />
The users roles include the dance school owner (admin) and students. <br />

## Table of Contents
1. [ API Documentation (Swagger) ](#API)
2. [ Demo ](#Demo) 
3. [ Feature Highlights ](#Feature)
4. [ Maintainability ](#Maintainability)
5. [ Security ](#Security)
6. [ Scalability ](#Scalability)
7. [ Tech Stack ](#Tech)
8. [ Design Patterns ](#Design)
9. [ How to run the program locally ](#Run)

<a name="API"></a>
## API Documentation (Swagger)

<a name="Demo"></a> 
## Admin Demo: https://danceschoool.herokuapp.com/admin   (No sign up required)
## Student Demo: https://danceschoool.herokuapp.com/student      (No sign up required)
![Demo Photo](https://github.com/Zoe-0925/DanceSchool/blob/master/danceschool/Client/public/Demo.png)
![Demo Photo](https://github.com/Zoe-0925/DanceSchool/blob/master/danceschool/Client/public/Demo-2.png)

<a name="Feature"></a>
## Feature Highlights
- An interactive admin dashboard with visualisations for data analytics
- Display data such as class information, instructors and memberships
- Allow the admin user full access on CURD operations.
- Allow the student role to view public data such as courses, membrships
- Allow the student role to book dance classes and subscribe to memberships

## Development Highlights
<a name="Maintainability"></a>
### Maintainability
| Technology | Description |
| ----------- | ----------- |
| Entity Framework Core | Code-first object relational mapping for easy database schema updates |
| Swagger API Documentation | Documented the API and multiple error response codes |
| OOP and DI | Abstracted duplicated codes into middlewares |
| React.js Hooks| Seperate reusable view components from hooks to ensure testability |
| SASS | Moduarlized and extendable style sheets management for efficient UI styling update |

<a name="Security"></a>
### Security and Robustness
| Technology | Description |
| ----------- | ----------- |
| Firebase Auth | Authentication and role-based authorization implemented with middlewares to filter unauthenticated requests |
| Error handlings and Validations | Customised error response codes for business logics and fast client-side Formik validations |
| SQL Stored Procedures | Prevent SQL injection |

<a name="Scalability"></a>
### Scalability and Performance
| Technology | Description |
| ----------- | ----------- |
| CQRS and Mediatr | Decouple the business logics from data access logics to meet business requirements flexiblely |
| In-memory Caching | The Redis Database is utilised to boost query performance |
| Pagination and Lazy loading | Fasten the loading time even with a huge amount of data |
| Complex front end state management | Keep small local states to minimise rerendering in React.js |
| Advanced SQL | Restrict necessary data fields returned from the server. Map "1-to-many" relationships. Minimise database oprations with batch queries. |
| Deployment | Azure SQL and Azure service app |
| CI/CD | Github Actions |

<a name="Tech"></a>
## Tech Stack:
Front End: React.js, Redux Thunk, SASS, Material-UI, Formik, Firebase Authentication <br />
Back End: ASP.NET Core 5.0, Entity Framework Core, SQL Server, Linq, Redis, Firebase Authentication, Azure Deployment <br />

<a name="Design"></a>
## Design Patterns:
- The Clean Architecture
- Asp.Net MVC
- CQRS
- Mediatr
- React Higher Order Components
- Redux
- Progressive Web Application with Redis Cache

<a name="Run"></a>
## How to run the program locally:
First, create a firebase project to acquire your firebase authentication API credentials from  <a href="https://firebase.google.com/">Firebase Console</a>.<br />
Then, set up your local SQL server and Redis to achieve connection strings.<br />
Alternatively, you can set up Azure SQL and Azure Redis Cache via <a href="https://azure.microsoft.com/en-au/features/azure-portal/">Azure Portal</a>.<br />
<br />
Replace connection strings in the "/danceschool/appsetting.json" and the "/danceschool/Context/ApplicationContext.cs" files before running the following commands. 
<br /><br />
Note that the server is running in the production mode. <br />
In order to run in the development mode, please replace the "production" keyword to "development" in the "daneschool/properties/launchsetting.json".
<br /><br />
Double click the `danceschool.sln` to open Visual Studio <br />
Open the terminal,
Change directory to the project root folder. <br />
Then run the following.  <br />

`cd danceschool` <br />
`dotnet restore`<br />
`cd Client` <br />
`npm install` <br /> 
`npm start` <br /> 
`cd ..` <br />
`dotnet run`. <br />

Then open [http://localhost:3000](http://localhost:3000) to view it in the browser.<br />
 <br />
To view the Swagger API documentation, <br />
Open [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)
