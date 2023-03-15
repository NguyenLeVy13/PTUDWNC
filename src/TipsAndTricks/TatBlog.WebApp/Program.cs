using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using Microsoft.EntityFrameworkCore;
using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Mapsters;

var builder = WebApplication.CreateBuilder(args);
{

    builder
        .ConfigureMvc()
        .ConfigureServices()
        .ConfigureMapster();
}

var app = builder.Build();
{
    app.UseRequestPipeLine();
    app.UseBlogRoutes();
    app.UseDataSeeder();
}

/*//Thêm dữ liệu mẫu vào CSDL
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
}*/

app.Run();
