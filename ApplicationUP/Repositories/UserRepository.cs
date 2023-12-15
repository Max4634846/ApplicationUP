using ApplicationUP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Dynamic;

namespace ApplicationUP.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }
        public void DeleteUser()
        {
            
        }
        public void EditUser(string newusername, string newpassword, string newemail, string newaccesslevel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET password = @password, email = @email, accesslevel = @accesslevel where username = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = newusername;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = newpassword;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = newemail;
                command.Parameters.Add("@accesslevel", SqlDbType.NVarChar).Value = newaccesslevel;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            UserName = reader[1].ToString(),
                            Password = string.Empty,
                            Email = reader[4].ToString(),
                            AccessLevel = reader["AccessLevel"].ToString()
                        };
                    }
                }
            }
            return user;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("select * from [User]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        var user = new UserModel
                        {
                            Id = Convert.ToInt32(reader["Id"]).ToString(),
                            UserName = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            AccessLevel = reader["AccessLevel"].ToString()
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public bool LoginExists(string login)
        {
        bool exists;
        using (var connection = GetConnection())
        using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where sername=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = login;

                int count = (int)command.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }

        public void CreateUser(string login, string password, string email)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [User] (username, password, email) " +
                                      "values (@username, @password, @email)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = login;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.ExecuteNonQuery();
            }
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
