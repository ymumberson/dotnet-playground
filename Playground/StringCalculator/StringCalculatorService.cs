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

            int number1 = Int32.Parse(input.Substring(0, i));

            if (i+1 >= input.Length) return number1;

            char operand = input[i];

            int j = i+1;
            for (j=i+1; j<input.Length; ++j)
            {
                if (!Char.IsNumber(input[j])) break;
            }

            int number2 = Int32.Parse(input.Substring(i+1, (j - (i+1))));

            switch (operand)
            {
                case '+':
                    return number1 + number2;
            }

            return 0;
        }
    }
}