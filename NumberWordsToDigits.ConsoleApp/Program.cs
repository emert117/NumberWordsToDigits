using NumberWordsToDigits.Business;

namespace NumberWordsToDigits.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Text: ");
            string input = Console.ReadLine();
            //string input = "He paid twenty millions for three such cars.";
            INumbersToWordsConverter converter = new NumbersToWordsConverter();
            string output = converter.ConvertNumberWordsToDigits(input);
            Console.WriteLine(output);
        }
    }
}