using Xunit;

namespace PostfixCalculator.Tests;

/// <summary>
/// Third TDD iteration: arbitrary-length RPN expressions with any
/// mix of operands and operators. In RPN, ordinary infix groupings
/// such as "(1 + 2) * (3 + 4)" become linear postfix strings like
/// "1 2 + 3 4 + *" — no explicit parentheses are needed, and the
/// evaluator must honour the implicit grouping via a stack.
/// </summary>
public class ComplexExpressionTests
{
    private readonly ICalculator _calculator = new Calculator();

    [Fact]
    public void GIVEN_single_number_WHEN_calculated_THEN_returns_that_number()
    {
        var result = _calculator.Calculate("42");

        Assert.Equal(42, result);
    }

    [Fact]
    public void GIVEN_expression_with_two_independent_groups_WHEN_calculated_THEN_combines_them()
    {
        // (1 + 2) * (3 + 4) = 21
        var result = _calculator.Calculate("1 2 + 3 4 + *");

        Assert.Equal(21, result);
    }

    [Fact]
    public void GIVEN_wikipedia_example_WHEN_calculated_THEN_returns_expected_value()
    {
        // 5 + ((1 + 2) * 4) - 3 = 14
        var result = _calculator.Calculate("5 1 2 + 4 * + 3 -");

        Assert.Equal(14, result);
    }

    [Fact]
    public void GIVEN_four_operand_chain_WHEN_calculated_THEN_folds_left_to_right()
    {
        // (((1 + 2) + 3) + 4) = 10
        var result = _calculator.Calculate("1 2 + 3 + 4 +");

        Assert.Equal(10, result);
    }

    [Fact]
    public void GIVEN_decimal_operands_WHEN_calculated_THEN_respects_invariant_culture()
    {
        // (1.5 + 2.5) * 2 = 8
        var result = _calculator.Calculate("1.5 2.5 + 2 *");

        Assert.Equal(8, result);
    }

    [Theory]
    [InlineData("3 4 + 5 * 2 -", 33)]              // (3 + 4) * 5 - 2
    [InlineData("10 2 / 3 + 4 *", 32)]             // ((10 / 2) + 3) * 4
    [InlineData("2 3 4 * +", 14)]                  // 2 + (3 * 4)
    [InlineData("2 3 + 4 5 + *", 45)]              // (2 + 3) * (4 + 5)
    [InlineData("1 2 + 3 + 4 + 5 + 6 +", 21)]      // 1 + 2 + 3 + 4 + 5 + 6
    public void GIVEN_arbitrary_rpn_expression_WHEN_calculated_THEN_returns_expected_value(
        string expression, double expected)
    {
        var result = _calculator.Calculate(expression);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GIVEN_expression_with_missing_operand_WHEN_calculated_THEN_throws_argument_exception()
    {
        // Only one operand but an operator that needs two.
        Assert.Throws<ArgumentException>(() => _calculator.Calculate("1 +"));
    }

    [Fact]
    public void GIVEN_expression_with_extra_operand_WHEN_calculated_THEN_throws_argument_exception()
    {
        // Two values left on the stack at the end → malformed.
        Assert.Throws<ArgumentException>(() => _calculator.Calculate("1 2 3 +"));
    }
}
