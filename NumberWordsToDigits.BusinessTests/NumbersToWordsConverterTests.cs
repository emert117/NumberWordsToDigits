using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberWordsToDigits.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NumberWordsToDigits.Business.Tests
{
    [TestClass()]
    public class NumbersToWordsConverterTests
    {
        [DataTestMethod]
        [DataRow("one", "1")]
        [DataRow("eleven", "11")]
        [DataRow("three cars", "3 cars")]
        [DataRow("two thousand", "2000")]
        [DataRow("two thousands", "2000")]
        [DataRow("two thousand five hundred twenty six", "2526")]
        [DataRow("two thousand five hundreds twenty six", "2526")]
        [DataRow("two thousand nine hundred twenty six cats in five hundred fifty homes", "2926 cats in 550 homes")]
        [DataRow("two thousand six", "2006")]
        [DataRow("four million seven", "4000007")]
        [DataRow("World population has reached eight billion on November fifteen, two thousand twenty two", "World population has reached 8000000000 on November 15, 2022")]
        [DataRow("eight billion people live on earth", "8000000000 people live on earth")]
        [DataRow("He paid twenty million for three such cars.", "He paid 20000000 for 3 such cars.")]
        [DataRow("He paid twenty millions for three such cars.", "He paid 20000000 for 3 such cars.")]
        public void ConvertNumberWordsToDigitsTest(string input, string expected)
        {
            NumbersToWordsConverter converter = new NumbersToWordsConverter();
            string actual = converter.ConvertNumberWordsToDigits(input);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConvertNumberWordsToDigits_Should_Throw_Error_For_Null_Input()
        {
            NumbersToWordsConverter converter = new NumbersToWordsConverter();
            string actual = converter.ConvertNumberWordsToDigits(null);
        }
        
        [DataTestMethod]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")]
        public void ConvertNumberWordsToDigits_Should_Not_Add_Any_Digits_If_There_Is_No_Any_NumberWords(string input, string expected)
        {
            NumbersToWordsConverter converter = new NumbersToWordsConverter();
            string actual = converter.ConvertNumberWordsToDigits(input);
            Assert.AreEqual(expected, actual);

        }
    }
}