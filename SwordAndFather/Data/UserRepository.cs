using System.Collections.Generic;
using System.Data.SqlClient;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";
        public User AddUser(string username, string password)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
            connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into users(username, password)
                                              Output inserted.*
                                              Values(@username', @password)";

                insertUserCommand.Parameters.AddWithValue("username", username); //username from our AddUser parameter.
                insertUserCommand.Parameters.AddWithValue("password", username);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUsername = reader["username"].ToString();
                    var insertedPassword = reader["password"].ToString();
                    var insertedId = (int)reader["Id"];

                    var newUser = new User(insertedUsername, insertedPassword) { Id = insertedId };
                    return newUser;
                }
            }
            throw new System.Exception("No user found");
        }
        


        public List<User> GetAll()
        {
            //create a list of users
            var users = new List<User>();

            //connect to your local host to get your database
            var connection = new SqlConnection("Server=localhost;Database=SwordAndFather;Trusted_Connection=True;");
            connection.Open();

            //create the command that gets all users: all the information you want from the user
            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select username, password, id from users";

            //send command thru connection
            var reader = getAllUsersCommand.ExecuteReader();

            //read the results, map it to a type and repository list
            while (reader.Read())//direct cast
            {
                var id = (int)reader["Id"];
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var user = new User(username, password) { Id = id };

                users.Add(user);
            }

            //close the conection
            connection.Close();

            //return the list
            return users;
        }
    }
}