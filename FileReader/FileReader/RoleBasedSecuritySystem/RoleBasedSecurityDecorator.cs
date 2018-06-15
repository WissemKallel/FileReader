using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    /// <summary>
    /// Role-based security checking functionnality that can be added dynamically to the file reader
    /// </summary>
    public class RoleBasedSecurityDecorator : IFileReader
    {
        private RoleBasedSecurityDecorator() { }
        public RoleBasedSecurityDecorator(IFileReader fileReader, IRoleBasedSecuritySystem securitySystem, UserRoles userRole)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException("fileReader");
            this.securitySystem = securitySystem ?? throw new ArgumentNullException("securitySystem");
            this.userRole = userRole;
        }
        public bool TryReadFile(string filePath, out string fileContent)
        {
            if (!securitySystem.CheckRightsToReadFile(filePath, userRole))
            {
                fileContent = "Error: User is not allowed to read the file";
                return false;
            }

            return fileReader.TryReadFile(filePath, out fileContent);
        }

        private IFileReader fileReader;
        private IRoleBasedSecuritySystem securitySystem;
        private UserRoles userRole;
    }
}
