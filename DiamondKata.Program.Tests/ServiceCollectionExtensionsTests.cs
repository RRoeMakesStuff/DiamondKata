using DiamondKata.Core;
using DiamondKata.Core.Validation;
using DiamondKata.Program.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DiamondKata.Program.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void ConfigureHandlerService_RegistersExpectedService()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureHandlerService()
            .BuildServiceProvider();
        var service = serviceProvider.GetService<IHandler>();
        
        Assert.NotNull(service);
        Assert.IsType<Handler>(service);
    }
    
    [Fact]
    public void ConfigureArgumentValidatorService_RegistersExpectedService()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureArgumentsValidator()
            .BuildServiceProvider();
        var service = serviceProvider.GetService<IArgumentsValidator>();
        
        Assert.NotNull(service);
        Assert.IsType<ArgumentsValidator>(service);
    }
}