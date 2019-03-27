using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UNiDAYS.Identity.Models;
using UNiDAYS.Identity.Repositories;
using UNiDAYS.Identity.Stores;

namespace UNiDAYS.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<UserModel>()
                .AddUserStore<UserStore>()
                .AddUserManager<UserManager<UserModel>>()
                .AddSignInManager<SignInManager<UserModel>>();

            services.AddTransient<IRepository<UserModel>, SqlRepository<UserModel>>();
            services.AddTransient<ISqlTranslator<UserModel>, UserSqlTranslator>();
            services.AddTransient<IMethodLookup, SqlMethodLookup>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Identity}/{action=Create}/{id?}");
            });
        }
    }
}
