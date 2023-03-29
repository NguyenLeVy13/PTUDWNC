using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Mapsters;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .ConfigureCors()
        .ConfigureNLog()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster();
}

var app = builder.Build();
{
    app.SetupRequestPipeline();

    app.Run();
}

