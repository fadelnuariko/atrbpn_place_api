using Microsoft.EntityFrameworkCore;
using PlacesAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlacesDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) => {
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted) {
       
        context.Response.Clear();
        context.Response.StatusCode = 404;
        await context.Response.WriteAsJsonAsync(new { message = "The requested resource was not found." });

    } else if (context.Response.StatusCode == 405 && !context.Response.HasStarted) {
        
        context.Response.Clear();
        context.Response.StatusCode = 405;
        await context.Response.WriteAsJsonAsync(new { message = "Method not allowed." });

    }
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapGet("/", async context => {
        await context.Response.WriteAsync("Welcome to the Places API!");
    });
});

app.Run();
