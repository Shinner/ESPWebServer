using ESPWebServer;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseKestrel(o =>
    {
        o.ListenAnyIP(1833, l => l.UseMqtt());
        o.ListenAnyIP(5000);
    });

    webBuilder.UseStartup<Startup>();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
