using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        // Adicionar os serviços de controladores com views
        services.AddControllersWithViews();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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
