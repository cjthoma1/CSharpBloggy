using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInClass
{
    public class LogInScreen
    {               /*------- Opened Database Connection */
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=\\mac\home\documents\visual studio 2015\Projects\Bloggy\Bloggy\Blogsandsuch.mdf;Integrated Security=True");
        SqlCommand command;
        SqlDataReader reader;
        public string password { get; set; }
        public string userName { get; set; }
        string userInput = "";
        bool checkLoop = true, welcomeScreenLoop = true;

        private void AddNewUser()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Users (UserName, UserID) VALUES ('" + userName + "','" + password + "')", connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Close();
            connection.Close();
            Console.WriteLine("User Name and Password have been saved");
        }

        private bool PasswordCheckForNewUsers()
        {
            connection.Open();
            command = new SqlCommand("Select UserID From Users Where UserID= ('" + password + "')", connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("You need to come up with a more unique password");
                Console.WriteLine();
                return true;
            }
            else
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("If it works for you it works for me!!");
                Console.WriteLine();
                return false;
            }
        }

        private bool UserNameCheckForNewUsers()
        {
            connection.Open();
            command = new SqlCommand("Select UserName From Users Where UserName= ('" + userName + "')", connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("Up Looks Like Somebody Else Has That User Name");
                return true;
            }
            else
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("Lets get you all signed up!!");
                Console.WriteLine();
                return false;
            }
        }

        private bool PasswordCheck()
        {
            connection.Open();
            command = new SqlCommand("Select UserID From Users Where UserID= ('" + password + "')", connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("LETS DO THIS!!!");
                return false;
            }
            else
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("I don't think that's correct");
                return true;
            }
        }

        private bool UserNameCheck()
        {
            connection.Open();
            command = new SqlCommand("Select UserName From Users Where UserName= ('" + userName + "')", connection);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("Welcome Back " + userName);
                return false;
            }
            else
            {
                reader.Close();
                connection.Close();
                Console.WriteLine("Are you sure you're a Bloggy???");
                return true;
            }
        }

        private void WelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine("Welcome to BLOGGY");
            Console.WriteLine();
            Console.WriteLine("Type 'Newy' to Create New Profile");
            Console.WriteLine("Type 'Bloggest' to Log In");
            Console.WriteLine();
        }
        public LogInScreen()
        {
            while (welcomeScreenLoop)
            {
                WelcomeScreen();
                Console.WriteLine("So what would you like to do?");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "NEWY":
                        Console.Clear();
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("Create a User Name!!!");
                            userName = Console.ReadLine();
                            checkLoop = UserNameCheckForNewUsers();
                        }
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("Lets Create a Password");
                            password = Console.ReadLine();
                            checkLoop = PasswordCheckForNewUsers();
                        }
                        AddNewUser();
                        welcomeScreenLoop = false;
                        break;
                    case "BLOGGEST":
                        Console.Clear();
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("What's Your User Name?");
                            userName = Console.ReadLine();
                            checkLoop = UserNameCheck();
                        }
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("What's Your Password?");
                            password = Console.ReadLine();
                            checkLoop = PasswordCheck();
                        }
                        welcomeScreenLoop = false;
                        break;

                }
            }
        }
    }





}
