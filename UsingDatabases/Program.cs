using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UsingDatabases
{
    class Program
    {
        private static void Main(string[] args)
        {

            Console.WriteLine("1.Create Blog\n2.CreatePost\n3.List All blogs\n4.List all posts\n0.End\nPlease enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        CreateBlog();
                        break;
                    case 2:
                        CreatePost();
                        break;
                    case 3:
                        ListAllBlogs();
                        break;
                    case 4:
                        ListAllPost();
                        break;
                }
                Console.WriteLine(
                    "1.Create Blog\n2.CreatePost\n3.List All blogs\n4.List all posts\n0.End\nPlease enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ListAllPost()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Posts
                            orderby b.BlogId
                            select b;

                foreach (var post in query)
                {
                    Console.WriteLine(post.Title);
                    Console.WriteLine(post.Content);
                }

                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }

        }

        private static void ListAllBlogs()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Blogs
                            orderby b.BlogId
                            select b;

                foreach (var blog in query)
                {
                    Console.WriteLine(blog.Name);
                    Console.WriteLine(blog.Url);
                }

                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }

        private static void CreatePost()
        {
            using (var db = new BloggingContext())
            {
                Console.WriteLine("Please enter the name of the blog: ");
                var name = Console.ReadLine();

                var query = 
                    from data in db.Blogs
                    where data.Name == name
                    select data;

                var blogId = 1;

                foreach (var blog in query)
                {
                    blogId = blog.BlogId;
                }
                
                Console.WriteLine("Please enter a title: ");
                var title = Console.ReadLine();

                Console.WriteLine("Please enter the content: ");
                var content = Console.ReadLine();

                var post = new Post { Title = title, Content = content, BlogId = blogId };
                db.Posts.Add(post);
                db.SaveChanges();

                Console.WriteLine("Post created.");

                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
            }
        }

        private static void CreateBlog()
        {
            using (var db = new BloggingContext())
            {
                Console.WriteLine("Please enter a name for the blog: ");
                var name = Console.ReadLine();

                Console.WriteLine("Please enter an url for the blog: ");
                var url = Console.ReadLine();

                var blog = new Blog { Name = name, Url = url};
                db.Blogs.Add(blog);
                db.SaveChanges();

                Console.WriteLine("Blog created.");

                Console.WriteLine("Press any key to return.");
                Console.ReadKey();

            }
        }
    }
}
