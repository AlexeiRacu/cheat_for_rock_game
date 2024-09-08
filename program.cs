using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Numerics;

class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
        while (true)
        {
            Console.Write("Введите число: ");
            string userInput = Console.ReadLine();
            if (IsValidNumber(userInput, out BigInteger number))
            {
                Console.WriteLine("Введенное число в формате текста: " + CountInText(number));
            }
            else
            {
                Console.WriteLine("Введенные символы не являются числом или число слишком велико!");
            }
        }
    }

    public static string CountInText(BigInteger number)
    {
        if (number == 0)
            return "ноль";

        if (number < 0)
        {
            return "минус " + CountInText(-number);
        }

        string[] units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };
        string[] teens = { "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
        string[] tens = { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
        string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };
        string[] thousands = { "", "тысяча", "тысячи", "тысяч" };
        string[] millions = { "", "миллион", "миллиона", "миллионов" };
        string[] billions = { "", "миллиард", "миллиарда", "миллиардов" };
        string[] trillions = { "", "триллион", "триллиона", "триллионов" };

        var result = new StringBuilder();

        if (number >= 1_000_000_000_000)
        {
            // Handle trillions
            BigInteger trillion = number / 1_000_000_000_000;
            result.AppendFormat("{0} {1} ", CountInText(trillion), trillions[trillion == 1 ? 1 : (trillion >= 2 && trillion <= 4 ? 2 : 3)]);
            number %= 1_000_000_000_000;
        }

        if (number >= 1_000_000_000)
        {
            // Handle billions
            BigInteger billion = number / 1_000_000_000;
            result.AppendFormat("{0} {1} ", CountInText(billion), billions[billion == 1 ? 1 : (billion >= 2 && billion <= 4 ? 2 : 3)]);
            number %= 1_000_000_000;
        }

        if (number >= 1_000_000)
        {
            // Handle millions
            BigInteger million = number / 1_000_000;
            result.AppendFormat("{0} {1} ", CountInText(million), millions[million == 1 ? 1 : (million >= 2 && million <= 4 ? 2 : 3)]);
            number %= 1_000_000;
        }

        if (number >= 1_000)
        {
            // Handle thousands
            BigInteger thousand = number / 1_000;
            if (thousand == 1)
                result.Append("одна тысяча ");
            else if (thousand == 2)
                result.Append("две тысячи ");
            else
                result.AppendFormat("{0} {1} ", CountInText(thousand), thousands[3]);
            number %= 1_000;
        }

        if (number >= 100)
        {
            // Handle hundreds
            BigInteger hundred = number / 100;
            result.AppendFormat("{0} ", hundreds[(int)hundred]);
            number %= 100;
        }

        if (number >= 20)
        {
            // Handle tens
            BigInteger ten = number / 10;
            result.AppendFormat("{0} ", tens[(int)ten]);
            number %= 10;
        }
        else if (number >= 10)
        {
            // Handle teens
            result.AppendFormat("{0} ", teens[(int)number - 10]);
            number = 0;
        }

        if (number > 0)
        {
            // Handle units
            result.AppendFormat("{0} ", units[(int)number]);
        }

        return result.ToString().Trim();
    }

    public static bool IsValidNumber(string user_data, out BigInteger number)
    {
        // Check if the input is a valid number and fits within the BigInteger type
        if (BigInteger.TryParse(user_data, out number))
        {
            return true;
        }
        else
        {
            number = 0;
            return false;
        }
    }
}
