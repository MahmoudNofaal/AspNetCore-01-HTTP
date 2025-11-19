# HTTP Protocol Reference Guide

## Table of Contents
1. [Protocol Fundamentals](#protocol-fundamentals)
2. [Request-Response Cycle](#request-response-cycle)
3. [HTTP Methods](#http-methods)
4. [Status Codes](#status-codes)
5. [Headers](#headers)
6. [ASP.NET Core Implementation](#aspnet-core-implementation)
7. [Testing Tools](#testing-tools)

---

## Protocol Fundamentals

### What is HTTP?
HTTP (Hypertext Transfer Protocol) is the foundation of data communication on the web. It defines how messages are formatted and transmitted between clients and servers.

**Key Characteristics:**
- **Stateless:** Each request is independent; the server doesn't retain information between requests
- **Client-Server Model:** Clients initiate requests, servers respond with resources or errors
- **Text-Based:** Human-readable message format
- **Application Layer:** Operates on top of TCP/IP

### How Browsers Use HTTP
When you enter a URL in your browser:
1. Browser parses the URL
2. DNS resolves the domain to an IP address
3. Browser sends an HTTP request to the server
4. Server processes the request and sends a response
5. Browser renders the content (HTML, CSS, JavaScript, images)

---

## Request-Response Cycle

### HTTP Request Structure
```
[Method] [URI] [HTTP Version]
[Header]: [Value]
[Header]: [Value]

[Optional Body]
```

**Example:**
```http
GET /api/products?category=electronics HTTP/1.1
Host: example.com
User-Agent: Mozilla/5.0
Accept: application/json
```

**Components:**
- **Start Line:** Method, path, and HTTP version
- **Headers:** Metadata about the request (host, user agent, content type, etc.)
- **Body:** Data being sent (only for POST, PUT, PATCH)

### HTTP Response Structure
```
[HTTP Version] [Status Code] [Status Message]
[Header]: [Value]
[Header]: [Value]

[Optional Body]
```

**Example:**
```http
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 85
Date: Thu, 20 Nov 2025 10:30:00 GMT

{
  "id": 123,
  "name": "Laptop",
  "price": 999.99
}
```

---

## HTTP Methods

### GET
**Purpose:** Retrieve data from the server

**Characteristics:**
- Data passed via query strings in the URL
- Idempotent (multiple identical requests have the same effect)
- Cacheable
- Limited data size (URL length restrictions)
- Visible in browser history and logs
- Should not be used for sensitive data

**Example:**
```http
GET /products?category=electronics&brand=apple HTTP/1.1
Host: example.com
```

### POST
**Purpose:** Submit data to the server

**Characteristics:**
- Data passed in the request body
- Not idempotent (multiple requests may create multiple resources)
- Not cacheable by default
- No size limitations
- Suitable for sensitive data
- Creates new resources

**Example:**
```http
POST /api/users HTTP/1.1
Host: example.com
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com"
}
```

### PUT
**Purpose:** Update or replace an existing resource

**Characteristics:**
- Idempotent
- Replaces the entire resource
- Data in request body

### PATCH
**Purpose:** Partially update a resource

**Characteristics:**
- Not necessarily idempotent
- Modifies specific fields
- More efficient than PUT for small changes

### DELETE
**Purpose:** Remove a resource

**Characteristics:**
- Idempotent
- May or may not have a body

### Other Methods
- **HEAD:** Same as GET but returns only headers (no body)
- **OPTIONS:** Returns supported HTTP methods for a resource
- **TRACE:** Diagnostic method for debugging
- **CONNECT:** Establishes a tunnel (used for HTTPS proxying)

---

## Status Codes

### 1xx — Informational
Indicates that the request was received and is being processed.

| Code | Status              | Description                                                    |
|------|---------------------|----------------------------------------------------------------|
| 100  | Continue            | Server received headers; client can proceed with body          |
| 101  | Switching Protocols | Server agrees to switch protocols (e.g., HTTP to WebSocket)   |
| 102  | Processing          | Server is processing but no response available yet             |

### 2xx — Success
The request was successfully received, understood, and accepted.

| Code | Status                        | Description                                              |
|------|-------------------------------|----------------------------------------------------------|
| 200  | OK                            | Request succeeded                                        |
| 201  | Created                       | Request succeeded and a new resource was created         |
| 202  | Accepted                      | Request accepted for processing but not yet completed    |
| 203  | Non-Authoritative Information | Response from proxy, not origin server                   |
| 204  | No Content                    | Success, but no content to return                        |
| 205  | Reset Content                 | Client should reset the document view                    |
| 206  | Partial Content               | Partial resource delivered (range request)               |

### 3xx — Redirection
Further action is needed to complete the request.

| Code | Status             | Description                                                 |
|------|--------------------|-------------------------------------------------------------|
| 300  | Multiple Choices   | Multiple options for the resource                           |
| 301  | Moved Permanently  | Resource permanently moved to new URI                       |
| 302  | Found              | Resource temporarily at different URI                       |
| 303  | See Other          | Response found under another URI (use GET)                  |
| 304  | Not Modified       | Cached version is still valid                               |
| 307  | Temporary Redirect | Temporary redirect; method must not change                  |
| 308  | Permanent Redirect | Permanent redirect; method must not change                  |

### 4xx — Client Errors
The request contains bad syntax or cannot be fulfilled.

| Code | Status                        | Description                                          |
|------|-------------------------------|------------------------------------------------------|
| 400  | Bad Request                   | Invalid syntax                                       |
| 401  | Unauthorized                  | Authentication required                              |
| 403  | Forbidden                     | Server refuses to authorize                          |
| 404  | Not Found                     | Resource doesn't exist                               |
| 405  | Method Not Allowed            | HTTP method not supported for this resource          |
| 408  | Request Timeout               | Server timed out waiting for request                 |
| 409  | Conflict                      | Request conflicts with server state                  |
| 410  | Gone                          | Resource permanently deleted                         |
| 413  | Payload Too Large             | Request body too large                               |
| 414  | URI Too Long                  | URI exceeds server limits                            |
| 415  | Unsupported Media Type        | Media type not supported                             |
| 418  | I'm a teapot                  | April Fools' joke status (RFC 2324)                  |
| 422  | Unprocessable Entity          | Syntax correct but semantic errors                   |
| 429  | Too Many Requests             | Rate limit exceeded                                  |

### 5xx — Server Errors
The server failed to fulfill a valid request.

| Code | Status                          | Description                                       |
|------|---------------------------------|---------------------------------------------------|
| 500  | Internal Server Error           | Generic server error                              |
| 501  | Not Implemented                 | Server doesn't support requested functionality    |
| 502  | Bad Gateway                     | Invalid response from upstream server             |
| 503  | Service Unavailable             | Server temporarily unavailable                    |
| 504  | Gateway Timeout                 | Upstream server timeout                           |
| 505  | HTTP Version Not Supported      | HTTP version not supported                        |
| 507  | Insufficient Storage            | Server out of storage                             |
| 511  | Network Authentication Required | Client must authenticate for network access       |

---

## Headers

### Common Request Headers
- **Host:** Domain name of the server (required in HTTP/1.1)
- **User-Agent:** Client application information
- **Accept:** Media types client can process
- **Accept-Language:** Preferred languages
- **Accept-Encoding:** Supported compression formats
- **Authorization:** Authentication credentials
- **Content-Type:** Media type of request body
- **Content-Length:** Size of request body in bytes
- **Cookie:** Stored cookies for the domain

### Common Response Headers
- **Content-Type:** Media type of response body
- **Content-Length:** Size of response body
- **Content-Encoding:** Compression format used
- **Server:** Server software information
- **Date:** Response timestamp
- **Set-Cookie:** Cookie to be stored by client
- **Cache-Control:** Caching directives
- **Location:** Redirect URL (for 3xx responses)
- **ETag:** Resource version identifier
- **Access-Control-Allow-Origin:** CORS policy

---

## ASP.NET Core Implementation

### HTTP Servers in .NET

**Kestrel** is the default cross-platform web server for ASP.NET Core:
- Lightweight and high-performance
- Can run standalone or behind reverse proxies (IIS, Nginx, Apache)
- Supports HTTP/1.x, HTTP/2, and HTTP/3
- Built-in support for HTTPS

### Request Processing Flow
1. Client sends HTTP request
2. Kestrel receives and parses request
3. Middleware pipeline processes request
4. Application code generates response
5. Kestrel sends response to client

### Working with Responses

#### Setting Status Codes and Headers
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    // Set custom headers
    context.Response.Headers["Content-Type"] = "application/json";
    context.Response.Headers["X-Custom-Header"] = "MyValue";
    
    // Set status code
    context.Response.StatusCode = 200;
    
    await context.Response.WriteAsync("{\"message\": \"Success\"}");
});

app.Run();
```

#### Conditional Status Codes
```csharp
app.Run(async (HttpContext context) =>
{
    if (context.Request.Path == "/success")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("OK");
    }
    else if (context.Request.Path == "/notfound")
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Not Found");
    }
    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Bad Request");
    }
});
```

### Working with Requests

#### Accessing Request Properties
```csharp
app.Run(async (HttpContext context) =>
{
    string method = context.Request.Method;      // GET, POST, etc.
    string path = context.Request.Path;          // /api/products
    string queryString = context.Request.QueryString.Value;  // ?id=123
    
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync($"<p>Method: {method}</p>");
    await context.Response.WriteAsync($"<p>Path: {path}</p>");
    await context.Response.WriteAsync($"<p>Query: {queryString}</p>");
});
```

#### Reading Query Parameters
```csharp
app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            
            // Multiple values
            var categories = context.Request.Query["category"];
            
            await context.Response.WriteAsync($"ID: {id}");
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Missing 'id' parameter");
        }
    }
});
```

#### Reading Headers
```csharp
app.Run(async (HttpContext context) =>
{
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        string userAgent = context.Request.Headers["User-Agent"];
        await context.Response.WriteAsync($"User-Agent: {userAgent}");
    }
    
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        string auth = context.Request.Headers["Authorization"];
        // Process authentication
    }
});
```

#### Reading Request Body
```csharp
app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "POST")
    {
        using var reader = new StreamReader(context.Request.Body);
        string body = await reader.ReadToEndAsync();
        
        // Process body (parse JSON, form data, etc.)
        await context.Response.WriteAsync($"Received: {body}");
    }
});
```

---

## Testing Tools

### Postman
A comprehensive API development and testing platform.

**Installation:**
1. Download from [postman.com/downloads](https://www.postman.com/downloads/)
2. Install for Windows, macOS, or Linux
3. Create a free account (optional but recommended)

**Basic Usage:**
1. Create a new request
2. Select HTTP method (GET, POST, etc.)
3. Enter URL (e.g., `https://localhost:7070/api/products`)
4. Add headers (if needed)
5. Add body data for POST/PUT requests
6. Click **Send**
7. View response: status code, headers, body, time

