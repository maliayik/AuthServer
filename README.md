<h1>AuthServer & MiniApps - JWT Authentication & Authorization</h1>

<h2>Overview</h2>

This project demonstrates a JWT-based authentication and authorization system using role-based, claim-based, and policy-based authentication. The authentication server (AuthServer.API) issues JWT tokens, while the mini applications (MiniApp1.API, MiniApp2.API, MiniApp3.API) consume these tokens for secure access.


<h2>Project Structure</h2>

AuthServer.API        --> Handles authentication and token issuance
AuthServer.Core       --> Contains policy-based authorization implementation
AuthServer.Data       --> Database and data layer for user authentication
AuthServer.Service    --> Business logic layer for authentication services
MiniApp1.API         --> Example microservice using JWT authentication
MiniApp2.API         --> Example microservice using JWT authentication
MiniApp3.API         --> Example microservice using JWT authentication
SharedLibrary        --> Common utilities and helper functions


Features

- JWT Token Issuance: AuthServer.API generates JWT tokens.

- Role-Based Authorization: Users are granted access based on assigned roles.

- Claim-Based Authorization: Users can be authorized based on specific claims.

- Policy-Based Authorization: Custom authorization policies are applied.

- Token-Based Secure API Communication: The mini applications validate JWT tokens for secure requests.

 <h2>Authentication & Authorization Flow</h2> 

1. User Login → AuthServer.API authenticates user credentials and issues a JWT token.

2. Token Consumption → MiniApp APIs consume the token in requests and validate it.

3. Authorization Handling → Based on roles, claims, and policies, access is granted or denied.

<h2>Setup & Configuration</h2>

1. Clone the Repository

git clone repository-url cd repository-folder

2.  Configure AppSettings

{
  "ConnectionStrings": "Database connection string",
  "TokenOptions": {
    "Issuer": "AuthServer",
    "Audience": "MiniApps",
    "SecretKey": "YourSuperSecretKey"
  },
  "Clients": {
    "Id": "Example",
    "Secret": "secret",
    "Audiences": [ "www.miniapi3.com" ]
  }
}

3. Run the Authentication Server

cd AuthServer.API
dotnet run

<h2>API Endpoints</h2>

api/auth/CreateToken → Generates JWT token.

api/user →Creates a new user.

api/stock → Used for claim-based authorization testing.



<h2>Conclusion</h2>

This project provides a modular JWT authentication system with different authorization strategies. The separation of authentication (AuthServer) and service consumers (MiniApps) ensures scalability and security.



Contributions & Issues
If you find any issues or want to contribute, feel free to open a pull request or issue.

Happy coding! 🚀



















