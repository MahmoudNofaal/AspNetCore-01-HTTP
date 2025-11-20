namespace MyFirstApp;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);
      var app = builder.Build();

      //app.MapGet("/", () => "Hello World!");

      app.Run(async (HttpContext context) =>
      {
         context.Response.Headers["Content-type"] = "text/html";

         // first check if it's a GET request
         if (context.Request.Method == "GET")
         {
            // query string as: https://localhost:7009/?id=12345
            if (context.Request.Query.ContainsKey("id"))
            {
               // get the value of "id" from query string
               string id = context.Request.Query["id"];

               await context.Response.WriteAsync($"<p>{id}</p>");
            }

         }

      });

      app.Run();
   }
}
