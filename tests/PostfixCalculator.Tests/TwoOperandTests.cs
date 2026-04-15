using Xunit;

namespace PostfixCalculator.Tests;

/// <summary>
/// First TDD iteration: the calculator has to evaluate the simplest
/// possible RPN expressions — two operands followed by exactly one
/// binary operator. Test names follow the Given/When/Then convention.
/// </summary>
public class TwoOperandTests
{
    private readonly ICalculator _calculator = new Calculator();

    [Fact]
    public void GIVEN_two_positive_numbers_WHEN_addition_operator_THEN_returns_sum()
    {
        var result = _calculator.Calculate("2 3 +");

        Assert.Equal(5, result);
    }

    [Fact]
    public void GIVEN_two_positive_numbers_WHEN_subtraction_operator_THEN_returns_difference()
    {
        var result = _calculator.Calculate("10 4 -");

        Assert.Equal(6, result);
    }

    [Fact]
    public void GIVEN_two_positive_numbers_WHEN_multiplication_operator_THEN_returns_product()
    {
        var result = _calculator.Calculate("6 7 *");

        Assert.Equal(42, result);
    }

    [Fact]
    public void GIVEN_two_positive_numbers_WHEN_division_operator_THEN_returns_quotient()
    {
        var result = _calculator.Calculate("20 4 /");

        Assert.Equal(5, result);
    }

    [Theory]
    [InlineData("-3 5 +", 2)]
    [InlineData("-3 -5 +", -8)]
    [InlineData("-10 -4 -", -6)]
    [InlineData("-6 7 *", -42)]
    [InlineData("-20 -4 /", 5)]
    public void GIVEN_expression_with_negative_operands_WHEN_calculated_THEN_returns_expected_value(
        string expression, double expected)
    {
        var result = _calculator.Calculate(expression);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GIVEN_unknown_operator_WHEN_calculated_THEN_throws_argument_exception()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Calculate("1 2 ?"));
    }

    [Fact]
    public void GIVEN_null_or_empty_expression_WHEN_calculated_THEN_throws_argument_exception()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Calculate(""));
        Assert.Throws<ArgumentException>(() => _calculator.Calculate("   "));
    }
}
