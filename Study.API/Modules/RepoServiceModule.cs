using System.Reflection;
using Autofac;
using Study.Cache;
using Study.Core.Repositories;
using Study.Core.Services;
using Study.Core.UnitOfWorks;
using Study.Repository;
using Study.Repository.Repositories;
using Study.Repository.UnitOfWorks;
using Study.Service.Mapping;
using Study.Service.Services;
using Module = Autofac.Module;

namespace Study.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            
            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(type: typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(type: typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();



        }
    }
}
