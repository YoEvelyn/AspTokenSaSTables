using AspTokenSaSTables.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Token Sas Generator",
        Version = "v1"
    });
});

string azurekeys = builder.Configuration.GetConnectionString("AzureStorageKeys");
builder.Services.AddTransient<ServiceSaSToken>(z => new ServiceSaSToken(azurekeys));


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Token SaS Generator");
    options.RoutePrefix = "";
});

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
