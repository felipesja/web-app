using webapp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();

app.ConfigurePipeline();

app.Run();
