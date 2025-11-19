using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

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
         StreamReader reader = new StreamReader(context.Request.Body);

         string body = await reader.ReadToEndAsync();

         Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(body);

         if (queryDict.ContainsKey("firstName"))
         {
            string firstName = queryDict["firstName"][0];

            await context.Response.WriteAsync(firstName);
         }

      });


      app.Run();
   }
}
