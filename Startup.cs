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
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });
  }
}