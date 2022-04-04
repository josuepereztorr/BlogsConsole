using System;
using System.Linq;
using NLog;

namespace BlogsConsole.View
{
    public class Display
    {
        private static BloggingContext _context;
        private static Logger _logger;
        
        private static string BlogId { get; set; }
        private static string BlogName { get; set; }
        private static string PostTitle { get; set; }
        private static string PostContent { get; set; }
        
        private static string _input;
        
        private const string AllBlogs = "ALLBLOGS";
        private const string AllPost = "ALLPOSTS";
        private const string ById = "ID";

        public Display(BloggingContext context, Logger logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // Shows list of menu options
        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Enter your selection:");
            Console.WriteLine("1) Display all blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Create Post");
            Console.WriteLine("4) Display Posts");
            Console.WriteLine("Enter q to quit");
        }
        
        // Shows a list of blogs in the database by id 
        private static void ShowAllBlogs()
        {
            _logger.Info("Option '1' selected");
            Console.WriteLine($"{_context.Blogs.Count()} Blogs returned");
            ShowTypeById(false, AllBlogs);
            Console.WriteLine();
        }
        
        // Shows prompt to enter new Blog name
        private static void ShowAddBlog()
        {
            _logger.Info("Option '2' selected");
            Console.WriteLine("Enter a name for a new Blog: ");
            _input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(_input))
            {
                BlogName = _input;
            }
            else
            {
                _logger.Info("blog name cannot be null");
            }
        }
        
        // Shows a list of blogs in the database to choose from to create a post in, then it prompts the user to enter a title 
        private static void ShowCreatePost()
        {
            if (!_context.Blogs.Any())
            {
                Console.Clear();
                _logger.Info("Option '3' selected");
                Console.WriteLine("Select the Blog you would like to post to: ");
                ShowTypeById(true, AllBlogs);
                
                _input = Console.ReadLine();
                
                // check if BlogId is an integer
                if (int.TryParse(_input, out int blogId))
                {
                    // check if id is in database 
                    if (_context.Blogs.Any(blog => blogId.Equals(blogId)))
                    {
                        // prompt user for PostTitle, PostContent
                        Console.WriteLine("Enter the Post title: ");
                        _input = Console.ReadLine();
                        
                        if (!string.IsNullOrWhiteSpace(_input))
                        {
                            PostTitle = _input;
                            
                            // prompt user for Post.Content
                            Console.WriteLine("Enter the Post content"); 
                            _input = Console.ReadLine();

                            PostContent = _input;
                        }
                        else
                        {
                            _logger.Info("Post title cannot be null");
                        }

                    }
                    else
                    {
                        _logger.Info("There are no Blogs saved with that id");
                    }
                }
                else
                {
                    _logger.Info("Invalid Blog Id");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Cannot create post, there must be at least one blog created to create a new post");
                Console.WriteLine();
                Console.WriteLine("Press enter to return to the main menu...");
                Console.ReadLine();
            }
        }
        
        private static void ShowPosts()
        {
            Console.Clear();
            _logger.Info("Option '4' selected");
            Console.WriteLine("Select the Blog's posts to display: ");
            Console.WriteLine("0) Posts from all blogs");

            
        }
        
        // Utility Methods
        private static void ShowTypeById(bool showId, string selection, int id = 0)
        {
            switch (selection)
            {
                case AllBlogs:
                {
                    var index = 0;
                    foreach (var blog in _context.Blogs)
                    {
                        Console.WriteLine(showId ? $"{index++}) {blog.Name}" : blog.Name);
                    }
                    
                    break;
                }
                case AllPost:
                {
                    ShowQueryById(_context.Posts);
                    break;
                }
                case ById:
                    ShowQueryById(_context.Blogs.Where(blog => blog.BlogId.Equals(id)));
                    break;
            }
        }

        private static void ShowAllByType(IQueryable<BloggingContext> context, string type, bool showId)
        {
            var index = 0;

            Console.WriteLine($"{context.Count()} item(s) returned");
            
            foreach (var item in context)
            {
                switch (type)
                {
                    case AllBlogs:
                        Console.WriteLine(showId ? $"{index++}) {item}" : item);
                        break;
                    case AllPost:
                        Console.WriteLine(showId ? $"{index++}) from {item}" : item);
                        break;
                }
            }
        }

        private static void ShowQueryById<T>(IQueryable<T> contextQuery)
        {
            Console.WriteLine($"{contextQuery.Count()} item(s) returned");
            foreach (var item in contextQuery)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine();
            }
        }
        
    }
}