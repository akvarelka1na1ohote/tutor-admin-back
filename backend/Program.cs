using backend.DataAccess;
using backend.InfoJSON;
using backend.Controllers;
using backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApplicationDbContext>();

// Регистрация репозиториев
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<MatchPerformerRepository>();
builder.Services.AddScoped<MatchClientRepository>();
builder.Services.AddScoped<PerformerRepository>();
builder.Services.AddScoped<SubjectRepository>();
builder.Services.AddScoped<TimetableClientRepository>();
builder.Services.AddScoped<TimetablePerformerRepository>();
builder.Services.AddScoped<UserRepository>();

// Регистрация контроллеров
builder.Services.AddScoped<ClientsController>();
builder.Services.AddScoped<UsersController>(); 
builder.Services.AddScoped<MatchPerformersController>();
builder.Services.AddScoped<PerformersController>();
builder.Services.AddScoped<MatchClientsController>();
builder.Services.AddScoped<SubjectsController>();
builder.Services.AddScoped<TimetablesClientsController>();
builder.Services.AddScoped<TimetablesPerformersController>();

// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Инициализация БД
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.EnsureCreatedAsync();
    await Info.SeedData(db);
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

//app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

//app.UseAuthorization();
app.MapControllers();

app.Run();
