using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// A simplistic implementation of the role-based security system
    /// </summary>
    public class DummySecuritySystem : IRoleBasedSecuritySystem
    {
        public bool CheckRightsToReadFile(string filePath, UserRoles role)
        {
            if (role == UserRoles.Administrator)
                ///Admin has the right to read any file
                return true;
            else
            {
                ///Guests are not allowed to read any file
                return false;
            }
        }
    }
}
