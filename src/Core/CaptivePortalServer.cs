using Core.Models;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
namespace Core
{
    public class CaptivePortalServer : AsyncServerBase
    {
        private readonly ILoginService loginService;
        private readonly string pagesDirectory;
        public CaptivePortalServer(EndPoint endPoint,ILoginService loginService,string pagesDirectory) : base(endPoint)
        {
            this.loginService = loginService;
            this.pagesDirectory = pagesDirectory;
        }

        protected override async Task<HttpResponse> ProccesRequest(Request request)
        {
            try
            {
                var httpRequest = request.HttpRequest;
                if (httpRequest.Method == "GET" && httpRequest.Uri == "/")
                {
                    
                        string indexHtmlStr = await File.ReadAllTextAsync(Path.Combine(pagesDirectory,"index.html"));
                        return new HttpResponse(
                            200,"OK",
                            new() { { "Content-Type", "text/html" }},
                            indexHtmlStr
                        );
                }
                if (httpRequest.Method == "GET" && httpRequest.Uri == "/script.js")
                {
                    string scriptStr = await File.ReadAllTextAsync(Path.Combine(pagesDirectory, "script.js"));
                    return new HttpResponse(
                        200,"OK",
                        new() { { "Content-Type", "text/javascript" } },
                        scriptStr
                        );
                }
                if (httpRequest.Method=="GET" && httpRequest.Uri=="/style.css")
                {
                    string styleStr = await File.ReadAllTextAsync(Path.Combine(pagesDirectory, "style.css"));
                    return new HttpResponse(
                        200,"OK",
                        new() { { "Content-Type", "text/css" } },
                        styleStr
                        );
                }
                
                if (httpRequest.Method == "POST" && httpRequest.Uri == "/login")
                {

                    using (JsonDocument jsonDocument = JsonDocument.Parse(httpRequest.Body))
                    {
                        JsonElement root = jsonDocument.RootElement;
                        string username = root.GetProperty("username").GetString() ?? throw new NotImplementedException();
                        string password = root.GetProperty("password").GetString() ?? throw new NotImplementedException();
                        try
                        {
                            await loginService.LoginAsync(username, password, request.IP);

                            return new HttpResponse(
                            200,"OK",
                            new(),
                            "{}"
                            );
                        }
                        catch (Exception ex)
                        {
                            return new HttpResponse(401,"Unauthorized", new(), "{}");
                        }
                    }                    
                }
                if(httpRequest.Method=="POST" &&httpRequest.Uri=="/logout")
                {

                    await loginService.LogoutAsync(request.IP);
                    return new HttpResponse(
                            200, "OK",
                            new(),
                            "{}"
                            ); ;
                }
            }
            catch(Exception e)
            {

                throw;            
            }
        }
    }
}
