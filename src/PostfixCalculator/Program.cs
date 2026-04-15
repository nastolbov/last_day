namespace PostfixCalculator;

/// <summary>
/// Console entry point for the RPN calculator.
/// Reads an expression from the command line (or standard input)
/// and prints the evaluated result.
/// </summary>
public static class Program
{
    public static int Main(string[] args)
    {
        var expression = args.Length > 0
            ? string.Join(' ', args)
            : Console.In.ReadToEnd();

        if (string.IsNullOrWhiteSpace(expression))
        {
            Console.Error.WriteLine("Usage: PostfixCalculator \"<rpn expression>\"");
            return 1;
        }

        ICalculator calculator = new Calculator();
        var result = calculator.Calculate(expression);
        Console.WriteLine(result.ToString(System.Globalization.CultureInfo.InvariantCulture));
        return 0;
    }
}
