using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// Defines a class to decrypt data
    /// </summary>
    public interface IDecrypter
    {
        /// <summary>
        /// Defines a class that decrypts the passed text
        /// </summary>
        /// <param name="encryptedText"> the encrypted text </param>
        /// <param name="output"> contains the decrypted text if decryption succeeds, otherwise the error message</param>
        /// <returns> true if decryption succeeds, otherwise false </returns>
        bool TryDecrypt(string encryptedText, out string output);
    }
}
