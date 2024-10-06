using DiamondKata.Core.Validation;
using Shouldly;

namespace DiamondKata.Core.Tests;

public class ArgumentValidatorTests
{
    [Theory]
    [InlineData("A")]
    [InlineData("Q")]
    [InlineData("F")]
    public void Validate_ForValidInput_NoExceptionIsThrown(string argument)
    {
        var args = new[] { argument };
        GetSut().ValidateCommandLineArguments(args);
    }

    [Fact]
    public void Validate_WhenArgsLengthIsZero_ThrowAggregateException_WithOneInnerException_WithErrorMessage()
    {
        var args = new string[] { };
        var ex = Assert.Throws<AggregateException>(() => GetSut().ValidateCommandLineArguments(args));
        ex.InnerExceptions.Count.ShouldBe(1);
        ex.InnerExceptions[0].Message.ShouldBe("Expected 1 argument, received none");
    }

    [Theory]
    [InlineData("a", "b")]
    [InlineData("q", "w", "e")]
    public void Validate_WhenArgsLengthIsGreaterThanOne_ThrowAggregateException_WithOneInnerException_WithErrorMessage(params string[] args)
    {
        var ex = Assert.Throws<AggregateException>(() => GetSut().ValidateCommandLineArguments(args));
        ex.InnerExceptions.Count.ShouldBe(1);
        ex.InnerExceptions[0].Message.ShouldBe($"Expected 1 argument, received {args.Length}");
    }

    [Theory]
    [InlineData("qwe")]
    [InlineData("4")]
    [InlineData("£")]
    public void
        Validate_WhenArgsCannotBeParsedToAlphabeticalChar_ThrowAggregateException_WithOneInnerException_WithErrorMessage(params string[] args)
    {
        var ex = Assert.Throws<AggregateException>(() => GetSut().ValidateCommandLineArguments(args));
        ex.InnerExceptions.Count.ShouldBe(1);
        ex.InnerExceptions[0].Message.ShouldBe($"Value \"{args[0]}\" is not recognised as alphabetical character");
    }

    [Fact]
    public void Validate_WhenMultipleValidationRulesBroken_ThrowAggregateException_WithCorrectNumberOfInnerExceptions_WithErrorMessages()
    {
        var args = new string[] { "4", "V" };
        var ex = Assert.Throws<AggregateException>(() => GetSut().ValidateCommandLineArguments(args));
        
        ex.InnerExceptions.Count.ShouldBe(2);
        ex.InnerExceptions.ShouldContain(x => x.Message.Equals($"Value \"4\" is not recognised as alphabetical character"));
        ex.InnerExceptions.ShouldContain(x => x.Message.Equals($"Expected 1 argument, received 2"));
    }
    
    
    private IArgumentsValidator GetSut() => new ArgumentsValidator();
}