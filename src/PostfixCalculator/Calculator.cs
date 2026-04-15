using System.Globalization;

namespace PostfixCalculator;

/// <summary>
/// Reverse Polish Notation calculator.
/// Final TDD iteration: a classic stack-based evaluator that handles
/// expressions of arbitrary length. Every numeric token is pushed
/// onto a stack; every operator pops its two operands, applies the
/// binary operation and pushes the result back. A well-formed RPN
/// expression always ends with exactly one value on the stack —
/// that value is the result.
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

        var stack = new Stack<double>();

        foreach (var token in tokens)
        {
            if (IsOperator(token))
            {
                if (stack.Count < 2)
                {
                    throw new ArgumentException(
                        $"Operator '{token}' requires two operands.", nameof(expression));
                }

                var right = stack.Pop();
                var left = stack.Pop();
                stack.Push(ApplyOperator(left, right, token));
            }
            else
            {
                stack.Push(ParseNumber(token));
            }
        }

        if (stack.Count != 1)
        {
            throw new ArgumentException(
                "Malformed RPN expression: operands left unconsumed.", nameof(expression));
        }

        return stack.Pop();
    }

    private static bool IsOperator(string token) =>
        token is "+" or "-" or "*" or "/";

    private static double ParseNumber(string token)
    {
        if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
        {
            throw new ArgumentException($"Unknown token: '{token}'.", nameof(token));
        }

        return value;
    }

    private static double ApplyOperator(double left, double right, string op) => op switch
    {
        "+" => left + right,
        "-" => left - right,
        "*" => left * right,
        "/" => left / right,
        _ => throw new ArgumentException($"Unknown operator: '{op}'.", nameof(op)),
    };
}
