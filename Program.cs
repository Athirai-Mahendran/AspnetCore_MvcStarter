using Serilog;
using Starter_API.Hubs;
using Starter_Proxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Add BaseServiceProxy
builder.Services.AddHttpClient<ServiceProxy>();
builder.Services.AddTransient<ServiceProxy>();

//SignalR
//builder.Services.AddSingleton(sp => new HubConnectionBuilder()
//    .WithUrl("https://localhost:5001/notificationHub")  // Update with your actual API URL
//    .Build());

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reads settings from appsettings.json
    .Enrich.FromLogContext()                       // Adds contextual information to logs
    .WriteTo.Console()                             // Writes logs to the console
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Writes logs to a file
    .CreateLogger();

builder.Host.UseSerilog();// Replace the default logging with Serilog
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//redirect 404 to 
app.UseStatusCodePages(async context =>
{
  if (context.HttpContext.Response.StatusCode == 404)
  {
    context.HttpContext.Response.Redirect("/WorkOut/WorkOut/NotFound");
  }
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
