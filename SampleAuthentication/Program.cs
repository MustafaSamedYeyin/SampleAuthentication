
using Microsoft.IdentityModel.Tokens;
using SampleAuthentication.Helper;

namespace SampleAuthentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
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
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ThisIsASecretKeyForJwtTokenGenerationAndOnlyYouShouldKnotIt")),
                    ClockSkew = TimeSpan.FromSeconds(1)
                };
            });
            builder.Services.AddScoped<JwtHelper>();

            var app = builder.Build();

            app.MapOpenApi();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
