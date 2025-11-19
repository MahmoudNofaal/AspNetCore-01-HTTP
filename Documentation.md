# HTTP Protocol Notes

## Overview
HTTP (Hypertext Transfer Protocol) is a protocol used for transmitting hypertext (e.g., HTML) over the internet.  
It operates on a client-server model, where the client (usually a web browser) makes requests to a server, which responds with resources or error messages.  

**Stateless Protocol:** Each HTTP request is independent; the server does not retain information from previous requests.

---

## Request/Response Model
- **Client Request:** The client sends an HTTP request to the server.  
- **Server Response:** The server processes the request and sends back an HTTP response.

---

## HTTP Server
An HTTP server handles HTTP requests and serves responses. It processes requests, executes logic (e.g., database access), and returns a response.

**Examples:** Apache, Nginx, Microsoft IIS, Kestrel.

### Kestrel
- Cross-platform web server in ASP.NET Core.
- Lightweight, high-performance, suitable for internal and public apps.

---

## Request and Response Flow with Kestrel
1. Client sends an HTTP request.
2. Kestrel receives the request.
3. Middleware processes the request.
4. The application generates a response.
5. Kestrel sends the response back to the client.

---

## How Browsers Use HTTP
Browsers use HTTP to request resources (HTML, CSS, images, JS) from servers.  
When a URL is entered, the browser sends an HTTP request, and the server responds with the resource.

---

## HTTP Response Message Format
**Structure:**
```
Start Line: HTTP version, status code, message
Headers: Key-value pairs
Body: Optional (HTML, JSON, etc.)
```

**Example:**
```
HTTP/1.1 200 OK
Content-Type: text/html
Content-Length: 137

<html>
<body>
<h1>Hello, World!</h1>
</body>
</html>
```

**Common Headers:**
- `Content-Type`
- `Content-Length`
- `Server`
- `Set-Cookie`
- `Cache-Control`

---

## Default Response Headers in Kestrel
- `Content-Type`
- `Server`
- `Date`

---

## HTTP Status Codes

**Categories:**
- **1xx:** Informational  
- **2xx:** Success  
- **3xx:** Redirection  
- **4xx:** Client Error  
- **5xx:** Server Error  

**Common Codes:**

# 1xx — Informational
| Code | Status              | Description                                                                              |
| ---- | ------------------- | ---------------------------------------------------------------------------------------- |
| 100  | Continue            | The server has received the request headers and the client can proceed to send the body. |
| 101  | Switching Protocols | The server agrees to switch protocols as requested.                                      |
| 102  | Processing          | The server is processing the request but no response is available yet.                   |

# 2xx — Success
| Code | Status                        | Description                                                                      |
| ---- | ----------------------------- | -------------------------------------------------------------------------------- |
| 200  | OK                            | The request succeeded.                                                           |
| 201  | Created                       | The request succeeded and a new resource was created.                            |
| 202  | Accepted                      | The request has been accepted for processing but not completed.                  |
| 203  | Non-Authoritative Information | The response is from a proxy, not the original server.                           |
| 204  | No Content                    | The server successfully processed the request, but is not returning any content. |
| 205  | Reset Content                 | The client should reset the view or form.                                        |
| 206  | Partial Content               | The server is delivering only part of the resource (e.g., range requests).       |

# 3xx — Redirection
| Code | Status             | Description                                                           |
| ---- | ------------------ | --------------------------------------------------------------------- |
| 300  | Multiple Choices   | The requested resource has multiple options.                          |
| 301  | Moved Permanently  | The resource has been permanently moved to a new URI.                 |
| 302  | Found              | The resource is temporarily located under a different URI.            |
| 303  | See Other          | The response can be found under another URI.                          |
| 304  | Not Modified       | The resource has not been modified since the last request.            |
| 307  | Temporary Redirect | The resource is temporarily redirected; method must remain unchanged. |
| 308  | Permanent Redirect | The resource has been permanently redirected to another URI.          |

# 4xx — Client Errors
| Code | Status                        | Description                                                        |
| ---- | ----------------------------- | ------------------------------------------------------------------ |
| 400  | Bad Request                   | The server could not understand the request due to invalid syntax. |
| 401  | Unauthorized                  | Authentication is required.                                        |
| 402  | Payment Required              | Reserved for future use.                                           |
| 403  | Forbidden                     | The client does not have access rights to the content.             |
| 404  | Not Found                     | The server cannot find the requested resource.                     |
| 405  | Method Not Allowed            | The HTTP method is not allowed for this resource.                  |
| 406  | Not Acceptable                | The requested resource is not available in an acceptable format.   |
| 407  | Proxy Authentication Required | Authentication with a proxy is required.                           |
| 408  | Request Timeout               | The server timed out waiting for the request.                      |
| 409  | Conflict                      | The request conflicts with the current state of the server.        |
| 410  | Gone                          | The resource is permanently removed.                               |
| 411  | Length Required               | The request did not specify a valid content length.                |
| 412  | Precondition Failed           | The server does not meet one of the preconditions.                 |
| 413  | Payload Too Large             | The request body is too large.                                     |
| 414  | URI Too Long                  | The URI is too long for the server to process.                     |
| 415  | Unsupported Media Type        | The media type is not supported.                                   |
| 416  | Range Not Satisfiable         | The requested range cannot be served.                              |
| 417  | Expectation Failed            | The expectation cannot be met by the server.                       |
| 418  | I'm a teapot                  | Joke status code from RFC 2324.                                    |
| 422  | Unprocessable Entity          | The request was valid but contained semantic errors.               |
| 429  | Too Many Requests             | Too many requests were sent in a given time.                       |

