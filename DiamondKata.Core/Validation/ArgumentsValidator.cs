namespace DiamondKata.Core.Validation;

public class ArgumentsValidator: IArgumentsValidator
{
    public void ValidateCommandLineArguments(string[] args)
    {
        var exceptions = new List<Exception>();
        
        if (args.Length < 1)
        {
            exceptions.Add(new ArgumentException($"Expected 1 argument, received none"));
            throw new AggregateException(exceptions); // Throw here as any other checks are redundant past this point
        }

        if (args.Length > 1)
        {
            exceptions.Add(new ArgumentException($"Expected 1 argument, received {args.Length}"));
        }

        if (!Char.TryParse(args[0].ToUpper(), out var val) || !char.IsLetter(val))
        {
            exceptions.Add(new ArgumentException($"Value \"{args[0]}\" is not recognised as alphabetical character"));
        }

        if (exceptions.Any())
        {
            throw new AggregateException(exceptions);
        }
    }
}