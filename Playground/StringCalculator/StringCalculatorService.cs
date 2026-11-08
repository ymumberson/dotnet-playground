using System.ComponentModel;
using System.Text;

namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        private List<Token> _tokens = new();
        private int _position = 0;

        private Token Current => _position < _tokens.Count ? _tokens[_position] : throw new ArgumentException("Unexpected end of expression.");
        
        public int Calculate(string input)
        {
            _tokens = TokeniseInput(input);
            _position = 0;

            int result = Calculate();

            if (_position != _tokens.Count)
                throw new ArgumentException("Unexpected token.");

            return result;
        }

        private int Calculate()
        {
            int result = ParseTerm();

            while (true)
            {
                if (Match(TokenType.Plus))
                {
                    result += ParseTerm();
                }
                else if (Match(TokenType.Minus))
                {
                    result -= ParseTerm();
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        private Token Consume()
        {
            return _tokens[_position++];
        }

        private bool Match(TokenType type)
        {
            if (_position >= _tokens.Count || _tokens[_position].token != type) return false;

            _position++;
            return true;
        }

        private int ParseTerm()
        {
            int result = ParseFactor();

            while (true)
            {
                if (Match(TokenType.Multiply))
                {
                    result *= ParseFactor();
                }
                else if (Match(TokenType.Divide))
                {
                    int divisor = ParseFactor();

                    if (divisor == 0)
                        throw new DivideByZeroException();
                    
                    result /= divisor;
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private int ParseFactor()
        {
            if (Match(TokenType.Number))
            {
                return (int)_tokens[_position-1].Value;
            }

            if (Match(TokenType.OpenBracket))
            {
                int result = Calculate();

                if (!Match(TokenType.CloseBracket))
                    throw new ArgumentException("Missing closing bracket.");
                
                return result;
            }

            throw new ArgumentException("Expected a number or opening bracket.");
        }
        
        private List<Token> TokeniseInput(string input)
        {
            List<Token> tokens = new();

            int start = 0;
            bool previousCharWasOperator = false;
            bool previousCharWasNumber = false;
            for (int i=0; i<input.Length; ++i)
            {
                if (Char.IsNumber(input[i]) || ((previousCharWasOperator || i == 0) && input[i] == '-'))
                {
                    previousCharWasOperator = false;
                    previousCharWasNumber = true;

                    if (i == input.Length-1)
                    {
                        Double num = Double.Parse(input.Substring(start, i-start + 1));
                        tokens.Add(new Token(TokenType.Number, num));
                    }
                }
                else
                {
                    if (previousCharWasNumber)
                    {
                        Double num = Double.Parse(input.Substring(start, i-start));
                        tokens.Add(new Token(TokenType.Number, num));
                    }
                    
                    switch (input[i])
                    {
                        case '+':
                            tokens.Add(new Token(TokenType.Plus));
                            break;
                        case '-':
                            tokens.Add(new Token(TokenType.Minus));
                            break;
                        case '/':
                            tokens.Add(new Token(TokenType.Divide));
                            break;
                        case '*':
                            tokens.Add(new Token(TokenType.Multiply));
                            break;
                        case '(':
                            tokens.Add(new Token(TokenType.OpenBracket));
                            break;
                        case ')':
                            tokens.Add(new Token(TokenType.CloseBracket));
                            break;
                        case ' ':
                            break;
                        default:
                            throw new ArgumentException($"'{input}' is not a valid input.");
                    }
                    previousCharWasOperator = true;
                    previousCharWasNumber = false;
                    start = i+1;
                }
            }

            return tokens;
        }

        private string TokensToString(List<Token> tokens)
        {
            StringBuilder str = new();
            foreach(Token t in tokens)
            {
                switch (t.token)
                {
                    case TokenType.Number:
                        str.Append(t.Value);
                        break;
                    case TokenType.Plus:
                        str.Append('+');
                        break;
                    case TokenType.Minus:
                        str.Append('-');
                        break;
                    case TokenType.Multiply:
                        str.Append('*');
                        break;
                    case TokenType.Divide:
                        str.Append('/');
                        break;
                    case TokenType.OpenBracket:
                        str.Append('(');
                        break;
                    case TokenType.CloseBracket:
                        str.Append(')');
                        break;
                }
                str.Append(',');
            }
            return str.ToString();
        }
    }

    public enum TokenType
    {
        Number,
        Plus,
        Minus,
        Multiply,
        Divide,
        OpenBracket,
        CloseBracket
    }

    public record Token(TokenType token, double Value = 0);
}