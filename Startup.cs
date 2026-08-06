using DemoDownloadPage.Models;
using DemoDownloadPage.Models.Interface;
using DemoDownloadPage.Models.Repository;
using DemoDownloadPage.Services;
using DNTCaptcha.Core;

namespace DemoDownloadPage
    {
    public class Startup
        {

        private readonly IConfiguration _config;
        public Startup (IConfiguration config)
            {
            _config = config;
            }

        public void ConfigureServices (IServiceCollection services)
            {
            services.AddHttpClient();
            services.AddMvc();
            services.Configure<ConnString>(_config.GetSection("connString"));
            services.Configure<MailParameters>(_config.GetSection("MailParameters"));
            services.AddScoped<IDownloadRepository, DownloadRepository>();
            services.AddScoped<MailService>();
            services.AddScoped<SmsService>();
            services.AddDNTCaptcha(options =>
            {
                options.UseCookieStorageProvider()
                            .ShowThousandsSeparators(false)
                            .WithEncryptionKey(_config["DNTKey"]);
            });
            }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
            {

            if (env.IsDevelopment())
                {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/ErrorNotFound");
                }
            else
                {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/ErrorNotFound");
                }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=DownloadForm}/{action=SoftwareDownloadPage}/{id?}");
            });
            }
        }
    }
