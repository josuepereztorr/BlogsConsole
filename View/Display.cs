using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlogsConsole.Controller;
using NLog.Web;

namespace BlogsConsole.View
{
    public class Display
    {
        private static BloggingContext _context;

        public Display(BloggingContext context)
        {
            _context = context;
        }

    }
}