**Advanced Features:**
- Collections: Group related requests
- Environment variables: Reuse values across requests
- Tests: Automated response validation
- Mock servers: Simulate API responses
- Documentation: Generate API documentation

### Browser Developer Tools
Built-in tools for inspecting HTTP traffic.

**Chrome DevTools (F12):**
1. Open DevTools (F12 or right-click → Inspect)
2. Go to **Network** tab
3. Reload page or trigger requests
4. Click any request to see:
   - Headers (request and response)
   - Response body
   - Timing information
   - Cookies

**Useful Features:**
- Filter by type (XHR, JS, CSS, Images)
- Preserve log across page loads
- Disable cache for testing
- Throttle network speed
- Copy requests as cURL commands

### cURL
Command-line tool for making HTTP requests.

**Examples:**
```bash
# GET request
curl https://api.example.com/products

# POST request with JSON
curl -X POST https://api.example.com/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Laptop","price":999}'

# Include headers in output
curl -i https://api.example.com/products

# Follow redirects
curl -L https://example.com
```

---

## Best Practices

### API Design
- Use appropriate HTTP methods for operations
- Return meaningful status codes
- Include descriptive error messages
- Version your APIs (e.g., `/api/v1/products`)
- Use HTTPS for production
- Implement rate limiting
- Document your APIs

### Security
- Validate all input
- Use HTTPS for sensitive data
- Implement authentication and authorization
- Set security headers (CORS, CSP, X-Frame-Options)
- Don't expose sensitive information in error messages
- Use CSRF protection for state-changing operations

### Performance
- Enable compression (gzip, brotli)
- Use caching headers appropriately
- Implement pagination for large datasets
- Use HTTP/2 or HTTP/3 when possible
- Minimize payload sizes
- Use CDNs for static resources

### Error Handling
- Return appropriate status codes
- Provide helpful error messages
- Use consistent error response format
- Log errors server-side
- Don't expose stack traces in production

---

## Quick Reference

### Method Selection
- **GET:** Retrieve data
- **POST:** Create new resource
- **PUT:** Replace entire resource
- **PATCH:** Update part of resource
- **DELETE:** Remove resource

### Common Status Codes
- **200:** Success
- **201:** Created
- **204:** No content
- **400:** Bad request
- **401:** Unauthorized
- **403:** Forbidden
- **404:** Not found
- **500:** Server error

### When to Use Query Strings vs. Body
- **Query strings:** Filter, sort, search parameters (GET)
- **Body:** Resource data, large payloads (POST, PUT, PATCH)