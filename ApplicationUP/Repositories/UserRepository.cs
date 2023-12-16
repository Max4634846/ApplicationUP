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
using System.Windows.Input;

namespace ApplicationUP.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [UsersTrain] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }
        public void DeleteUsername(int userId)
        {
            SqlConnection connection = new SqlConnection("Data Source=ONLYUP;Initial Catalog=MVVMLoginDb;Integrated Security=True");
            string cmd = "DELETE FROM [UsersTrain] WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(cmd, connection);
            deleteCommand.Parameters.AddWithValue("@Id", userId);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void EditUser(string newusername, string newpassword, string newemail, string newaccesslevel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [UsersTrain] SET Password = @Password, Email = @Email, AccessLevel = @AccessLevel where Username = @Username";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = newusername;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = newpassword;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = newemail;
                command.Parameters.Add("@AccessLevel", SqlDbType.NVarChar).Value = newaccesslevel;
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
                command.CommandText = "select *from [UsersTrain] where username=@username";
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
                            Email = reader[3].ToString(),
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
            using (var command = new SqlCommand("select * from [UsersTrain]", connection))
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

        public void CreateUser(string login, string password, string email)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [UsersTrain] (Username , Password, Email) " +
                                      "values (@Username, @Password, @Email)";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                command.ExecuteNonQuery();
            }
        }
        public void Add(string login, string password, string email, string accesslevel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [UsersTrain] (Username , Password, Email, AccessLevel) " +
                                      "values (@Username, @Password, @Email, @AccessLevel)";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = login;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@AccessLevel", SqlDbType.NVarChar).Value = accesslevel;
                command.ExecuteNonQuery();
            }
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
