using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogInClass;

namespace BloggyClass
{ 
   

        public class BloggyC {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=\\mac\home\documents\visual studio 2015\Projects\Bloggy\Bloggy\Blogsandsuch.mdf;Integrated Security=True");
            SqlCommand command;
            SqlDataReader reader;
            string m_password, blogName, m_userName = "", m_userInput = "";
            bool checkLoop = true, BloggyOnLoop=true; 
        //add a function that checks for "'' replaces with a * and add this function to all ReadLine areas
            private bool BlogCheck()
            {
                connection.Open();
                command = new SqlCommand("Select blog From Blogs Where blog= ('" + blogName + "')", connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    connection.Close();
                    return false;
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    Console.WriteLine("That Blog Does Not Exist");
                    Console.WriteLine();
                    return true;
                }
            }
            private void AddBlog()
            {
                Console.WriteLine("Ok! What do you want to name your blog?");
                blogName = Console.ReadLine();
                Console.WriteLine("And What do you want the body of your blog to be?");
                string body = Console.ReadLine();

                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Blogs (Blog, UserID, Body) VALUES ('" + blogName + "','" + m_password + "','" + body + "')", connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                Console.WriteLine("Your Bloggy has been saved");
            }
            private void AddComment()
            {
                Console.WriteLine("Ok! What is your comment?");
                string comment = Console.ReadLine();
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Comments (Comment, UserID, blogs) VALUES ('" + comment + "','" + m_password + "','" + blogName + "')", connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                connection.Close();
                Console.WriteLine("Your Comment has been saved");
            }

        private void InnerScreen()
        {
            //Console.Clear();
            Console.WriteLine("Hello " + m_userName + " what would you like to do today?");
            Console.WriteLine("'Bloggy' Create a New Blog");
            Console.WriteLine("'Chatter' Leave a Comment on a Blog");
            Console.WriteLine("'BloggyEyes' View All of Your Blogs");
            Console.WriteLine("'SocialLife' View All of Your Comments");
            Console.WriteLine("'Looky' View All Comments under a Blog");
            Console.WriteLine();
            Console.WriteLine("'S' to Close Bloggy App");
        }

        public BloggyC()
            {
                LogInScreen bloggyLogIn = new LogInScreen();
                m_password = bloggyLogIn.password;
                m_userName = bloggyLogIn.userName;
            }
            public void GetYourBloggyOn()
            {
            while (BloggyOnLoop)
            {
                InnerScreen();
                m_userInput = Console.ReadLine().ToUpper();
                switch (m_userInput)
                {
                    case "BLOGGY":
                        Console.Clear();
                        Console.WriteLine("Create a Blog");
                        AddBlog();
                        break;

                    case "CHATTER":
                        Console.Clear();
                        Console.WriteLine("Add a Comment to a Blog");
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("What Blog would you like to comment on?");
                            blogName = Console.ReadLine();
                            checkLoop = BlogCheck();
                        }
                        command = new SqlCommand("SELECT Blogs.Blog,Blogs.Body FROM BLOGS WHERE Blogs.Blog = '" + blogName + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();                    
                            while (reader.Read())
                            {
                                Console.WriteLine("Body: " + reader["Body"]);
                                Console.WriteLine();
                            }
                        reader.Close();
                        connection.Close();
                        AddComment();
                        break;

                    case "BLOGGYEYES":
                        Console.Clear();
                        command = new SqlCommand("SELECT Blogs.Blog,Blogs.Body, Users.UserName FROM BLOGS Inner Join Users on Blogs.UserID = Users.UserID WHERE Users.UserID = '" + m_password + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Your Blogs are as follows :");
                            while (reader.Read())
                            {           
                                Console.WriteLine(reader["Blog"]);
                                Console.WriteLine("Body: " + reader["Body"]);
                                //show comments under body
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Your Message Does not Exist");
                        }
                        reader.Close();
                        connection.Close();

                        break;

                    case "SOCIALLIFE":
                        Console.Clear();
                        command = new SqlCommand("SELECT Comments.comment,Comments.blogs, Users.UserName FROM Comments Inner Join Users on Comments.UserID = Users.UserID WHERE Users.UserID = '" + m_password + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Your Comments are as follows :");
                            while (reader.Read())
                            {

                                Console.WriteLine(" Blog: " + reader["blogs"]);
                                Console.WriteLine("Comments: " + reader["Comment"]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You Need to Start Being Social My Friend");
                        }
                        reader.Close();
                        connection.Close();
                        break;

                    case "LOOKY":
                        Console.Clear();
                        checkLoop = true;
                        while (checkLoop)
                        {
                            Console.WriteLine("What Blog would you like to look up?");
                            blogName = Console.ReadLine();
                            checkLoop = BlogCheck();
                        }
                        command = new SqlCommand("SELECT Comments.comment, Blogs.Blog FROM Comments Inner Join Blogs on comments.blogs = Blogs.blog WHERE Blogs.blog = '" + blogName + "'", connection);
                        connection.Open();
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Console.WriteLine(blogName + " has these comments thus far :");
                            while (reader.Read())
                            {
                                // show Blog body on top of comments
                                Console.WriteLine(reader["Comment"]);   //show users who commented
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nobody is talking on this blog");
                        }
                        reader.Close();
                        connection.Close();
                        break;
                    case "S":
                        BloggyOnLoop = false;
                        break;
                   
                }
            }
            Console.WriteLine("Have a nice day!!");
            }

        }
    }

