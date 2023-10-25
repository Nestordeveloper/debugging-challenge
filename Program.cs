var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Hint: is everything that needs to be here listed?

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();//Se añade Use Session, ya que podría deriba a un problema si no lo aplicamos y usamos string sessions

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
