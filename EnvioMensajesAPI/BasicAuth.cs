using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnvioMensajesAPI
{
    public class BasicAuth
    {
        private readonly RequestDelegate _next;
        private readonly string _realm;
        private readonly IOptions<UserAuth> config;
        public static IOptions<ConexionString> connexion;
        public BasicAuth(RequestDelegate next, string realm, IOptions<UserAuth> config, IOptions<ConexionString> conn)
        {
            _next = next;
            _realm = realm;
             this.config = config;
            connexion = conn;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the encoded username and password
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                // Decode from Base64 to string
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                // Split username and password
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];
                // Check if login is correct
                if (IsAuthorized(username, password))
                {
                    await _next.Invoke(context);
                    return;
                }
            }
            // Return authentication type (causes browser to show login dialog)
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            // Add realm if it is not null
            if (!string.IsNullOrWhiteSpace(_realm))
            {
                context.Response.Headers["WWW-Authenticate"] += $" realm=\"{_realm}\"";
            }
            // Return unauthorized
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        // Make your own implementation of this
        public bool IsAuthorized(string username, string password)
        {
            var basicAuthUserName = config.Value.UserName;
            var basicAuthPassword = config.Value.Password;
            // Check that username and password are correct
            return username.Equals(basicAuthUserName, StringComparison.InvariantCultureIgnoreCase)
                   && password.Equals(basicAuthPassword);
        }
    }

    public class UserAuth
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class ConexionString
    {
        public String ConnectionString { get; set; }
    }

}
