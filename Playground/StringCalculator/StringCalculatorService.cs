namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        public int Calculate(string input)
        {
            return CalculateWithWhiteSpaceRemoved(RemoveWhiteSpace(input));
        }
        
        private string RemoveWhiteSpace(string input)
        {
            return new string([.. input.Where(c => !Char.IsWhiteSpace(c))]);
        }

        private int CalculateWithWhiteSpaceRemoved(string input)
        {
            input = ResolveParenthetis(input);

            // Check for first number
            int i = 0;
            if (i >= input.Length || (input[i] != '-' && !Char.IsNumber(input[i])))
            {
                throw new ArgumentException($"'{input}' is an invalid input");
            }
            for (i=1; i<input.Length; ++i)
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


            // Split operand
            char operand = input[i];


            // Check for second number
            int j = i+1;
            if (j >= input.Length || (input[j] != '-' && !Char.IsNumber(input[j])))
            {
                throw new ArgumentException($"'{input}' is an invalid input");
            }
            for (j=i+2; j<input.Length; ++j)
            {
                if (!Char.IsNumber(input[j])) break;
            }

            if (!Int32.TryParse(input.Substring(i+1, (j - (i+1))), out int number2))
            {
                throw new ArgumentException($"'{input}' is an invalid input");
            }

            // Apply operand
            switch (operand)
            {
                case '+':
                    return number1 + number2;
                case '*':
                    return number1 * number2;
                case '/':
                    return number1 / number2;
                case '-':
                    return number1 - number2;
            }

            throw new ArgumentException($"'{input}' is an invalid input: {operand} is not supported");
        }

        private string ResolveParenthetis(string input)
        {
            int openBracketIndex = -1;
            for (int i=0; i<input.Length-1; ++i)
            {
                if (input[i] == '(')
                {
                    openBracketIndex = i;
                    break;
                }
            }

            if (openBracketIndex == -1) return input;

            int closeBracketIndex = -1;
            int numUnresolvedBrackets = 0;
            for (int i=openBracketIndex+1; i<input.Length; ++i)
            {
                if (input[i] == '(')
                {
                    numUnresolvedBrackets++;
                }
                else if (input[i] == ')')
                {
                    if (numUnresolvedBrackets == 0)
                    {
                        closeBracketIndex = i;
                        break;
                    }
                    numUnresolvedBrackets--;
                }
            }

            if (closeBracketIndex == -1) return input;

            string parenthetis = CalculateWithWhiteSpaceRemoved(input.Substring(openBracketIndex+1, closeBracketIndex - openBracketIndex)).ToString();
            string output = input.Substring(0, openBracketIndex) + parenthetis;
            if (closeBracketIndex < input.Length-1)
            {
                output += input.Substring(closeBracketIndex+1, input.Length-1 - closeBracketIndex);
            }

            return ResolveParenthetis(output);
        }
    }
}