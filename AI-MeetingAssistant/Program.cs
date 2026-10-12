using AI_MeetingAssistant.Services;

namespace AI_MeetingAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // 1. Configure the CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Verify this matches your browser address bar exactly!
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddHttpClient<GeminiService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowFrontend");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}