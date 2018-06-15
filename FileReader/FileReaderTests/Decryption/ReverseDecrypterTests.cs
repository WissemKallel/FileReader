using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;

namespace FileReader.Tests
{
    [TestClass()]
    public class ReverseDecrypterTests
    {
        /// <summary>
        /// This test expects decrypting text to return true and the content to be reversed
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulDecryptFile()
        {
            using (ShimsContext.Create())
            {
                ///Arrange

                ///Act
                string decrypted;
                var result = sut.TryDecrypt("test", out decrypted);

                ///Assert
                Assert.AreEqual(true, result);
                Assert.AreEqual("tset", decrypted);
            }
        }

        /// <summary>
        /// This test expects decrypting text to return true when passed string empty
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulDecryptEmptyString()
        {
            using (ShimsContext.Create())
            {
                ///Arrange

                ///Act
                string content;
                var result = sut.TryDecrypt("", out content);

                ///Assert
                Assert.AreEqual(true, result);
            }
        }

        /// <summary>
        /// This test expects decrypt text to throw ArgumentNullException when null string passed
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUnSuccessfulDecryptNullArgument()
        {
            using (ShimsContext.Create())
            {
                ///Arrange

                ///Act
                string content;
                var result = sut.TryDecrypt(null, out content);

                ///Assert

            }
        }

        private IDecrypter sut = new ReverseDecrypter();
    }
}