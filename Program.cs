using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

/*
 * написать функцию, принимающую строку, 
 * предположительно содержащую дату в формате дд.мм.гггг,
 * разделитель может быть любым не числовым символом и значения дня, месяца и года может не совпадать с форматом.
 * Функция проверяет на корректность введенной даты и возвращает строку в строго определенном формате дд.мм.гггг.
 * По возможности не использовать стандартные функции.
 * Если дата введена не корректно - возвращает пустую строку.
 * Например функция принимает 5/9-11 возвращает 05.09.2011
 * 
 * Проверки :
 * Числа показывающие день и месяц проверяются на  
 * - дни не более 31,
 * - месяцы не более 12
 * Если имеем одно число - дописываем впереди 0
 * Год - для текущей разработки подразумеваем, что год это 4-х значное число
 * Т.е. если значение года из 2-х чисел то дописываем 20, а если 3 - х значное то допишем 2
 * 
 * ltvch 07.10.2019
 */

namespace ParceStringWithDate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string date = /*"5/ 09 /// 111"*/ "5\\ 9 - 1878";

            Console.WriteLine(ParseString(ParseStringUseRegExp(date)));
            Console.WriteLine(ParseString(ParceStringUseSplit(date)));


            Console.ReadLine();
        }

        /// <summary>
        /// Создаем массив строк содержащий день, месяц и год используя регулярное выражение
        /// </summary>
        /// <param name="phrase">Исходная строка</param>
        /// <returns>Возвращаем масив строк</returns>
        public static string[] ParseStringUseRegExp(string phrase)
        {
            ///делим строку с датой на массив групп чисел по шаблону регулярного выражения
            return Regex.Split(phrase, @"(?<day>\d{1,2})\D+(?<month>\d{1,2})\D+(?<year>\d{2,4})"); ;
        }

        /// <summary>
        /// Создаем масив строк содержащий день, месяц и год распарсив строку
        /// </summary>
        /// <param name="phrase">Исходная строка</param>
        /// <returns>Возвращаем масив строк</returns>
        private static string[] ParceStringUseSplit(string phrase)
        {
            ///набор разделителей в тексте для даты
            char[] delimiterChars = { ' ', '\n', '\t', '\0', '/', '\\', '-', ';', ',', '.' };
            ///делим строку с датой на массив групп чисел
            string[] words = phrase.Split(delimiterChars);

            return words;
        }

        /// <summary>
        /// Формируем выходную строку.
        /// </summary>
        /// <param name="words">Массив строковых данных</param>
        /// <returns>Строка с датой</returns>
        public static string ParseString(string[] words)//is suppsoed to parse the string
        {
            ///пустая строка для сформированной даты
            string result = string.Empty;//""

            DelByValue(ref words, "");//удаляем ставшие ненужными пробелы
            #region --- Первая проверка в режиме отладки ---
#if DEBUG
            Debug.WriteLine(string.Concat(words));
#endif
            #endregion
            ///для удобства чтения кода передаем данные из массива в отдельные переменные
            ///Кроме того, таким способом мы игнорируем все другие записи кроме первых трех групп чисел
            string day = words[0];
            string month = words[1];
            string year = words[2];

            //todo Обрабатываем или нет "ненужные" значения в массиве
            /// Обрабатываем "ненужные" значения в массиве
            if (words.Length > 3)
            {
#if !DEBUG
                words = DelByIndex(words);//укорачиваем массив если обрабатываем первые три группы чисел
                return result;                               
#else
                throw new Exception("В дате более чем три группы чисел. Проверте вводимые данные");//или выбрасываем ошибку
#endif
            }

            #region --- Обработка исключений в формате даты ---

            if (day.Length > 2)
            {
#if !DEBUG
                 return result;
#else
                throw new Exception("В дате не может быть более двух чисел");
#endif
            }

            if (Int32.Parse(day) > 31)
            {
#if !DEBUG
                return result;
#else
                throw new Exception("В дате не может быть более 31 дня");
#endif
            }

            day = ZiroInFront(day);//дописывем спереди ноль если нужно

            if (Int32.Parse(month) > 12)
            {
#if !DEBUG
                   return result;
#else
                throw new Exception("Не может быть более 12 месяцев");
#endif
            }

            month = ZiroInFront(month);//дописывем спереди ноль если нужно

            year = ZiroInFront(year);//дописывем спереди ноль если нужно

            if (year.Length <= 2)
            {
                year = year.Insert(0, "20");
            }

            if (year.Length <= 3)
            {
                year = year.Insert(0, "2");
            }
            #endregion

            ///формируем окончательную строку с датой 
            result = string.Format($"{day}.{month}.{year}");

            return result;
        }       

        /// <summary>
        /// Удаляем все значения с определенного индекса
        /// </summary>
        /// <param name="words">Массив значений</param>
        /// <returns>Возврат "укороченной" строки</returns>
        private static string[] DelByIndex(string[] words)
        {
            for (int i = 2; i < words.Length; i++)
            {
                DelByIndex(ref words, i);
            }

            return words;
        }

        /// <summary>
        /// Добавляем ноль к месяцу или дню, если он меньше двух знаков
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Возвращаем значение с "0" спереди</returns>
        private static string ZiroInFront(string value)
        {
            if (Int32.Parse(value) <= 9 && value.Length == 1)
            {
                value = value.Insert(0, "0");
            }

            return value;
        }

        /// <summary>
        /// Удаляем значение из массива по индексу
        /// </summary>
        /// <param name="data">Массив значений</param>
        /// <param name="delIndex">Удадяемый индекс</param>
        public static void DelByIndex(ref string[] data, int delIndex)
        {
            string[] newData = new string[data.Length - 1];
            for (int i = 0; i < delIndex; i++)
            {
                newData[i] = data[i];
            }
            for (int i = delIndex; i < newData.Length; i++)
            {
                newData[i] = data[i + 1];
            }
            data = newData;
        }

        /// <summary>
        /// Удаляем из массива по значению
        /// </summary>
        /// <param name="data">Массив значений</param>
        /// <param name="delValue">Удаляемій параметр</param>
        public static void DelByValue(ref string[] data, string delValue)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == delValue)
                {
                    DelByIndex(ref data, i);
                    i--;
                }
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
