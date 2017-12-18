using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StatMethods.Parsers
{
    class DataParser
    {
        public double[] ParsedArray { get; private set; } = new double[0];
        //Additional parameter to work with array. It should be grater than 2 and smaller
        //than array.Length, see below
        public int Parameter { get { return parameter; } }
        public string ValidationMessage { get; private set; } = "";

        private int parameter;

        private static string tryAgain = $"{Environment.NewLine}Попробуйте ввести еще раз. {Environment.NewLine}";

        private string emptyTemplate = "Вместо массива данных введена пустая строка. " + tryAgain;

        private string errorTemplate = "В позиции (позициях): {0} введены данные, " +
                       Environment.NewLine + "не являющиеся числовыми либо не соответствующие диапазону. " +
                       tryAgain;

        private string parameterTemplate = "Введенный параметр не является числовым значением либо " +
                       Environment.NewLine + "не соответствует заданным условиям. " +
                       tryAgain;

        private string rangeTemplate = "Размер окна не может быть меньше 2 и больше размера массива. " +
                       tryAgain;

        public void ParseArray(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                ValidationMessage = emptyTemplate;
                return;
            }

            input = input.Replace('.', ',').Trim(' ');

            string[] tempArray = Regex.Split(input, @"\s+");
            ParsedArray = new double[tempArray.Length];

            List<int> errorPositions = new List<int>();
            bool inputErrors = false;

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (!double.TryParse(tempArray[i], out ParsedArray[i]))
                {
                    inputErrors = true;
                    //converting to count starting with 1
                    errorPositions.Add(i+1);
                }
            }

            if (inputErrors)
                ValidationMessage = string.Format(errorTemplate, string.Join(", ", errorPositions));
            else
                ValidationMessage = "";
        }

        public void ParseParameter(string input)
        {
            if (!int.TryParse(input, out parameter))
                ValidationMessage = parameterTemplate;
            else if (parameter < 2 || parameter > ParsedArray.Length)
                ValidationMessage = rangeTemplate;
            else
                ValidationMessage = "";
        }
    }
}
