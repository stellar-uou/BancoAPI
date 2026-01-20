var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (antes do Build)
builder.Services.AddCors(options =>
{
    options.AddPolicy("test-cors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware
app.UseCors("test-cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se estiver rodando em Docker, remova ou comente para evitar problema
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
