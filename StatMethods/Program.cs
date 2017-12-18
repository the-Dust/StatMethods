using StatMethods.MedianFilters;
using StatMethods.Parsers;
using StatMethods.Winsorizators;
using System;

namespace StatMethods
{
    class Program
    {
#region UI Messages
        private static string greetingMessage = Environment.NewLine + "Какую операцию необходимо выполнить?" +
            Environment.NewLine + "1 - Винсоризация массива данных" +
            Environment.NewLine + "2 - Медианная фильтрация на массиве данных" +
            Environment.NewLine + "3 - Демонстрация винсоризации со стандартными массивами" +
            Environment.NewLine + "Введите номер операци (1, 2 или 3): ";

        private static string enterArray = "Введите массив данных. Числа массива необходимо ввести через пробел." +
            Environment.NewLine + "Это могут быть дробные числа." +
            Environment.NewLine + "Для записи дробных чисел может быть использована как точка, так и запятая." +
            Environment.NewLine + "Пример ввода: 323 343.56 22,1 653.456" + Environment.NewLine;

        private static string enterWinsParameter = "Введите степень винсоризации.";

        private static string enterMedianParameter = "Введите размер окна фильтрации.";

        private static string parameterCharachter = "Это должно быть целое число не менее 2 и не " +
            "более размера массива." + Environment.NewLine;

        private static string resultOfTask = "Результат выполнения задания:";

        private static string anythingElse = "Выполнить другую операцию? Введите \"Да\", если " +
            "хотите выполнить другую операцию, либо нажмите Enter для завершения приложения" + Environment.NewLine;
#endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте!");
            while (true)
            {
                Console.Write(greetingMessage);

                //1-winsorization, 2 - median filtration, 3 - demonstration
                int operationNumber = ChooseAction();

                if (operationNumber == 3)
                {
                    WinsorizationDemonstration();
                    continue;
                }

                Console.WriteLine(enterArray);

                DataParser parser = new DataParser();

                double[] parsedArray = null;

                do
                {
                    string inputArray = Console.ReadLine();
                    parsedArray = parser.ParseArray(inputArray);
                    Console.WriteLine(parser.ValidationMessage);
                }
                while (parsedArray == null);

                
                if (operationNumber == 1)
                    Console.WriteLine(enterWinsParameter);
                else if (operationNumber == 2)
                    Console.WriteLine(enterMedianParameter);
                Console.WriteLine(parameterCharachter);

                int parsedParameter = -1;

                do
                {
                    string inputParameter = Console.ReadLine();
                    parsedParameter = parser.ParseParameter(inputParameter, parsedArray.Length);
                    Console.WriteLine(parser.ValidationMessage);
                }
                while (parsedParameter == -1);

                Console.WriteLine(resultOfTask);

                if (operationNumber == 1)
                {
                    Winsorizator wins = new Winsorizator();
                    double output = wins.FindWinsorizedMedium(parsedArray, parsedParameter);
                    Console.WriteLine(output);
                }
                else
                {
                    MedianFilter filter = new MedianFilter();
                    double[] output = filter.GetMedianFiltration(parsedArray, parsedParameter);
                    Console.WriteLine(string.Join(" ", output));
                }

                Console.WriteLine(anythingElse);
                string answer = Console.ReadLine();
                if (!answer.ToLower().Contains("да"))
                    break;
            }
        }

        static int ChooseAction()
        {
            int opNumber = 0;
            while (true)
            {
                string operation = Console.ReadLine();
                int.TryParse(operation, out opNumber);
                if (opNumber > 3 || opNumber < 1)
                    Console.WriteLine("Введено неверное значение параметра. Попробуйте еще раз.");
                else
                    break;
            }
            return opNumber;
        }

        static void WinsorizationDemonstration()
        {
            double[] demoFirst = { 2, 3, 17, 6, 1 };
            double[] demoSecond = { 17, 38, 9, 63, 41, 15 };
            double[] demoThird = { 7, 82, 33, 33, 33, 1, 56 };

            Winsorizator wins = new Winsorizator();

            Console.WriteLine("Демонстрационные значения винсоризации на заданных массивах " +
                Environment.NewLine + "(m - степень винсоризации)");
            Console.WriteLine($"Массив [2, 3, 17, 6, 1] и m = 2: {wins.FindWinsorizedMedium(demoFirst,2)}");
            Console.WriteLine($"Массив [17, 38, 9, 63, 41, 15] и m = 4: {wins.FindWinsorizedMedium(demoSecond, 4)}");
            Console.WriteLine($"Массив [7, 82, 33, 33, 33, 1, 56] и m = 2: {wins.FindWinsorizedMedium(demoThird, 2)}");
        }
    }
}
