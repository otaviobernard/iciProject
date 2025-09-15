using Microsoft.EntityFrameworkCore; 
using NoticiasProvaIci.Data;
using NoticiasProvaIci.Services;
using NoticiasProvaIci.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); 
} 

app.UseHttpsRedirection(); 
app.UseStaticFiles(); 
app.UseRouting(); 
app.UseAuthorization(); 

app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=FirstPage}/{action=Initial}/{id?}"
    ); 

app.MapGet("/", context =>
{
    context.Response.Redirect("/Home");
    return Task.CompletedTask;
});

app.Run();