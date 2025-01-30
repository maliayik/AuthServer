<h2>Overview</h2>
<p>This project demonstrates a <strong>JWT-based authentication and authorization</strong> system using <strong>role-based, claim-based, and policy-based authentication</strong>. The authentication server (<strong>AuthServer.API</strong>) issues JWT tokens, while the mini applications (<strong>MiniApp1.API, MiniApp2.API, MiniApp3.API</strong>) consume these tokens for secure access.</p>

<h2>Project Structure</h2>
<pre>

<h2>Features</h2>
<ul>
    <li><strong>JWT Token Issuance:</strong> AuthServer.API generates JWT tokens.</li>
    <li><strong>Role-Based Authorization:</strong> Users are granted access based on assigned roles.</li>
    <li><strong>Claim-Based Authorization:</strong> Users can be authorized based on specific claims.</li>
    <li><strong>Policy-Based Authorization:</strong> Custom authorization policies are applied.</li>
    <li><strong>Token-Based Secure API Communication:</strong> The mini applications validate JWT tokens for secure requests.</li>
</ul>

<h2>Authentication & Authorization Flow</h2>
<ol>
    <li><strong>User Login</strong> → AuthServer.API authenticates user credentials and issues a JWT token.</li>
    <li><strong>Token Consumption</strong> → MiniApp APIs consume the token in requests and validate it.</li>
    <li><strong>Authorization Handling</strong> → Based on roles, claims, and policies, access is granted or denied.</li>
</ol>

<h2>Setup & Configuration</h2>
<h3>1. Clone the Repository</h3>
<pre>
git clone <repository-url>
cd <repository-folder>

<h3>2. Configure AppSettings</h3>
<pre>
"ConnectionStrings" => for database connection

Configure your Token options
"TokenOptions": {
"Issuer": "AuthServer",
"Audience": "MiniApps",
"SecretKey": "YourSuperSecretKey"
}
Configure your Clients authentication
"Clients"{
"Id": "Example",
"Secret": "secret",
"Audiences": [ "www.miniapi3.com" ]
}
<h3>3. Run the Authentication Server</h3>
<pre>
cd AuthServer.API
dotnet run

<h2>API Endpoints</h2>
<ul>
    <li><strong>api/auth/CreateToken</strong> → Generates JWT token.</li>
    <li><strong>api/user</strong> → Creates a new user.</li>
    <li><strong>api/stock</strong> → Used for claim-based authorization testing.</li>
</ul>

<h2>Authentication & Authorization Flow</h2>
<ol>
    <li><strong>User Login</strong> → AuthServer.API authenticates user credentials and issues a JWT token.</li>
    <li><strong>Token Consumption</strong> → MiniApp APIs consume the token in requests and validate it.</li>
    <li><strong>Authorization Handling</strong> → Based on roles, claims, and policies, access is granted or denied.</li>
</ol>

<h2>Conclusion</h2>
<p>This project provides a <strong>modular JWT authentication system</strong> with different <strong>authorization strategies</strong>. The separation of <strong>authentication (AuthServer)</strong> and <strong>service consumers (MiniApps)</strong> ensures scalability and security.</p>

<hr>
<h2>Contributions & Issues</h2>
<p>If you find any issues or want to contribute, feel free to open a pull request or issue.</p>
<p>Happy coding! 🚀</p>
