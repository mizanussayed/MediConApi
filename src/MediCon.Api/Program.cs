using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.ServiceConfigurations;
using MediCon.Api.Configurations.Settings;

using DinkToPdf.Contracts;
using DinkToPdf;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Models;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
var logConfigSettings = LogConfigSettings.Get(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    if (logConfigSettings.FileLogConfig is not null)
    {
        var fileLogConfig = logConfigSettings.FileLogConfig;
        var defaultLogPath = Path.Combine(builder.Environment.ContentRootPath, "..", "logs", "log.txt");

        configuration.WriteTo.Async(x => x.File(
            fileLogConfig.GetPath(defaultLogPath),
            restrictedToMinimumLevel: fileLogConfig.LogLevel,
            rollingInterval: fileLogConfig.RollingInterval,
            retainedFileCountLimit: fileLogConfig.RetainedFileCountLimit,
            flushToDiskInterval: TimeSpan.FromSeconds(fileLogConfig.FlushToDiskIntervalInSeconds),
            shared: fileLogConfig.Shared,
            buffered: fileLogConfig.Buffered));
    }

    configuration.ReadFrom.Configuration(context.Configuration);
});
if (logConfigSettings.LogRequestResponse)
{
    builder.Services.AddHttpLogging(logging => logging.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestMethod | HttpLoggingFields.Duration);
}
#endregion

builder.Services.AddApiConfigurations(builder.Configuration);

var apiConfig = ApiConfigSettings.Get(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(opt => { });

#region Swagger
app.UseSwagger(c => c.PreSerializeFilters.Add((swaggerDoc, _) =>
{
    var servers = new List<OpenApiServer>();

    foreach (var swaggerBaseUrl in apiConfig.SwaggerBaseUrlList)
    {
        servers.Add(new OpenApiServer
        {
            Url = $"{swaggerBaseUrl.Scheme}://{swaggerBaseUrl.Host}/{swaggerBaseUrl.Prefix}",
        });
    }

    swaggerDoc.Servers = servers;
}));
app.UseSwaggerUI();
#endregion

if (apiConfig.UseHttps)
{
    app.UseHttpsRedirection();
}

//app.UseAuthentication();
//app.UseAuthorization();

if (logConfigSettings.LogRequestResponse)
{
    app.UseHttpLogging();
}

foreach (var endpoint in app.Services.GetRequiredService<IEnumerable<IEndpoint>>())
{
    endpoint.MapRoutes(app);
}

await app.RunAsync().ConfigureAwait(false);

