using System;
using System.Globalization;
using System.Linq;

namespace INNorPINFL
{
    class Program
    {
        // /Это главная функция программа
        static void Main(string[] args)
        {
            bool result_pinfl = isPINFLCorrect("31210632040244");
            bool result_inn = isINNCorrect("476403680"); //476403680
            Console.WriteLine(result_pinfl + " " + result_inn + " " + isPINFLCorrect("91210632040244"));
            Console.ReadLine();
        }

        //Проверка ПИНФЛ на корректность, согласно алгоритма
        static bool isPINFLCorrect(String pinfl)
        {
            int total = 0;
            int weight_digit = 0;
            int remainder = 0;
            DateTime dt;
            String sub_dt;

            if (pinfl.Length == 14)
            {
                sub_dt = pinfl.Substring(1, 6);
                if (DateTime.TryParseExact(sub_dt, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)
                   && Char.IsDigit(pinfl[0])
                   && Enumerable.Range(1, 6).Contains((int)Char.GetNumericValue(pinfl[0])))
                {
                    for (int i = 0; i <= pinfl.Length - 2; i++)
                    {
                        if (Char.IsDigit(pinfl[i]))
                        {
                            remainder = i % 3;
                            if (remainder == 0)
                                weight_digit = 7;
                            else if (remainder == 1)
                                weight_digit = 3;
                            else
                                weight_digit = 1;

                            total = total + (int)Char.GetNumericValue(pinfl[i]) * weight_digit;
                        }
                        else
                            return false;
                    }
                    if (total % 10 == (int)Char.GetNumericValue(pinfl[pinfl.Length - 1]))
                        return true;
                }
                else
                    return false;

            }
            return false;

        }

        //Проверка ИНН на корректность, согласно алгоритма
        static bool isINNCorrect(String inn)
        {
            bool result = false;
            if (inn.Length != 9)
                return result;
            try
            {
                float innFloat = (Int32.Parse(inn.Substring(0, 1)) * 37 +
                                  Int32.Parse(inn.Substring(1, 1)) * 29 +
                                  Int32.Parse(inn.Substring(2, 1)) * 23 +
                                  Int32.Parse(inn.Substring(3, 1)) * 19 +
                                  Int32.Parse(inn.Substring(4, 1)) * 17 +
                                  Int32.Parse(inn.Substring(5, 1)) * 13 +
                                  Int32.Parse(inn.Substring(6, 1)) * 7 +
                                  Int32.Parse(inn.Substring(7, 1)) * 3) / 11f;

                int key = (int)(9 - (innFloat - (int)(innFloat)) * 9);
                return key == Int32.Parse(inn.Substring(8, 1));
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        //Проверка, что именно передано в качестве аргумента - ИНН или ПИНФЛ
        //Возвращает 0, если корректность ИНН или ПИНФЛ не подтверждена
        static int isINNorPINFL(String innorpinfl)
        {
            if (isPINFLCorrect(innorpinfl))
                return 2;
            else
            if (isINNCorrect(innorpinfl))
                return 1;

            return 0;
        }

    }

}
