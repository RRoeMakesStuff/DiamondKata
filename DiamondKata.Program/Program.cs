// See https://aka.ms/new-console-template for more information

using DiamondKata.Core;
using DiamondKata.Core.Validation;
using DiamondKata.Program.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace DiamondKata.Program;

public class Program
{
    private readonly IHandler _handler;
    private readonly IArgumentsValidator _validator;

    public Program(IHandler handler, IArgumentsValidator validator)
    {
        _handler = handler;
        _validator = validator;
    }

    public void Run(string[] args)
    {
        try
        {
            _validator.ValidateCommandLineArguments(args);
        }
        catch (AggregateException e)
        {
            Console.WriteLine("ERROR: Input not valid. Returned following errors;");
            foreach (var inner in e.InnerExceptions)
            {
                Console.WriteLine(inner.Message);
            }

            return;
        }
        
        char letter = Char.Parse(args[0].ToUpperInvariant());
        Console.WriteLine(_handler.Handle(letter));
    }

    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureArgumentsValidator()
            .ConfigureHandlerService()
            .BuildServiceProvider();

        var program = new Program(
            serviceProvider.GetService<IHandler>() ?? throw new ArgumentNullException(nameof(IHandler)),
            serviceProvider.GetService<IArgumentsValidator>() ??throw new ArgumentNullException(nameof(IArgumentsValidator))
        );

        program.Run(args);
    }
}