namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        public int Calculate(string input)
        {
            int i;
            for (i=0; i<input.Length; ++i)
            {
                if (!Char.IsNumber(input[i])) break;
            }

            if (!Int32.TryParse(input.Substring(0, i), out int number1))
            {
                throw new ArgumentException($"'{input}' is an invalid input");
            }

            if (i == input.Length)
            {
                if (Char.IsNumber(input[i-1]))
                {
                    return number1;
                }
                else
                {
                    throw new ArgumentException($"'{input}' is an invalid input");
                }
            }

            char operand = input[i];

            int j;
            for (j=i+1; j<input.Length; ++j)
            {
                if (!Char.IsNumber(input[j])) break;
            }

            if (!Int32.TryParse(input.Substring(i+1, (j - (i+1))), out int number2))
            {
                throw new ArgumentException($"'{input}' is an invalid input");
            }

            switch (operand)
            {
                case '+':
                    return number1 + number2;
                case '*':
                    return number1 * number2;
                case '/':
                    return number1 / number2;
            }

            throw new ArgumentException($"'{input}' is an invalid input");
        }
    }
}