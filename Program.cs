﻿﻿using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

try
{
    var db = new BloggingContext();

    while (true)
    {
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Display all blogs");
        Console.WriteLine("2. Add a new blog");
        Console.WriteLine("3. Create a new post");
        Console.WriteLine("4. Display posts");
        Console.WriteLine("5. Exit");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                DisplayAllBlogs(db);
                break;
            case "2":
                AddBlog(db);
                break;
            case "3":
                CreatePost(db);
                break;
            case "4":
                DisplayPosts(db);
                break;
            case "5":
                logger.Info("Program ended");
                return;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

void DisplayAllBlogs(BloggingContext db)
{
    var query = db.Blogs.OrderBy(b => b.Name);

    Console.WriteLine("All blogs in the database:");
    foreach (var item in query)
    {
        Console.WriteLine(item.Name);
    }
}

void AddBlog(BloggingContext db)
{
    Console.Write("Enter a name for a new Blog: ");
    var name = Console.ReadLine();

    var blog = new Blog { Name = name };
    db.AddBlog(blog);
    logger.Info("Blog added - {name}", name);
}

void CreatePost(BloggingContext db)
{
    // Display all blogs
    var blogs = db.Blogs.ToList();
    Console.WriteLine("Select a blog to post:");
    for (int i = 0; i < blogs.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {blogs[i].Name}");
    }

    // Get user input for blog selection
    int blogIndex;
    while (!int.TryParse(Console.ReadLine(), out blogIndex) || blogIndex < 1 || blogIndex > blogs.Count)
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
    var selectedBlog = blogs[blogIndex - 1];

    // Get post details from user
    Console.Write("Enter post title: ");
    var title = Console.ReadLine();
    Console.Write("Enter post content: ");
    var content = Console.ReadLine();

    // Create and save the post
    var post = new Post { Title = title, Content = content, BlogId = selectedBlog.BlogId };
    db.Posts.Add(post);
    db.SaveChanges();
    logger.Info("Post added - {title}", title);
}

void DisplayPosts(BloggingContext db)
{
    // Display all blogs
    var blogs = db.Blogs.ToList();
    Console.WriteLine("Select a blog to view posts:");
    for (int i = 0; i < blogs.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {blogs[i].Name}");
    }

    // Get user input for blog selection
    int blogIndex;
    while (!int.TryParse(Console.ReadLine(), out blogIndex) || blogIndex < 1 || blogIndex > blogs.Count)
    {
        Console.WriteLine("Invalid input. Please try again.");
    }
    var selectedBlog = blogs[blogIndex - 1];

    // Display posts for the selected blog
    var posts = db.Posts.Where(p => p.BlogId == selectedBlog.BlogId).ToList();
    Console.WriteLine($"Posts for {selectedBlog.Name} ({posts.Count}):");
    foreach (var post in posts)
    {
        Console.WriteLine($"- {post.Title} ({post.Blog.Name})");
        Console.WriteLine(post.Content);
        Console.WriteLine();
    }
}