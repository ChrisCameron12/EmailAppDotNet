var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<EmailService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Enables API endpoint discovery
builder.Services.AddSwaggerGen();           // Adds Swagger generator

builder.Services.AddScoped<EmailService>(); // your email service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             // Serve Swagger UI
    app.UseSwaggerUI();          // Serve Swagger JSON
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
