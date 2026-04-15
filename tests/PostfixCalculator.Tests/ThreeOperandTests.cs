using Xunit;

namespace PostfixCalculator.Tests;

/// <summary>
/// Second TDD iteration: expressions consisting of three operands
/// and two operators. These tests force the calculator to evaluate
/// an intermediate result and then combine it with the third operand.
/// </summary>
public class ThreeOperandTests
{
    private readonly ICalculator _calculator = new Calculator();

    [Fact]
    public void GIVEN_three_operands_WHEN_two_additions_THEN_returns_total_sum()
    {
        // (2 + 3) + 4 = 9
        var result = _calculator.Calculate("2 3 + 4 +");

        Assert.Equal(9, result);
    }

    [Fact]
    public void GIVEN_three_operands_WHEN_add_then_multiply_THEN_applies_operators_left_to_right()
    {
        // (2 + 3) * 4 = 20
        var result = _calculator.Calculate("2 3 + 4 *");

        Assert.Equal(20, result);
    }

    [Fact]
    public void GIVEN_three_operands_WHEN_subtract_then_divide_THEN_applies_operators_left_to_right()
    {
        // (20 - 4) / 2 = 8
        var result = _calculator.Calculate("20 4 - 2 /");

        Assert.Equal(8, result);
    }

    [Theory]
    [InlineData("10 2 * 5 +", 25)]   // (10 * 2) + 5
    [InlineData("10 2 / 3 -", 2)]    // (10 / 2) - 3
    [InlineData("1 2 + 3 *", 9)]     // (1 + 2) * 3
    [InlineData("-1 -2 + 3 *", -9)]  // (-1 + -2) * 3
    public void GIVEN_three_operand_expression_WHEN_calculated_THEN_returns_expected_value(
        string expression, double expected)
    {
        var result = _calculator.Calculate(expression);

        Assert.Equal(expected, result);
    }
}
