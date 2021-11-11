using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskyJ.Interface.AspNetCore
{
    public class Startup
    {
        public string message = "";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);
            //configure cookie authentication
            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Auth/Index";
                });

            services.AddControllersWithViews();

            // configure jwt authentication
            /*
                        var appSettingsSection = Configuration.GetSection("AppSettings");
                        //TODO secret
                        var key = Encoding.ASCII.GetBytes("appSettings.Secret");
                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(key),
                                ValidateIssuer = false,
                                ValidateAudience = false,
                                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                                ClockSkew = TimeSpan.Zero
                            };

                            options.Events = new JwtBearerEvents
                            {
                                OnAuthenticationFailed = ctx =>
                                {
                                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    message += "From OnAuthenticationFailed:\n";
                                    message += FlattenException(ctx.Exception);
                                    return Task.CompletedTask;
                                },

                                OnChallenge = ctx =>
                                {
                                    message += "From OnChallenge:\n";
                                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    ctx.Response.ContentType = "text/plain";
                                    return ctx.Response.WriteAsync(message);
                                },

                                OnMessageReceived = ctx =>
                                {
                                    message = "From OnMessageReceived:\n";
                                    ctx.Request.Headers.TryGetValue("Authorization", out var BearerToken);
                                    if (BearerToken.Count == 0)
                                        BearerToken = "no Bearer token sent\n";
                                    message += "Authorization Header sent: " + BearerToken + "\n";
                                    return Task.CompletedTask;
                                },

                                OnTokenValidated = ctx =>
                                {
                                    Debug.WriteLine("token: " + ctx.SecurityToken.ToString());
                                    return Task.CompletedTask;
                                }
                            };
                        });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var dbParameters = new Dictionary<string, string>();
            dbParameters["STSDBHTTPBaseURL"] = Configuration.GetValue<string>("STSDBHTTPBaseURL");
            Business.TaskyJManager.SetRepoTask(Globals.EngineWindows.ResolveRepoTask(Configuration.GetValue<string>("taskydb"), dbParameters));
            Business.TaskyJManager.SetRepoCat(Globals.EngineWindows.ResolveRepoCat(Configuration.GetValue<string>("taskydb")));
            Business.TaskyJManager.SetRepoUsr(Globals.EngineWindows.ResolveRepoUsr(Configuration.GetValue<string>("taskydb")));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // turn on PII logging
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Index}/{id?}");
            });
        }

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);
                exception = exception.InnerException;
            }
            return stringBuilder.ToString();
        }
    }
}
