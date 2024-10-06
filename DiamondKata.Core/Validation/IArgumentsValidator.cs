namespace DiamondKata.Core.Validation;

public interface IArgumentsValidator
{
    public void ValidateCommandLineArguments(string[] args);
}