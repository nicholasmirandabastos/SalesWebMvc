using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração do DbContext
        services.AddDbContext<SalesWebMvcContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SalesWebMvcContext")
                ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));

        // Adicionando o SeedingService ao contêiner de DI
        services.AddScoped<SeedingService>();
        services.AddScoped<SellerService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped <SalesRecordService>();

        // Adicionar os serviços de controladores com views
        services.AddControllersWithViews();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
    {
        Console.WriteLine($"Environment: {env.EnvironmentName}"); // Mostra o ambiente no console

        var enUS = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(enUS),
            SupportedCultures = new List<CultureInfo> { enUS },
            SupportedUICultures = new List<CultureInfo> { enUS }
        };

        app.UseRequestLocalization(localizationOptions);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            seedingService.Seed(); // Chama o método Seed para popular os dados
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Middleware padrão
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Usando UseEndpoints em vez de MapControllerRoute
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