# 5xx — Server Errors
| Code | Status                          | Description                                                                |
| ---- | ------------------------------- | -------------------------------------------------------------------------- |
| 500  | Internal Server Error           | The server encountered an unexpected condition.                            |
| 501  | Not Implemented                 | The server does not support the requested functionality.                   |
| 502  | Bad Gateway                     | The server received an invalid response from the upstream server.          |
| 503  | Service Unavailable             | The server is not ready to handle the request.                             |
| 504  | Gateway Timeout                 | The server did not receive a timely response from an upstream server.      |
| 505  | HTTP Version Not Supported      | The server does not support the HTTP version used.                         |
| 507  | Insufficient Storage            | The server cannot store the representation needed to complete the request. |
| 508  | Loop Detected                   | The server detected an infinite loop while processing the request.         |
| 511  | Network Authentication Required | The client must authenticate to gain network access.                       |


---

## Setting Status Codes and Response Headers in ASP.NET Core

### Example 1
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    context.Response.Headers["MyKey"] = "my value";
    context.Response.Headers["Server"] = "My server";
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<h1>Hello</h1>");
    await context.Response.WriteAsync("<h2>World</h2>");
});

app.Run();
```

### Example 2
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (1 == 1)
        context.Response.StatusCode = 200;
    else
        context.Response.StatusCode = 400;

    await context.Response.WriteAsync("Hello");
    await context.Response.WriteAsync(" World");
});

app.Run();
```

---

## HTTP Requests

An HTTP request is the client’s way of asking the server for a resource or action.

### Anatomy of an HTTP Request
- **Start Line:** Method, URI, HTTP version  
- **Headers:** Extra information (User-Agent, Accept, Host, etc.)  
- **Empty Line:** Separator  
- **Body (optional):** Data being sent (e.g., form data, JSON)

### Query Strings
Used to pass parameters in URLs:
```
https://example.com/products?category=electronics&brand=apple
```

---

## HttpRequest in ASP.NET Core
Provides access to request details:
- `Method`
- `Path`
- `Query`
- `Headers`
- `Body`

### Example 1
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    
    context.Response.Headers["Content-type"] = "text/html";
    await context.Response.WriteAsync($"<p>{path}</p>");
    await context.Response.WriteAsync($"<p>{method}</p>");
});

app.Run();
```

### Example 2
```csharp
app.Run(async (HttpContext context) =>
{
    context.Response.Headers["Content-type"] = "text/html";
    if (context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            string id = context.Request.Query["id"];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }
    }
});
```

### Example 3
```csharp
app.Run(async (HttpContext context) =>
{
    context.Response.Headers["Content-type"] = "text/html";
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        string userAgent = context.Request.Headers["User-Agent"];
        await context.Response.WriteAsync($"<p>{userAgent}</p>");
    }
});
```

---

## HTTP Methods

### GET
- Retrieves data.
- Data in URL.
- Idempotent and cacheable.
- Limited data size.
- Not for sensitive info.

Example:
```
GET /products?category=electronics&brand=apple HTTP/1.1
Host: example.com
```

### POST
- Submits data to the server.
- Data in the body.
- Not idempotent or cacheable.
- Suitable for large or sensitive data.

Example:
```
POST /login HTTP/1.1
Host: example.com
Content-Type: application/x-www-form-urlencoded

username=john&password=secret
```

**Use GET for:** fetching data.  
**Use POST for:** submitting data.

---

## Postman
Postman is a tool for testing and developing APIs.

### Installation
- Download from: [https://www.postman.com/downloads/](https://www.postman.com/downloads/)
- Install for Windows, macOS, or Linux.

### Usage
1. Open Postman.
2. Create a new request.
3. Set HTTP method and URL (e.g., `https://localhost:7070/api/products`).
4. Add headers if needed.
5. Add body data (for POST/PUT).
6. Click **Send**.
7. View response status, headers, and body.

---

## Summary
- **HTTP Protocol:** Foundation of web communication; stateless, request-response model.
- **HTTP Requests:** Contain method, path, headers, and body.
- **HTTP Responses:** Include status code, headers, and body.
- **Methods:** GET (retrieve), POST (submit), PUT (update), DELETE (remove).
- **Status Codes:** Indicate result of requests.
- **Tools:** Chrome Dev Tools for inspection; Postman for testing.
