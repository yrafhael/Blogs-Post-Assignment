﻿using NLog;
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
                // method to add blog
                break;
            case "3":
                // method to create post
                break;
            case "4":
                // method to display post
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