using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// Decryption functionnality that can be added dynamically to the file reader
    /// </summary>
    public class DecryptionDecorator : IFileReader
    {
        private DecryptionDecorator() { }
        public DecryptionDecorator(IFileReader fileReader, IDecrypter decrypter)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            this.decrypter = decrypter ?? throw new ArgumentNullException("decrypter");
        }
        public bool TryReadFile(string filePath, out string output)
        {
            string fileContent;
            if (!fileReader.TryReadFile(filePath, out fileContent))
            {
                ///File reading failed
                output = fileContent;
                return false;
            }

            return decrypter.TryDecrypt(fileContent, out output);
        }

        private readonly IFileReader fileReader;
        private readonly IDecrypter decrypter;
    }
}