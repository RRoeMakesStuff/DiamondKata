using DiamondKata.Core;
using DiamondKata.Core.Validation;
using Moq;

namespace DiamondKata.Program.Tests;

public class ProgramTests
{
    private static Mock<IHandler> _handler;
    private static Mock<IArgumentsValidator> _validator;

    public ProgramTests()
    {
        _handler = new Mock<IHandler>();
        _validator = new Mock<IArgumentsValidator>();
    }
    
    [Fact]
    public void Run_WhenInputValid_HandlerAndValidatorAreCalled()
    {
        var args = new [] { "a" };
        
        GetSut().Run(args);
        
        _validator.Verify(x => x.ValidateCommandLineArguments(args), Times.Once);
        _handler.Verify(x => x.Handle(char.Parse(args[0].ToUpper())), Times.Once);
    }

    [Fact]
    public void Run_WhenValidatorThrowsAggregateException_ErrorsHandledGracefully_HandlerIsNotCalled()
    {
        var args = new [] { "a" };

        _validator.Setup(x => x.ValidateCommandLineArguments(It.IsAny<string[]>()))
            .Throws(new AggregateException("Error validating"));
        
        GetSut().Run(args);
        
        _validator.Verify(x => x.ValidateCommandLineArguments(args), Times.Once);
        _handler.Verify(x => x.Handle(It.IsAny<char>()), Times.Never);
    }

    private Program GetSut() => new Program(_handler.Object, _validator.Object);
}