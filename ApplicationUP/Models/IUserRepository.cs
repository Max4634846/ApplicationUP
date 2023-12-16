using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUP.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(string login, string password, string email, string accesslevel);
        void CreateUser(string login, string password, string email);
        void EditUser(string newusername, string newpassword, string newemail, string newaccesslevel);
        void DeleteUsername(int userId);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUsername(string username);
        IEnumerable<UserModel> GetByAll();
    }
}
