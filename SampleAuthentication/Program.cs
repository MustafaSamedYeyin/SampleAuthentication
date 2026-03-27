
using Microsoft.IdentityModel.Tokens;
using SampleAuthentication.Helper;
using System.Net;
using System.Text;

namespace SampleAuthentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.WebHost.UseKestrel(options =>
            {
                options.Listen(IPAddress.Parse("127.0.0.4"), 1234, config =>
                {
                    config.UseHttps("C:\\Lab\\Youtube\\Secure Coding\\Auth\\SampleAuthentication\\SampleAuthentication\\certificate\\yourenvironment.pfx");
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddCors();
            builder.Services.AddAuthentication().AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("secret"))),
                    ClockSkew = TimeSpan.FromSeconds(1)
                };
            });
            builder.Services.AddScoped<JwtHelper>();
            var app = builder.Build();
            app.MapOpenApi();
            app.UseCors(config =>
            {
                config.WithOrigins("http://localhost:56132").AllowAnyHeader().AllowAnyMethod();
            });
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
