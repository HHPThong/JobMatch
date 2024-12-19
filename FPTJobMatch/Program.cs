using FPTJobMatch.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FPTJobMatch.Repository;
using FPTJobMatch.Repository.IRepository;
using Microsoft.AspNetCore.Identity.UI.Services;
using FPTJobMatch.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSendercs>();
builder.Services.ConfigureApplicationCookie(option =>
{
	option.AccessDeniedPath = $"/Identity/Page/Account/AccessDenied";
	option.AccessDeniedPath = $"/Identity/Page/Account/Login";
	option.AccessDeniedPath = $"/Identity/Page/Account/Logout";
});
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
	name: "default",
	pattern: "{area=Employer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
