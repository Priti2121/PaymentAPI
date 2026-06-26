using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;

namespace PaymentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // PostgreSQL Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins(
                            "http://localhost:4200",
                            "https://payment-management-orcin.vercel.app"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowAngular");

            app.UseAuthorization();

            app.MapControllers();
            app.MapGet("/", () =>
            {
                return Results.Content("""
    <!DOCTYPE html>
    <html>
    <head>
        <title>Payment API</title>
        <style>
            body{
                font-family:Arial;
                background:#f4f4f4;
                display:flex;
                justify-content:center;
                align-items:center;
                height:100vh;
                margin:0;
            }
            .box{
                background:white;
                padding:40px;
                border-radius:10px;
                box-shadow:0 0 15px rgba(0,0,0,.2);
                text-align:center;
            }
            a{
                display:inline-block;
                margin-top:20px;
                padding:10px 20px;
                background:#007bff;
                color:white;
                text-decoration:none;
                border-radius:5px;
            }
        </style>
    </head>
    <body>
        <div class="box">
            <h1>Payment API is Live</h1>
            <p>Backend is running successfully.</p>
            <a href="/swagger">Open Swagger</a>
        </div>
    </body>
    </html>
    """, "text/html");
            });

            app.Run();
        }
    }
}