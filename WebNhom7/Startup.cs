using _2180607734_LamQuangMinh.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebNhom7.DataAccess;
using WebNhom7.Repositories;

namespace WebNhom7
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.AddControllersWithViews()
                .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("vi-VN");

                var cultures = new CultureInfo[]
                {
                    new CultureInfo("vi-VN"),
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("th-TH"),
                    new CultureInfo("ko-kr"),
                    new CultureInfo("zh-cn"),
                    new CultureInfo("es-ES"),
                    new CultureInfo("ja-JP"),
                    new CultureInfo("fr-FR")
                };

                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;

                services.AddScoped<IProductRepository, EFProductRepository>();
                services.AddScoped<ICategoryRepository, EFCategoryRepository>();

                // Add services to the container.
                services.AddControllersWithViews();
                services.AddDbContext<ApplicationDbContext>(options =>

                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
