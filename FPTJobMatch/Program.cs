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
builder.Services.ConfigureApplicationCookie(option =>
{
	option.LoginPath = $"/Identity/Account/Login";
	option.LogoutPath = $"/Identity/Account/Logout";
	option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IApplicationJobRepository, ApplicationJobRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITimeWorkRepository, TimeWordRepository>();
builder.Services.AddScoped<IEmailSender, EmailSendercs>();
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
app.MapRazorPages();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=JobSeeker}/{controller=ApplicationJob}/{action=Index}/{id?}");

app.Run();
