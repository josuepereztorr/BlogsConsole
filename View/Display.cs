using System;
using System.Linq;
using NLog;

namespace BlogsConsole.View
{
    public class Display
    {
        private static BloggingContext _context;
        private static Logger _logger;
        
        private static Blog Blog { get; set; }
        private static Post Post { get; set; }

        private static string _input;
        
        private const string AllBlog = "ALLBLOGS";
        private const string AllPost = "ALLPOSTS";
        private const string AllPostsFromBlog = "ALLPOSTSFROMBLOG";
        
        public Display(BloggingContext context, Logger logger)
        {
            _context = context;
            _logger = logger;
        }
        
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
        
        private static void ShowAllBlogs()
        {
            _logger.Info("Option '1' selected");
            
            Console.WriteLine($"{_context.Blogs.Count()} Blogs returned");
            ShowListOfEntitiesByType(AllBlog);
            Console.WriteLine();
        }
        
        private static void ShowAddBlog()
        {
            _logger.Info("Option '2' selected");
            Console.WriteLine("Enter a name for a new Blog: ");
            _input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(_input))
            {
                Blog.Name = _input;
            }
            else
            {
                _logger.Info("Blog name cannot be null");
            }
        }
        
        private static void ShowCreatePost()
        {
            if (!_context.Blogs.Any())
            {
                Console.Clear();
                _logger.Info("Option '3' selected");
                Console.WriteLine("Select the Blog you would like to post to: ");
                ShowListOfEntitiesByType(AllBlog, true);
                
                _input = Console.ReadLine();
                
                if (int.TryParse(_input, out int blogId))
                {
                    if (_context.Blogs.Any(blog => blog.BlogId.Equals(blogId)))
                    {
                        Console.WriteLine("Enter the Post title: ");
                        
                        _input = Console.ReadLine();
                        
                        if (!string.IsNullOrWhiteSpace(_input))
                        {
                            Post.Title = _input;
                            
                            Console.WriteLine("Enter the Post content"); 
                            _input = Console.ReadLine();

                            Post.Content = _input;
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
                _logger.Info("No Blogs found");
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
            
            ShowListOfEntitiesByType(AllPostsFromBlog, true);

            _input = Console.ReadLine();
            
            if (int.TryParse(_input, out int blogId))
            {
                if (_context.Blogs.Any(blog => blog.BlogId.Equals(blogId)))
                {
                    if (blogId.Equals(0))
                    {
                        ShowListOfEntitiesByType(AllPost);
                    }
                    ShowListOfEntitiesByType(AllPost, id: blogId);
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
        
        private static void ShowListOfEntitiesByType(string entityType, bool showId = false, int id = 0)
        {
            int index = 0;
            
            switch (entityType)
            {
                case AllBlog:
                {
                    foreach (var blog in _context.Blogs)
                    {
                        Console.WriteLine(showId ? $"{index++}) {blog.Name}" : blog.Name);
                    }
                    
                    break;
                }
                case AllPost:
                {
                    IQueryable<Post> query = _context.Posts.Where(post => post.BlogId.Equals(id));
                    Console.WriteLine($"{query.Count()} item(s) returned");
                    Console.WriteLine();
                    foreach (var post in query)
                    {
                        Console.WriteLine(post.ToString());
                        Console.WriteLine();
                    }
                    break;
                }
                case AllPostsFromBlog:
                {
                    foreach (var blog in _context.Blogs)
                    {
                        Console.WriteLine($"{index++}) Post from {blog.Name}");;
                    }

                    break;
                }
            }
        }
    }
}