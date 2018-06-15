namespace FileReader
{
    /// <summary>
    /// Defines a security system to check whether the user has the rights to read the file
    /// </summary>
    public interface IRoleBasedSecuritySystem
    {
        /// <summary>
        /// Check if the user has the rights to read the file
        /// </summary>
        /// <param name="filePath"> the path of the file to be opened </param>
        /// <param name="role"> the role of the current user </param>
        /// <returns> true if the user has the rights to read file, otherwise false </returns>
        bool CheckRightsToReadFile(string filePath, UserRoles role);
    }

    /// <summary>
    /// Defines the different user roles
    /// </summary>
    public enum UserRoles
    {
        Administrator,
        Guest,

        //Add new roles here
    }
}