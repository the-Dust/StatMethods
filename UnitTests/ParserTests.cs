using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatMethods.Parsers;


namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void CanParseValidArray()
        {
            //Arrange
            DataParser parser = new DataParser();

            string inputString = "  34  2,41 4.936 23 300    50    ";
            double[] reference = { 34, 2.41, 4.936, 23, 300, 50};

            //Action
            double[] parsedArray = parser.ParseArray(inputString);

            //Assert
            CollectionAssert.AreEqual(reference, parsedArray);
            Assert.AreEqual("", parser.ValidationMessage);
        }
        
        [TestMethod]
        public void GiveMessageWithInvalidInput()
        {
            //Arrange
            DataParser parser = new DataParser();

            string inputString1 = "  текст  2,41 ?;№ 23 300    50    ";
            string inputString2 = "    ";
            string inputString3 = null;
            string inputString4 = "111";

            string refMessage1 = "В позиции (позициях): 1, 3 введены данные, " +
                       Environment.NewLine + "не являющиеся числовыми либо не соответствующие диапазону. ";
            string refMessage2 = "Вместо массива данных введена пустая строка. ";

            string refMessage3 = "Вместо массива данных введена пустая строка. ";

            string refMessage4 = "Массив не может состоять из одного элемента. ";

            //Action
            double[] parsedArray1 = parser.ParseArray(inputString1);
            string message1 = parser.ValidationMessage;

            double[] parsedArray2 = parser.ParseArray(inputString2);
            string message2 = parser.ValidationMessage;

            double[] parsedArray3 = parser.ParseArray(inputString3);
            string message3 = parser.ValidationMessage;

            double[] parsedArray4 = parser.ParseArray(inputString4);
            string message4 = parser.ValidationMessage;

            //Assert
            CollectionAssert.AreEqual(null, parsedArray1);
            Assert.IsTrue(message1.Contains(refMessage1));
            
            CollectionAssert.AreEqual(null, parsedArray2);
            Assert.IsTrue(message2.Contains(refMessage2));

            CollectionAssert.AreEqual(null, parsedArray3);
            Assert.IsTrue(message3.Contains(refMessage3));

            CollectionAssert.AreEqual(null, parsedArray4);
            Assert.IsTrue(message4.Contains(refMessage4));
        }

        [TestMethod]
        public void CanParseValidParameter()
        {
            //Arrange
            DataParser parser = new DataParser();

            string inputString = "4";
            int reference = 4;

            //Action
            int parsedParameter = parser.ParseParameter(inputString, 10);

            //Assert
            Assert.AreEqual(reference, parsedParameter);
            Assert.AreEqual("", parser.ValidationMessage);
        }

        [TestMethod]
        public void GiveMessageWithInvalidParameter()
        {
            //Arrange
            DataParser parser = new DataParser();

            string inputString1 = "%$#4";
            int reference = -1;
            string refMessage1 = "Введенный параметр не является числовым значением";

            string inputString2 = "1";
            
            string refMessage2 = "Размер параметра не может быть меньше 2 и больше размера массива. ";

            string inputString3 = "20";
            
            string refMessage3 = "Размер параметра не может быть меньше 2 и больше размера массива. ";

            //Action
            int parsedParameter1 = parser.ParseParameter(inputString1, 10);
            string message1 = parser.ValidationMessage;

            int parsedParameter2 = parser.ParseParameter(inputString2, 10);
            string message2 = parser.ValidationMessage;

            int parsedParameter3 = parser.ParseParameter(inputString3, 10);
            string message3 = parser.ValidationMessage;


            //Assert
            Assert.AreEqual(reference, parsedParameter1);
            Assert.IsTrue(message1.Contains(refMessage1));

            Assert.AreEqual(reference, parsedParameter2);
            Assert.IsTrue(message2.Contains(refMessage2));

            Assert.AreEqual(reference, parsedParameter3);
            Assert.IsTrue(message3.Contains(refMessage3));
        }
    }
}
