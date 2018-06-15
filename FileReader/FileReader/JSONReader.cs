using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// File Reader to read JSON files
    /// </summary>
    public class JSONReader : IFileReader
    {
        public bool TryReadFile(string filePath, out string fileContent)
        {
            fileContent = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    fileContent = "Error reading the file: File path null or empty";
                    return false;
                }

                using (StreamReader r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    fileContent = jobj.ToString();
                }
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