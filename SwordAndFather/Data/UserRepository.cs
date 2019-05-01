using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";
        public User AddUser(string username, string password)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newUser = db.QueryFirstOrDefault<User>(
                    @"Insert into users(username, password)
                    Output inserted.*
                    Values(@username', @password)", 
                    new {username, password}); //automatically sets the property with the same name
                                               //it's an anonymous type passing in an object. but you don't have to create an anonymous one. 

                if (newUser != null)
                {
                    return newUser;
                }
            }
            throw new System.Exception("No user found");
        }

        public void DeleteUser(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute("Delete From Users where Id = @id", new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("Didn't do right.");
                }
            }
        }
        //number of rows affected
        //if you don't define the parameter, it won't have a value. 

        public User UpdateUser(User userToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var rowsAffected = db.Execute(@"Update Users 
                            Set username = @username,
                                password = @password
                            Where id = @id", userToUpdate);

                if (rowsAffected == 1)
                    return userToUpdate;
            }
            throw new Exception("Could not update user");
        }

        public IEnumerable<User> GetAll()
        {
            //connect to your local host to get your database
            using (var db = new SqlConnection(ConnectionString))
            {
                var users = db.Query<User>("select username, password, id from users").ToList();
                var targets = db.Query<Target>("select * from Targets").ToList();

                foreach (var user in users)
                {
                    user.Targets = targets.Where(target => target.UserId == user.Id).ToList();
                }
                return users;
            }
        }
    }
}

//create the command that gets all users: all the information you want from the user
//var getAllUsersCommand = connection.CreateCommand();
//getAllUsersCommand.CommandText = @"select username, password, id from users";

////send command thru connection
//var reader = getAllUsersCommand.ExecuteReader();

////read the results, map it to a type and repository list
//while (reader.Read())//direct cast
//{
//    var id = (int)reader["Id"];
//    var username = reader["username"].ToString();
//    var password = reader["password"].ToString();
//    var user = new User(username, password) { Id = id };

//    users.Add(user);
//}

//close the conection

//return the list