using System.IO;
using dotnetApiToRedis.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace dotnetApiToRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new RedisDB());
            
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "dotnetApiToRedis",
                    Version = "v1",
                    Description = "dotnetApiToRedis ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "gzz",
                        Email = "gaozengzhi@gmail.com",
                        Url = "http://gaozengzhi.cn"
                    },
                    License = new License
                    {
                        Name = "gzz",
                        Url = "http://gaozengzhi.cn"
                    }
                });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取程序所在目录   AppDomain.CurrentDomain.BaseDirectory
                var sawggerXmlPath = Path.Combine(basePath, "dotnetApiToRedis.xml");
                //var xmlModelPath = Path.Combine(basePath, "ApiModel.xml");//这个就是Model层的xml文件名
                //c.IncludeXmlComments(xmlModelPath);

                c.IncludeXmlComments(sawggerXmlPath);

                c.EnableAnnotations();
            
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerTest V1");
                c.RoutePrefix = string.Empty;
            });
            
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
