using DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<DAL.GoogleNewsDAL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "googleNewsRoute",
    pattern: "GoogleNews/GetAllNews",
    defaults: new { controller = "GoogleNews", action = "GetAllNews" });

app.MapControllerRoute(
    name: "googleNewsRoute",
    pattern: "GoogleNews/GetItem/{title}",
    defaults: new { controller = "GoogleNews", action = "GetItem" });

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();



