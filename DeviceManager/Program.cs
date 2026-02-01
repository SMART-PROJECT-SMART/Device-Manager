using DeviceManager.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebApi();
builder.Services.AddAppConfiguration(builder.Configuration);
builder.Services.AddMongoDBServices();
builder.Services.AddSimulatorNotification();
var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
