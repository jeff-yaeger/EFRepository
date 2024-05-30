namespace Repository
{
    using System.Reflection;
    using EF;
    using global::EntityDefaults;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IServiceCollection AddEfRepository(this IServiceCollection serviceCollection, Type context, Type setter = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            serviceCollection.AddScoped(typeof(IRepository<,>), context);

            // serviceCollection.FirstOrDefault(e => e.ServiceType == typeof(IUserService))?.Lifetime, ServiceLifetime.Scoped
            
            if (setter != null && typeof(IEntitySetter).IsAssignableFrom(setter))
            {
                var isTransient = false;
                var isScoped = false;
                var constructors = setter.GetConstructors();
                foreach (var constructor in constructors)
                {
                    var parameters = constructor.GetParameters();
                    foreach (var parameter in parameters)
                    {
                        var typeLifeTime = serviceCollection.FirstOrDefault(e => e.ServiceType == parameter.ParameterType)?.Lifetime;
                        if (typeLifeTime != null)
                        {
                            switch (typeLifeTime)
                            {
                                case ServiceLifetime.Transient:
                                    isTransient = true;
                                    break;
                                case ServiceLifetime.Scoped:
                                    isScoped = true;
                                    break;
                            }
                        }
                    }
                }

                if (isTransient)
                {
                    serviceCollection.AddTransient(typeof(IEntitySetter), setter);
                }
                else if (isScoped)
                {
                    serviceCollection.AddScoped(typeof(IEntitySetter), setter);
                }
                else
                {
                    serviceCollection.AddSingleton(typeof(IEntitySetter), setter);
                }
            }
            else
            {
                serviceCollection.AddSingleton<IEntitySetter, DoNothingEntitySetter>();
            }

            return serviceCollection;
        }
    }
}