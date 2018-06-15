using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Tests
{
    [TestClass()]
    public class DecryptionDecoratorTests
    {
        [TestInitialize()]
        public void Setup()
        {
            fakeDecrypter = new Fakes.StubIDecrypter()
            {
                TryDecryptStringStringOut = (string encrypted, out string decrypted) =>
                {
                    decrypted = "";
                    return true;
                },
            };

            fakeFileReader = new Fakes.StubIFileReader()
            {
                TryReadFileStringStringOut = (string path, out string output) =>
                {
                    output = "test";
                    return true;
                }
            };

            sut = new DecryptionDecorator(fakeFileReader, fakeDecrypter);
        }

        /// <summary>
        /// This test expects reading encypted file to return true when succeeding
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulReadEncryptedFile()
        {
            ///Arrange

            ///Act
            string content;
            var result = sut.TryReadFile("path", out content);

            ///Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// This test expects reading encypted file to return false when decryption fails
        /// </summary>
        [TestMethod()]
        public void TestUnSuccessfulReadEncryptedFileDecryptionFails()
        {
            ///Arrange
            fakeDecrypter.TryDecryptStringStringOut = (string encrypted, out string decrypted) =>
            {
                decrypted = "";
                return false;
            };

            ///Act
            string content;
            var result = sut.TryReadFile("path", out content);

            ///Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// This test expects reading encypted file to return false when reading fails
        /// </summary>
        [TestMethod()]
        public void TestUnSuccessfulTryReadEncryptedFileReadingFails()
        {
            ///Arrange
            fakeFileReader.TryReadFileStringStringOut = (string path, out string output) =>
            {
                output = "";
                return false;
            };

            ///Act
            string content;
            var result = sut.TryReadFile("path", out content);

            ///Assert
            Assert.AreEqual(false, result);
        }

        private DecryptionDecorator sut;
        private Fakes.StubIDecrypter fakeDecrypter;
        private Fakes.StubIFileReader fakeFileReader;
    }
}