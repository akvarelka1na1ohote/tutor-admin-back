using backend.DataAccess;
using backend.InfoJSON;
using backend.Controllers;
using backend.Repositories; // ��������� ������������ ���� � �������������


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApplicationDbContext>(); // ������������ ApplicationDbContext �������� �� ������������

// ����������� ������������
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<MatchPerformerRepository>();
builder.Services.AddScoped< MatchClientRepository>();
builder.Services.AddScoped<PerformerRepository>();
builder.Services.AddScoped< SubjectRepository>();
builder.Services.AddScoped<TimetableClientRepository>();
builder.Services.AddScoped<TimetablePerformerRepository>();
builder.Services.AddScoped<UserRepository>();

// ����������� ������������
builder.Services.AddScoped<ClientsController>();
builder.Services.AddScoped<UsersController>(); 
builder.Services.AddScoped<MatchPerformersController>();
builder.Services.AddScoped<PerformersController>();
builder.Services.AddScoped<MatchClientsController>();
builder.Services.AddScoped<SubjectsController>();
builder.Services.AddScoped<TimetablesClientsController>();
builder.Services.AddScoped<TimetablesPerformersController>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5173", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

//using var scope = app.Services.CreateScope();
//await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//await dbContext.Database.EnsureCreatedAsync();

// ������������� ��
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
app.UseCors("AllowLocalhost5173");

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();
