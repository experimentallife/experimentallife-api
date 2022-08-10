using Autofac;
using Autofac.Extras.DynamicProxy;

using Project.Common.DI;
using Project.Common.Global;

namespace Project.Extensions;

public class AutofacRegister : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    var cacheType = new List<Type>();

    var dependencyService = typeof(IDependencyService);
    var dependencyServiceArray = GlobalData.FxAllTypes
        .Where(x => dependencyService.IsAssignableFrom(x) && x != dependencyService).ToArray();
    builder.RegisterTypes(dependencyServiceArray)
        .AsImplementedInterfaces()
        .PropertiesAutowired()
        .InstancePerDependency()
        .EnableInterfaceInterceptors()
        .InterceptedBy(cacheType.ToArray());

    builder.RegisterType<DisposableContainer>()
        .As<IDisposableContainer>()
        .InstancePerLifetimeScope();
  }
}