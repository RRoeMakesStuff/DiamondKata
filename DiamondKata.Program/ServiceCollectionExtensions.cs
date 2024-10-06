using DiamondKata.Core;
using DiamondKata.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace DiamondKata.Program.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureHandlerService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IHandler, Handler>();
        return serviceCollection;
    }

    public static IServiceCollection ConfigureArgumentsValidator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IArgumentsValidator, ArgumentsValidator>();
        return serviceCollection;
    }
}