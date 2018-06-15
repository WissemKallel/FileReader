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
    public class RoleBasedReadingTests
    {
        [TestInitialize()]
        public void Setup()
        {
            var role = default(UserRoles);
            fakeSecuritySystem = new Fakes.StubIRoleBasedSecuritySystem()
            {
                CheckRightsToReadFileStringUserRoles = (p, r) =>
                {
                    return true;
                }
            };

            fakeFileReader = new Fakes.StubIFileReader()
            {
                TryReadFileStringStringOut = (string path, out string output) =>
                {
                    output = "test";
                    return true;
                }
            };

            sut = new RoleBasedSecurityDecorator(fakeFileReader, fakeSecuritySystem, role);
        }

        /// <summary>
        /// This test expects reading file in a role-based security context to return false when security is denied
        /// </summary>
        [TestMethod()]
        public void TestUnsuccessfulReadFileRoleBasedSecurityDenied()
        {
            ///Arrange

            ///Act
            string content;
            fakeSecuritySystem.CheckRightsToReadFileStringUserRoles = (p, r) =>
            {
                return false;
            };

            var result = sut.TryReadFile("path", out content);

            ///Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// This test expects reading text in a role-based security context to return true when the file reading succeeds
        /// </summary>
        [TestMethod()]
        public void TestSuccessfulReadFiletRoleBasedSecurity()
        {
            ///Arrange

            ///Act
            string content;
            var result = sut.TryReadFile("path", out content);

            ///Assert
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// This test expects reading encypted file to return false when reading fails
        /// </summary>
        [TestMethod()]
        public void TestUnSuccessfulReadFiletRoleBasedSecurityReadingFails()
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

        private RoleBasedSecurityDecorator sut;
        private Fakes.StubIFileReader fakeFileReader;
        private Fakes.StubIRoleBasedSecuritySystem fakeSecuritySystem;
    }
}