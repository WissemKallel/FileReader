using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using System.Xml;

namespace FileReader.Tests
{
    [TestClass()]
    public class XMLReaderTests
    {
        /// <summary>
        /// This test expects reading XML to return false when the file path is null or empty
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadXMLEmptyPath()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.Xml.Fakes.ShimXmlDocument.AllInstances.LoadString = (@this, s) => { };
                System.Xml.Fakes.ShimXmlDocument.AllInstances.PreserveWhitespaceSetBoolean = (@this, s) => { };

                ///Act
                string content;
                var result = sut.TryReadFile("", out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading XML to return false when the file loading fails by throwing an exception
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadXMLException()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.Xml.Fakes.ShimXmlDocument.AllInstances.LoadString = (@this, s) => { throw new Exception(); };
                System.Xml.Fakes.ShimXmlDocument.AllInstances.PreserveWhitespaceSetBoolean = (@this, s) => { };

                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading XML to return true when the file reading succeeds
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulReadXML()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.Xml.Fakes.ShimXmlDocument.AllInstances.LoadString = (@this, s) => { };
                System.Xml.Fakes.ShimXmlDocument.AllInstances.PreserveWhitespaceSetBoolean = (@this, s) => { };


                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(true, result);
            }
        }

        private XMLReader sut = new XMLReader();
    }
}