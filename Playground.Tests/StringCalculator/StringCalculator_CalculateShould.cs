using StringCalculator.Services;

namespace StringCalculator.Tests
{
    public class StringCalculator_CalculateShould
    {
        private readonly StringCalculatorService _stringCalculatorService;
        
        public StringCalculator_CalculateShould()
        {
            _stringCalculatorService = new StringCalculatorService();
        }
        
        [Theory]
        [InlineData("1+1", 2)]
        [InlineData("1+12", 13)]
        [InlineData("100+43", 143)]
        [InlineData("35+12", 47)]
        public void Calculate_WithAdd_ShouldAddTwoNumbers(string input, int expected)
        {
            // Act
            int result = _stringCalculatorService.Calculate(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}