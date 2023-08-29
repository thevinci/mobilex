using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using backend.Helpers;
using backend.Repositories;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);
var IsDevelopment = builder.Environment.IsDevelopment();

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddAutoMapper(typeof(AutoMapperProfile));

    // configure strongly typed settings object
    services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

    // configure DI for application services
    services.AddSingleton<DataContext>();

    services.AddScoped<IActivityRepository, ActivityRepository>();
    services.AddScoped<IActivityService, ActivityService>();

    services.AddScoped<IJobRepository, JobRepository>();
    services.AddScoped<IJobService, JobService>();

    services.AddScoped<IJobRunRepository, JobRunRepository>();
    services.AddScoped<IJobRunService, JobRunService>();

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserService, UserService>();

    if (IsDevelopment)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}

var app = builder.Build();

// ensure database and tables exist
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.MapControllers();
    // app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "../cdn")),
        RequestPath = "/cdn"
    });
    app.UseAuthorization();

    if (IsDevelopment)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}

app.Run();
