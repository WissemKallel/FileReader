using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileReader
{
    /// <summary>
    /// File Reader to read XML files
    /// </summary>
    public class XMLReader : IFileReader
    {
        public bool TryReadFile(string filePath, out string fileContent)
        {
            fileContent = "";

            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    fileContent = "Error reading the file: File path null or empty";
                    return false;
                }

                ///Load the xml file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load(filePath);
                fileContent = xmlDoc.OuterXml;
                return true;
            }
            catch (Exception e)
            {
                fileContent = "Error reading the file: " + e.Message;
                return false;
            }
        }
    }
}