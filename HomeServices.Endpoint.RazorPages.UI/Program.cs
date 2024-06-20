using HomeService.Core.Contracts.AdminContracts;
using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.Contracts.BaseService;
using HomeService.Core.Contracts.BidContracts;
using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.Contracts.CityContracts;
using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.Entities;
using HomeService.Core.Entities.Configurations;
using HomeService.Core.Services.AppServices;
using HomeService.Core.Services.Services;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Cache.InMemoryCache;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;
using HomeService.Infrastructure.DataBase.Sql.Dapper.Repositories;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using HomeServices.Endpoint.RazorPages.UI.Infrastructure.MiddleWare;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;


var builder = WebApplication.CreateBuilder(args);


#region Register Services

//Admin
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminAppService, AdminAppService>();
builder.Services.AddScoped<IAdminService, AdminService>();

//Bid
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IBidAppService, BidAppService>();
builder.Services.AddScoped<IBidService, BidService>();

//Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryAppService, CategoryAppService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

//City
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityAppService, CityAppService>();
builder.Services.AddScoped<ICityService, CityService>();

//Comment
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<ICommentService, CommentService>();

//Customer
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerAppService, CustomerAppService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

//Expert
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();
builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
builder.Services.AddScoped<IExpertService, ExpertService>();

//Request
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestAppService, RequestAppService>();
builder.Services.AddScoped<IRequestService, RequestService>();

//Service
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceAppService, ServiceAppService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

//Cache
builder.Services.AddScoped<IInMemoryCacheService, InMemoryCacheService>();

//Account
builder.Services.AddScoped<IAccountAppService, AccountAppService>();

//BaseService
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IBaseAppService, BaseAppService>();

builder.Services.AddScoped<IRequestDapperRepository, RequestDapperRepository>();

#endregion



#region Configuration

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

builder.Services.AddSingleton(siteSettings);

var connectionString = siteSettings.ConnectionStrings.AppConnectionString;


#endregion


#region CacheConfiguration

builder.Services.AddMemoryCache();

#endregion


#region LogConfiguration

builder.Logging.ClearProviders();

builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();

}).UseSerilog((context, config) =>
{
    config.WriteTo.Seq(siteSettings.SeqConfigurations.Url, LogEventLevel.Information);
});

#endregion


#region EfConfiguration

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(siteSettings.ConnectionStrings.AppConnectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

#endregion


#region IdentityConfiguration

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

#endregion


builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.CustomExceptionHandlingMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
