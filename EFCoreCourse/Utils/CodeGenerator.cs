using System.Text;

namespace EFCoreCourse.Utils
{
    public static class CodeGenerator
    {
        private static readonly Random _random = new();
        private static readonly object _lock = new();

        private const int MIN_NUMBER = 100000;
        private const int MAX_NUMBER = 999999;
        private const int LETTER_COUNT = 3;

        public static string Generate()
        {
            lock (_lock)
            {
                int number = _random.Next(MIN_NUMBER, MAX_NUMBER);
                return number.ToString();
            }
        }

        public static string Generate(string name, string lastName)
        {
            if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Name and LastName must be provided.");
            }

            char firstLetterName = char.ToUpper(name[0]);
            char firstLetterLastName = char.ToUpper(lastName[0]);

            StringBuilder codeBuilder = new();
            codeBuilder.Append(firstLetterName);
            codeBuilder.Append(firstLetterLastName);

            int number;
            lock (_lock)
            {
                // .Next(minValue, maxValue) is exclusive of maxValue, so we add 1
                number = _random.Next(MIN_NUMBER, MAX_NUMBER + 1);
            }

            codeBuilder.Append(number);

            return codeBuilder.ToString();
        }

        public static string Generate(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                throw new ArgumentException("A valid email must be provided.");
            }

            // Extract part before @
            string username = email.Split('@')[0];

            if (username.Length < LETTER_COUNT)
            {
                throw new ArgumentException("Email username must be at least 3 characters long.");
            }

            // Take 3 random letters from the username
            StringBuilder codeBuilder = new();

            lock (_lock)
            {
                for (int i = 0; i < LETTER_COUNT; i++)
                {
                    int index = _random.Next(username.Length);
                    char letter = char.ToLower(username[index]);
                    codeBuilder.Append(letter);
                }

                int number = _random.Next(MIN_NUMBER, MAX_NUMBER + 1);
                codeBuilder.Append(number);
            }

            return codeBuilder.ToString();
        }
    }
}
