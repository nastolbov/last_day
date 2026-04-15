using System.Globalization;

namespace PostfixCalculator;

/// <summary>
/// Reverse Polish Notation calculator.
/// This first TDD iteration only understands the simplest possible
/// expressions: two operands followed by exactly one binary operator
/// (three whitespace-separated tokens).
/// </summary>
public class Calculator : ICalculator
{
    public double Calculate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentException(
                "Expression must not be null or empty.", nameof(expression));
        }

        var tokens = expression.Split(
            ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (tokens.Length != 3)
        {
            throw new ArgumentException(
                "Expression must contain exactly two operands and one operator.",
                nameof(expression));
        }

        var left = ParseNumber(tokens[0]);
        var right = ParseNumber(tokens[1]);
        return ApplyOperator(left, right, tokens[2]);
    }

    private static double ParseNumber(string token) =>
        double.Parse(token, CultureInfo.InvariantCulture);

    private static double ApplyOperator(double left, double right, string op) => op switch
    {
        "+" => left + right,
        "-" => left - right,
        "*" => left * right,
        "/" => left / right,
        _ => throw new ArgumentException($"Unknown operator: '{op}'.", nameof(op)),
    };
}
