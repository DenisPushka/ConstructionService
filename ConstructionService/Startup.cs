using DataAccess;
using DataAccess.Interface;
using DataAccess.Realization;

namespace ConstructionService;

public class Startup
{
    private IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddTransient<DataSql>();
        services.AddTransient<DataSqlUser>();
        services.AddTransient<DataSqlCompany>();
        services.AddTransient<DataSqlFeedBack>();
        services.AddTransient<DataSqlService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
        services.AddTransient<IServiceRepository, ServiceRepository>();
        services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
        services.AddMvcCore();
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "api/{controller}/action");
            endpoints.MapControllers();
            // для включения браузера в доке LaunchSetting.json в 31 строке -> "launchBrowser": true 
        });
    }
}