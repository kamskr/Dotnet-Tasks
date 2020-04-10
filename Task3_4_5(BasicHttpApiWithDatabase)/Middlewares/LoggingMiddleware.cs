using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Task3.Services;


namespace Task3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        // "Scoped" SERVICE SHOULDN'T DO CONSTRUCTOR DI!!
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request != null) {
                string path = context.Request.Path; // /api/students
                string method = context.Request.Method; // GET, POST
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";

                using(StreamReader reader=new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                // save to log file / log to database
                SaveLogData("path:" + path + ";method:" + method +";queryString:" + queryString + ";body:" + bodyStr + "\n");
            }
            await _next(context);
        }

        private static void SaveLogData(string data) {
           File.AppendAllText("logs.txt", data);
        }
    }
}
