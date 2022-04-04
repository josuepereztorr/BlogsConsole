using System;
using BlogsConsole.Controller;

namespace BlogsConsole
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly BloggingContext Context = new BloggingContext();
        static void Main(string[] args)
        {
            Logger.Info("Program started");
            BloggingController controller = new BloggingController(Context, Logger);
        }
    }
}