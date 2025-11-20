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
            // user agent is the browser info string like: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/
            string userAgent = context.Request.Headers["User-Agent"];

            await context.Response.WriteAsync($"<p>{userAgent}</p>");
         }

      });

      app.Run();
   }

}

