using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// Text decrypter that simply reverses the text
    /// </summary>
    public class ReverseDecrypter : IDecrypter
    {
        public bool TryDecrypt(string encryptedText, out string decryptedText)
        {
            if (encryptedText == null) throw new ArgumentNullException("encryptedText");

            if (encryptedText == string.Empty)
            {
                ///Empty text, nothing to decrypt
                decryptedText = "";
                return true;
            }

            char[] charArray = encryptedText.ToCharArray();
            Array.Reverse(charArray);
            decryptedText = new string(charArray);
            return true;
        }
    }
}
