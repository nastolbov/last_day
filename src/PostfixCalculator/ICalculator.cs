namespace PostfixCalculator;

/// <summary>
/// Specification of a calculator that evaluates arithmetic expressions
/// written in Reverse Polish Notation (postfix form).
/// Supported binary operations: +, -, *, /.
/// Tokens in the input string are expected to be separated by spaces,
/// e.g. "3 4 +" or "5 1 2 + 4 * + 3 -".
/// </summary>
public interface ICalculator
{
    /// <summary>
    /// Evaluates the supplied RPN expression and returns the numeric result.
    /// </summary>
    /// <param name="expression">A non-empty RPN expression.</param>
    /// <returns>The value of the expression.</returns>
    /// <exception cref="System.ArgumentException">
    /// Thrown when the expression is null, empty or malformed.
    /// </exception>
    double Calculate(string expression);
}
