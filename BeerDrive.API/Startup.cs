using BeerDrive.Application;
using BeerDrive.Application.Abstractions.Persistence;
using BeerDrive.Application.Abstractions.Storage;
using BeerDrive.Infrastructure;
using BeerDrive.Infrastructure.Persistence;
using BeerDrive.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace BeerDrive.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped<IFileRepository, FileRepostory>();

        services.AddTransient<IFileStorage, LocalFileStorage>();

        services.AddTransient<FileEntryService>();

        services.AddSwaggerGen();

        services.AddDbContext<BeerDriveDbContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings:Default"])
        );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseAuthentication();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
