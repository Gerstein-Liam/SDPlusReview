using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;
using System.Text;
using System.Diagnostics;
namespace SDPlusApplicationServer_FakeDatabase.Middlewares
{








    public class AppException(int statusCode, string message, string? details)
    {
        public int StatusCode { get; set; } = statusCode;

        public string Message { get; set; } = message;

        public string? Details { get; set; } = details;

    }
    public class HTTPSnifferMiddleware(ILogger<HTTPSnifferMiddleware> logger, IHostEnvironment env) : IMiddleware
    {

        private readonly ILogger<HTTPSnifferMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();
            var request = context.Request;
            var method = request.Method;
            var path = request.Path;
            bool encoding = request.HasJsonContentType();

            string body = "";
            if (request.ContentLength > 0 && method == "POST")
            {

                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;

                Debug.WriteLine($"{method} is JSON?{encoding}: {body}");
            }
            await next(context);
        }
    }
}
