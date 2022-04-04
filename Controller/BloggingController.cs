using BlogsConsole.View;

namespace BlogsConsole.Controller
{
    public class BloggingController
    {
        private readonly BloggingContext _context;
        private static Display _display;
        private static NLog.Logger _logger;
        
        public BloggingController(BloggingContext context, NLog.Logger logger)
        {
            _context = context;
            _display = new Display(_context, logger);
            _logger = logger;
        }
    }
}