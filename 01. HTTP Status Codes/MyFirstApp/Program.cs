namespace MyFirstApp;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);
      var app = builder.Build();

      //app.MapGet("/", () => "Hello World!");

      // Http Provide an object to handle requests and responses
      app.Run(async (HttpContext context) =>
      {
         // make a dummy condition, to test status codes
         if (true)
         {
            context.Response.StatusCode = 200;
         }
         else
         {
            context.Response.StatusCode = 400;
         }

         await context.Response.WriteAsync("Hello, World!");
      });


      app.Run();
   }
}
