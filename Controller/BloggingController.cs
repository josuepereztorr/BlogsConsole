using BlogsConsole.View;

namespace BlogsConsole.Controller
{
    public class BloggingController
    {
        private readonly BloggingContext _context;
        private static Display _display;
        
        public BloggingController(BloggingContext context)
        {
            _context = context;
            _display = new Display(_context);
        }
    }
}