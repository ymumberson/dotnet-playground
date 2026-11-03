using Prime.Services;

namespace Prime.UnitTests.Services {
    public class PrimeService_IsPrimeShould
    {
        private readonly PrimeService _primeService;

        public PrimeService_IsPrimeShould()
        {
            _primeService = new PrimeService();
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnsFalse(int value)
        {
            // Act
            bool result = _primeService.IsPrime(value);

            // Assert
            Assert.False(result, $"{value} should not be prime.");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(71)]
        [InlineData(97)]
        public void IsPrime_WithPrimeNumbers_ReturnsTrue(int value)
        {
            // Act
            bool result = _primeService.IsPrime(value);

            Assert.True(result, $"{value} should be prime.");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(12)]
        [InlineData(22)]
        [InlineData(44)]
        [InlineData(96)]
        public void IsPrime_WithNonPrimeNumbers_ReturnsFalse(int value)
        {
            // Act
            bool result = _primeService.IsPrime(value);

            // Assert
            Assert.False(result, $"{value} should not be prime");
        }
    }
}