using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories;
using Bulky.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Bulky.DataAccess.DbInitializer;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//Add Dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])) ;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
    opt.LoginPath = "/Identity/Account/Login"; 
    opt.LogoutPath="/Identity/Account/Logout";
});
builder.Services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(100);
});

// 368919316531-2ujhvcjaku1pjaan830vpo4ec69t6ltv.apps.googleusercontent.com client-id
// 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 client secret 
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "368919316531-2ujhvcjaku1pjaan830vpo4ec69t6ltv.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
builder.Services.AddScoped<IDbInitializer,DbInitializer>();
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
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
DatabaseInitialize();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/
app.MapControllerRoute(
            name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();


void DatabaseInitialize()
{
    using (var scope = app.Services.CreateScope())
    {
        var i = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        i.Initialize();
    }
}