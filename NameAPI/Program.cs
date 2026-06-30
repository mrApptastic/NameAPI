using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NameBandit.Data;
using NameBandit.Managers;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddScoped<ICategoriesManager, CategoriesManager>();
builder.Services.AddScoped<INamesManager, NamesManager>();
builder.Services.AddScoped<INameCombosManager, NameCombosManager>();
builder.Services.AddScoped<IVibrationsManager, VibrationsManager>();

builder.Services.AddSingleton<AutoMapper.IMapper>(provider =>
{
    var loggerFactory = provider.GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>();
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddMaps(new[] { typeof(Program).Assembly });
    }, loggerFactory);
    return config.CreateMapper();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy(
        name: "AllowOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithExposedHeaders("X-Count");
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString!, ServerVersion.Parse("10.4.32-mariadb"))
);

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Names API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.MapControllers();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.SeedDatabase(context);
}

app.Run();

