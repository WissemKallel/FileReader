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
    public class JSONReaderTests
    {
        /// <summary>
        /// This test expects reading JSON file to return false when the file path is empty
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadJSONFilePathEmpty()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimStreamReader.ConstructorString = (@this, t) => { };
                System.IO.Fakes.ShimStreamReader.AllInstances.ReadToEnd = (t) => { return "test"; };
                Newtonsoft.Json.Linq.Fakes.ShimJObject.ParseString = (st) => { return new Newtonsoft.Json.Linq.Fakes.ShimJObject(); };

                ///Act
                string content;
                var result = sut.TryReadFile(null, out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading JSON file to return false when the file reading fails by throwing an exception
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadJSONException()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimStreamReader.ConstructorString = (@this, t) => { };
                System.IO.Fakes.ShimStreamReader.AllInstances.ReadToEnd = (t) => { throw new Exception(); };
                Newtonsoft.Json.Linq.Fakes.ShimJObject.ParseString = (st) => { return new Newtonsoft.Json.Linq.Fakes.ShimJObject(); };

                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(false, result);
            }
        }

        /// <summary>
        /// This test expects reading JSON file to return true when the file reading succeeds
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulReadJSON()
        {
            using (ShimsContext.Create())
            {
                ///Arrange
                System.IO.Fakes.ShimStreamReader.ConstructorString = (@this, t) => { };
                System.IO.Fakes.ShimStreamReader.AllInstances.ReadToEnd = (t) => { return "test"; };
                Newtonsoft.Json.Linq.Fakes.ShimJObject.ParseString = (st) => { return new Newtonsoft.Json.Linq.Fakes.ShimJObject(); };

                ///Act
                string content;
                var result = sut.TryReadFile("path", out content);

                ///Assert
                Assert.AreEqual(true, result);
            }
        }

        private JSONReader sut = new JSONReader();
    }
}