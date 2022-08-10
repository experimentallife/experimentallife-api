using Autofac.Extensions.DependencyInjection;

using ConfigurationSubstitution;

using dotenv.net;

public class Program
{
  public static void Main(string[] args)
  {
    DotEnv.Load();

    CreateHostBuilder(args)
    .Build()
    .Run();
  }

  private static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureAppConfiguration((ctx, builder) =>
          {
            builder.EnableSubstitutions();
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
          });
}
