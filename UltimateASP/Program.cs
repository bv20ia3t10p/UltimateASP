using UltimateASP.Extensions;
using NLog;
using Contracts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
    .Services.BuildServiceProvider()
    .GetRequiredService<IOptions<MvcOptions>>()
    .Value.InputFormatters
    .OfType<NewtonsoftJsonPatchInputFormatter>().First();


LogManager.Setup().LoadConfigurationFromFile("./nlog.config");

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggingService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services
    .AddControllers(config =>
    {
        config.RespectBrowserAcceptHeader = true;
        config.ReturnHttpNotAcceptable = true;
        config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
    })
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCsvFormatter()
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Logic before delegate");
    await next.Invoke();
    Console.WriteLine($"Logic after delegate");
});
//app.Run(async context =>
//{
//    Console.WriteLine($"Writing response");
//    await context.Response.WriteAsync("Hello from middleware");
//});

app.Map("/usingmapbranch", builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logic in the UIse method before the next delegate");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the use mtheod after the delegate");
    });
    builder.Run(async context =>
    {
        Console.WriteLine($"Map branch response to the client in the Run method");
        await context.Response.WriteAsync("Hello from the nwe branch.");
    });
});

app.MapWhen(context => context.Request.Query.ContainsKey("testquerystring"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from the mapwhen");
        Console.WriteLine("Mapwhen logic");
    });
});
app.Run();
