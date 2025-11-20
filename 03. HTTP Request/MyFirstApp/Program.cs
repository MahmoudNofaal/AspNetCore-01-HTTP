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
         // we can get the path or the request
         string path = context.Request.Path;

         // we can get the method of the request, [get, put, post, delete]
         string method = context.Request.Method;

         // set the content type of the response to html
         context.Response.Headers["Content-type"] = "text/html";

         await context.Response.WriteAsync($"<p>{path}</p>");
         await context.Response.WriteAsync($"<p>{method}</p>");
      });

      app.Run();
   }
}
