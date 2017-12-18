using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StatMethods.Parsers
{
    public class DataParser
    {
        public string ValidationMessage { get; private set; } = "";

        private static string tryAgain = $"{Environment.NewLine}Попробуйте ввести еще раз. {Environment.NewLine}";

        private string emptyTemplate = "Вместо массива данных введена пустая строка. " + tryAgain;

        private string errorTemplate = "В позиции (позициях): {0} введены данные, " +
                       Environment.NewLine + "не являющиеся числовыми либо не соответствующие диапазону. " +
                       tryAgain;

        private string parameterTemplate = "Введенный параметр не является числовым значением либо " +
                       Environment.NewLine + "не соответствует заданным условиям. " +
                       tryAgain;

        private string rangeTemplate = "Размер параметра не может быть меньше 2 и больше размера массива. " +
                       tryAgain;

        private string arraySizeTemplate = "Массив не может состоять из одного элемента. " +
                       tryAgain;

        public double[] ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                ValidationMessage = emptyTemplate;
                return null;
            }

            input = input.Replace('.', ',').Trim(' ');

            string[] tempArray = Regex.Split(input, @"\s+");
            double[] parsedArray = new double[tempArray.Length];
            if (parsedArray.Length<2)
            {
                ValidationMessage = arraySizeTemplate;
                return null;
            }

            List<int> errorPositions = new List<int>();
            bool inputErrors = false;

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (!double.TryParse(tempArray[i], out parsedArray[i]))
                {
                    inputErrors = true;
                    //converting to count starting with 1
                    errorPositions.Add(i+1);
                }
            }

            if (inputErrors)
            {
                ValidationMessage = string.Format(errorTemplate, string.Join(", ", errorPositions));
                return null;
            }
            else
            {
                ValidationMessage = "";
                return parsedArray;
            }
        }

        public int ParseParameter(string input, int upperLimit)
        {
            int parameter = -1;

            if (!int.TryParse(input, out parameter))
            {
                ValidationMessage = parameterTemplate;
                return -1;
            }

            else if (parameter < 2 || parameter > upperLimit)
            {
                ValidationMessage = rangeTemplate;
                return -1;
            }

            else
            {
                ValidationMessage = "";
                return parameter;
            }
        }
    }
}
