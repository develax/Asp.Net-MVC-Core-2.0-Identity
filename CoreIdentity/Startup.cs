using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using CoreIdentity.Core;
using CoreIdentity.Core.Helpers;

namespace CoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ChangeDefaultValidationClasses();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }

        #region Helpers

        /// <summary>
        /// Change from default validation CSS classes to 'Bootstrap 4 beta' validation classes.
        /// In particular it changes values of a bunch of static readonly variables in HtmlHelper class.
        /// </summary>
        private void ChangeDefaultValidationClasses()
        {
            ReflectionHelper.SetPublicFieldValue<HtmlHelper>(nameof(HtmlHelper.ValidationInputCssClassName), Constants.Validation.CSS.InvalidInput);
            ReflectionHelper.SetPublicFieldValue<HtmlHelper>(nameof(HtmlHelper.ValidationInputValidCssClassName), Constants.Validation.CSS.ValidInput);
            ReflectionHelper.SetPublicFieldValue<HtmlHelper>(nameof(HtmlHelper.ValidationMessageCssClassName), Constants.Validation.CSS.InvalidMessage);
            ReflectionHelper.SetPublicFieldValue<HtmlHelper>(nameof(HtmlHelper.ValidationMessageValidCssClassName), Constants.Validation.CSS.ValidMessage);
        }

        #endregion
    }
}
