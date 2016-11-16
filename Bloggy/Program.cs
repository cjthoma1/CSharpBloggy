using BloggyClass;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bloggy
{
    class Program
    {
       
        static void Main(string[] args)
        {   //Create a blog from the BloggyC class
            BloggyC blog = new BloggyC();
            //Run the GetYourBloggyOn() Function
            blog.GetYourBloggyOn();
           
        }

    }
}
