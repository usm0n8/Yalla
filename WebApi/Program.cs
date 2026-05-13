using Infrastructure;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<orderService>();
builder.Services.AddScoped<SubscriptionsService>();
builder.Services.AddScoped<MenusService>();
builder.Services.AddScoped<CompaniesService>();
builder.Services.AddScoped<DataContext>();
builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();