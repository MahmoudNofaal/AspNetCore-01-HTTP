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

         if (context.Request.Headers.ContainsKey("Authorization"))
         {
            string auth = context.Request.Headers["Authorization"];

            await context.Response.WriteAsync($"<p>{auth}</p>");
         }

      });


      app.Run();
   }
}
