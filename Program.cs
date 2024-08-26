var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<MossadAgentMVC.Controllers.MissionController>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]); // הגדרת ה-BaseAddress מהקונפיגורציה
});

// Configure Swagger if needed (uncomment if you want to use Swagger)
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Mission}/{action=Index}/{id?}");

app.Run();
