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
         // we can set custom headers
         context.Response.Headers["MyKey"] = "my value";

         // we can override existing headers
         context.Response.Headers["Server"] = "My server";

         // we can set content type
         context.Response.Headers["Content-Type"] = "text/html";

         // we can write to the response body directly, type as HTML
         await context.Response.WriteAsync("<h1>Hello</h1>");
         await context.Response.WriteAsync("<h2>World</h2>");
      });

      app.Run();
   }
}
