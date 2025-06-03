var builder = WebApplication.CreateBuilder(args);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/hello", () => "Hello World!")
   .WithOpenApi(); // Optional: makes endpoint show up in Swagger UI

app.Run();
