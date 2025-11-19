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

         if (context.Request.Headers.ContainsKey("User-Agent"))
         {
            string userAgent = context.Request.Headers["User-Agent"];

            await context.Response.WriteAsync($"<p>{userAgent}</p>");
         }

      });

      app.Run();
   }

}

