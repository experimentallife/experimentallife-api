using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Project.Common.DI;
using Project.Entity.System;
using Project.Extensions;

public class Startup
{
  public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
  {
    Configuration = configuration;
    WebHostEnvironment = webHostEnvironment;
  }

  public IConfiguration Configuration { get; }
  private IWebHostEnvironment WebHostEnvironment { get; }

  public void ConfigureServices(IServiceCollection services)
  {
    services.Configure<Settings>(Configuration.GetSection("Settings"));
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
    services.AddAutoMapperSetup();
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
  }

  public void ConfigureContainer(ContainerBuilder builder)
  {
    builder.RegisterModule(new AutofacRegister());
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    AutofacHelper.Container = app.ApplicationServices.GetAutofacRoot();

    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
    });
  }
}