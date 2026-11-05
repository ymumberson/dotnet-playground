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

        [Theory]
        [InlineData("2*2", 4)]
        [InlineData("7*9", 63)]
        [InlineData("2*1", 2)]
        [InlineData("0*2", 0)]
        [InlineData("21*345", 7245)]
        public void Calculate_WithMultiply_ShouldMultiplyTwoNumbers(string input, int expected)
        {
            // Act
            int result = _stringCalculatorService.Calculate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4/2", 2)]
        [InlineData("63/9", 7)]
        [InlineData("63/7", 9)]
        [InlineData("0/2", 0)]
        [InlineData("7245/21", 345)]
        public void Calculate_WithDivide_ShouldDivideTwoNumbers(string input, int expected)
        {
            // Act
            int result = _stringCalculatorService.Calculate(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_WithDivisionByZero_ShouldThrowDivideByZeroException()
        {
            // Act & Assert
            Assert.Throws<DivideByZeroException>(
                () => _stringCalculatorService.Calculate("1/0")
            );
        }

        [Theory]
        [InlineData("1+")]
        [InlineData("string")]
        [InlineData("*3")]
        public void Calculate_WithInvalidInput_ShouldThrowArgumentException(string input)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => _stringCalculatorService.Calculate(input)
            );
        }

        [Theory]
        [InlineData("2", 2)]
        [InlineData("22", 22)]
        [InlineData("4321", 4321)]
        public void Calculate_WithSingleNumbers_ReturnsThatNumber(string input, int expected)
        {
            // Act
            int result = _stringCalculatorService.Calculate(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}