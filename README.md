# Dance School - Member Management System
### ASP.NET Core, Entity Framework Core, SQL Server, React.js, SPA, Azure, Redis

An ASP.NET core web application designed for a small business that runs a dance school. <br />
It is an admin web portal for the business owner to view a dashboard and conduct CRUD operations. <br />
The users roles include the dance school owner (admin), instructors and students. <br />
The portals for instructors and registered students are coming soon. <br />

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
## Demo:   (To be updated)
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
| Entity Framework Core | Code-first object relational mapping for Code-first object relational mapping |
| Swagger API Documentation | Documented the API and multiple error response codes |
| OOP and DI | Abstracted duplicated codes into middlewares |
| React.js Hooks| Seperate reusable view components from hooks to ensure testability |
| SASS | Moduarlized and extendable style sheets management for for efficient UI styling update |

<a name="Security"></a>
### Security and Robustness
| Technology | Description |
| ----------- | ----------- |
| Firebase Auth | Authentication and role-based authorization implemented with middlewares to filter unauthenticated requests |
| Error handlings and Validations | Customised error response codes for business logics and fast client-side Formik validations |
| SQL Stored Procedures | Prevent SQL injection |
| Structured Logging | Serilog captures errors to enhance auditing and debugging |

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
Back End: ASP.NET Core 5.0, Entity Framework Core, SQL Server, Linq, Redis, Firebase Authentication, Azure Deployment, Serilog <br />

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
