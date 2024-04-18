﻿using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

try
{
    Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Display all blogs");
        Console.WriteLine("2. Add a new blog");
        Console.WriteLine("3. Create a new post");
        Console.WriteLine("4. Display posts");
        Console.WriteLine("5. Exit");

    // Create and save a new Blog
    Console.Write("Enter a name for a new Blog: ");
    var name = Console.ReadLine();

    var blog = new Blog { Name = name };

    var db = new BloggingContext();
    db.AddBlog(blog);
    logger.Info("Blog added - {name}", name);

    // Display all Blogs from the database
    var query = db.Blogs.OrderBy(b => b.Name);

    Console.WriteLine("All blogs in the database:");
    foreach (var item in query)
    {
        Console.WriteLine(item.Name);
    }
}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");