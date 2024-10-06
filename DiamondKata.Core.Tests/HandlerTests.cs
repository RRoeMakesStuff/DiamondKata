using Shouldly;

namespace DiamondKata.Core.Tests;

public class HandlerTests
{
    [Theory]
    [InlineData('A', "A\n")]
    [InlineData('B', " A\nB B\n A\n")]
    [InlineData('C', "  A\n B B\nC   C\n B B\n  A\n")]
    public void CalculateShapeString_ReturnsExpectedString(char letter, string expectedString)
    {
        var result = GetSut().Handle(letter);
        result.ShouldBe(expectedString);
    }
    
    [Theory]
    [InlineData('q')]
    [InlineData('w')]
    [InlineData('8')]
    [InlineData('$')]
    public void Handle_ThrowsArgumentException_WhenLetterIsNotValid(char letter)
    {
        var error = Assert.Throws<ArgumentException>(() => GetSut().Handle(letter));
        error.Message.ShouldBe("Argument is required to be an uppercase letter");
    }

    private IHandler GetSut() => new Handler();
}