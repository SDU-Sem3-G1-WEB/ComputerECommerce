using DataAccess;
using ComputerECommerce.Models;

namespace Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddUser(string name, string email, int userType)
        {
            userRepository.InsertUser(name, email, userType);
        }

        public void AddUserCredentials(byte[] emailHash, byte[] passwordHash)
        {
            userRepository.InsertUserCredentials(emailHash, passwordHash);
        }

        public void AddUserType(string name, string permissions)
        {
            userRepository.InsertUserType(name, permissions);
        }

        public void UpdateUserTypeName(int userTypeId, string userTypeName)
        {
            userRepository.EditUserTypeName(userTypeId, userTypeName);
        }

        public void UpdateUserTypePermissions(int userTypeId, string userTypePermissions)
        {
            userRepository.EditUserTypePermissions(userTypeId, userTypePermissions);
        }

        public void UpdateUserName(int userId, string userName)
        {
            userRepository.EditUserName(userId, userName);
        }

        public void UpdateUserMail(int userId, string userMail)
        {
            userRepository.EditUserMail(userId, userMail);
        }

        public void UpdateUserType(int userId, string userType)
        {
            userRepository.EditUserType(userId, userType);
        }

        public void UpdateHashPass(string mailHash, string newPassHash)
        {
            userRepository.EditHashPass(mailHash, newPassHash);
        }

        public void RemoveUser(int id)
        {
            userRepository.DeleteUser(id);
        }

        public void RemoveUserCredentials(int id)
        {
            userRepository.DeleteUserCredentials(id);
        }

        public void RemoveUserType(int id)
        {
            userRepository.DeleteUserType(id);
        }

        public IUser? GetUser(string? email = null, int? userId = null)
        {
            return userRepository.GetUser(email, userId);
        }

        public IUser? GetUser(int id)
        {
            return userRepository.GetUser(null, id);
        }

        public List<IUser> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public string GetUserType(int userId)
        {
            return userRepository.GetUserType(userId)!;
        }

        public bool DoesEmailExist(string email)
        {
            return userRepository.DoesEmailExist(email);
        }
    }
}