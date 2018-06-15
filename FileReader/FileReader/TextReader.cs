using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// File Reader to read Text files
    /// </summary>
    public class TextReader : IFileReader
    {
        public bool TryReadFile(string filePath, out string output)
        {           
            output = string.Empty;

            if (string.IsNullOrEmpty(filePath))
            {
                output = "Error opening the file: file path must not be empty";
                return false;
            }
            try
            {
                output = System.IO.File.ReadAllText(filePath);
                return true;
            }
            catch (Exception e)
            {
                output = "Error reading the file: " + e.Message;
                return false;
            }
        }
    }
}