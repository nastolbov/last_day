using System.Globalization;

namespace PostfixCalculator;

/// <summary>
/// Reverse Polish Notation calculator.
/// Second TDD iteration: supports either a simple two-operand
/// expression (three tokens) or a three-operand expression composed
/// of a first binary operation followed by a second operation that
/// combines the intermediate result with the third operand
/// (five tokens, form "a b op c op").
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

        return tokens.Length switch
        {
            3 => EvaluateTwoOperands(tokens),
            5 => EvaluateThreeOperands(tokens),
            _ => throw new ArgumentException(
                "Expression length is not supported yet.", nameof(expression)),
        };
    }

    private static double EvaluateTwoOperands(string[] tokens)
    {
        var left = ParseNumber(tokens[0]);
        var right = ParseNumber(tokens[1]);
        return ApplyOperator(left, right, tokens[2]);
    }

    private static double EvaluateThreeOperands(string[] tokens)
    {
        // Form: "a b op1 c op2"  →  ((a op1 b) op2 c)
        var a = ParseNumber(tokens[0]);
        var b = ParseNumber(tokens[1]);
        var intermediate = ApplyOperator(a, b, tokens[2]);
        var c = ParseNumber(tokens[3]);
        return ApplyOperator(intermediate, c, tokens[4]);
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
