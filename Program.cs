using System;
using BlogsConsole.Controller;

namespace BlogsConsole
{
    class Program
    {
        private static readonly BloggingContext Context = new BloggingContext();
        static void Main(string[] args)
        {
            BloggingController controller = new BloggingController(Context);
        }
    }
}