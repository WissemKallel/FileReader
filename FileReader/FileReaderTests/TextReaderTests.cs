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
    public class TextReaderTests
    {
        /// <summary>
        /// This test expects reading text to return false when the file path is empty
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadTextFilePathEmpty()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimFile.ReadAllTextString = (t) => { return "test"; };

                ///Act
                string content;
                var result = sut.TryReadFile(null, out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading text to return false when the file reading fails by throwing an exception
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadTextException()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimFile.ReadAllTextString = (t) => { throw new Exception(); };

                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading text to return true when the file reading succeeds
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulReadText()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimFile.ReadAllTextString = (t) => { return "test"; };

                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(true, result);
            }
        }

        private TextReader sut = new TextReader();
    }
}