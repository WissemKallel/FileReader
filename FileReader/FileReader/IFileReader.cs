using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// Defines a generic File Reader
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reads the file pointed by the supplied path
        /// </summary>
        /// <param name="filePath"> path of the file to open </param>
        /// <param name="output"> contains the file content if file reading succeeded, otherwise contains the error message </param>
        /// <returns> true if file reading succeeded, otherwise false </returns>
        bool TryReadFile(string filePath, out string output);
    }
}