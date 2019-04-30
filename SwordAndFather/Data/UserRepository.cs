using System.Collections.Generic;
using System.Data.SqlClient;
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



                //connection.Open();
                //var insertUserCommand = connection.CreateCommand();
                //insertUserCommand.CommandText = $@"Insert into users(username, password)
                //                              Output inserted.*
                //                              Values(@username', @password)";

                //insertUserCommand.Parameters.AddWithValue("username", username); //username from our AddUser parameter.
                //insertUserCommand.Parameters.AddWithValue("password", username); 

                //var reader = insertUserCommand.ExecuteReader();

                //if (reader.Read())
                //{
                //    var insertedUsername = reader["username"].ToString();
                //    var insertedPassword = reader["password"].ToString();
                //    var insertedId = (int)reader["Id"];

                //    var newUser = new User(insertedUsername, insertedPassword) { Id = insertedId };
                //    return newUser;
                //}
            }
            throw new System.Exception("No user found");
        }



        public IEnumerable<User> GetAll()
        {
            //connect to your local host to get your database
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<User>("select username, password, id from users");
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