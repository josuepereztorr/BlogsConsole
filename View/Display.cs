using System;
using System.Collections.Generic;

namespace BlogsConsole.View
{
    public class Display
    {
        private void ShowMenu()
        {
            Console.WriteLine("1) Display all blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Create Post");
            Console.WriteLine("4) Display Posts");
            Console.WriteLine("Enter q to quit");
        }

        private void ShowBlogs(List<Blog> blogs)
        {
            Console.WriteLine($"{blogs.Count} Blogs returned");
            
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.Name);
            }
        }
        
    }
}