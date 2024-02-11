using System.Text;

namespace LibraryApiTests.Utils
{
    public class UtilsTests : IUtilsTests
    {
        public UtilsTests() 
        { 
        }

        public string GenerateISBN()
        {
            Random random = new Random();

            // Generate the first 12 digits of the ISBN randomly
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                sb.Append(random.Next(10));
            }

            string partialISBN = sb.ToString();

            // Calculate the check digit of the ISBN
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int digit = partialISBN[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            int checkDigit = (10 - (sum % 10)) % 10;

            // Return the complete ISBN
            return partialISBN + checkDigit.ToString();
        }
    }
}
