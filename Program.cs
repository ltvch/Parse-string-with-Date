using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * написать функцию, принимающую строку, 
 * предположительно содержащую дату в формате дд.мм.гггг,
 * разделитель может быть любым не числовым символом и значения дня, месяца и года может не совпадать с форматом.
 * Функция проверяет на корректность введенной даты и возвращает строку в строго определенном формате дд.мм.гггг.
 * По возможности не использовать стандартные функции.
 * Если дата введена не корректно - возвращает пустую строку.
 * Например функция принимает 5/9-11 возвращает 05.09.2011
 *
 */


namespace ParceStringWithDate
{
    class Program
    {
        static void Main(string[] args)
        {
            string date = "05/ 9 - 11";

            char[] chars = date.ToCharArray();

            ParseString1(date);
           // char[] ret = date.Where(x => !char.IsNumber(x)).ToArray();

            //string day = new string(date.TakeWhile(c => char.IsDigit(c)).ToArray());
            //string NotDate = date.Substring(day.Length);

            //string month = new string(NotDate.TakeWhile(c => !char.IsSymbol(c)).ToArray());
            //string NotMonth = NotDate.Substring(month.Length);

            //           string yyyy = new string(date.TakeWhile(c => char.IsDigit(c)).ToArray());
            //           date.TakeWhile(c => char.IsDigit(c)).ToArray();


            //foreach(char c in chars)
            //{
            //    if (char.IsDigit(c) & (char.IsDigit(c)))
            //    {

            //    }
            //}

            Console.ReadLine();
        }

        public static string ParseString1(string phrase)//is suppsoed to parse the string
        {
            string fin = "\n";
            char[] delimiterChars = { ' ', '\n', ',', '\0', '/', '\\', '-', ';' };
            string[] words = phrase.Split(delimiterChars);
            string digitword = string.Empty;

            // words.Split(del);

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]) || !string.IsNullOrWhiteSpace(words[i]))
                {
                    digitword += words[i];
                }
            }

            TabToString(words);//I check the content of my tab

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != null)
                {
                    fin += words[i] + ";";
                    Console.WriteLine(fin);//help for debug
                }
            }

            return fin;
        }


        public static void TabToString(string[] montab)//display the content of my tab
        {
            foreach (string s in montab)
            {
                Console.WriteLine(s);
            }
        }

        public static string ParseFile(string FilePath)
        {
            using (var streamReader = new StreamReader(FilePath))
            {
                return streamReader.ReadToEnd().Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty).Replace(',', ';');
            }
        }

        public static string ParseString(string input)
        {
            input = input.Replace(Environment.NewLine, string.Empty);
            input = input.Replace(" ", string.Empty);
            string[] chunks = input.Split(',');
            StringBuilder sb = new StringBuilder();
            foreach (string s in chunks)
            {
                sb.Append(s);
                sb.Append(";");
            }
            return sb.ToString(0, sb.ToString().Length - 1);
        }
    }
}
