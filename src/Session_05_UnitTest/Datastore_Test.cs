using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceKit.Redis;
using NServiceKit.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace Session_05_UnitTest
{
    [TestClass]
    public class Datastore_Test
    {
        protected RedisClient Redis;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            RedisClient.NewFactoryFn = () => new RedisClient("localhost");
            Redis = RedisClient.New();
            Redis.FlushAll();
            InsertTestData();
        }

        public void InsertTestData()
        {
            var redisUsers = Redis.As<User>();
            var redisBlogs = Redis.As<Blog>();
            var redisBlogPosts = Redis.As<BlogPost>();

            var erhwenkuo = new User { Id = redisUsers.GetNextSequence(), Name = "Erhwen Kuo" };
            var godknowwho = new User { Id = redisUsers.GetNextSequence(), Name = "Godknow Who" };

            var erhwenkuoBlog = new Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = erhwenkuo.Id,
                UserName = erhwenkuo.Name,
                Tags = new List<string> { "Architecture", ".NET", "Databases" },
            };

            var godknowwhoBlog = new Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = godknowwho.Id,
                UserName = godknowwho.Name,
                Tags = new List<string> { "Architecture", ".NET", "Databases" },
            };

            var blogPosts = new List<BlogPost>
				{
					new BlogPost
					{
						Id = redisBlogPosts.GetNextSequence(),
						BlogId = erhwenkuoBlog.Id,
						Title = "Elasticsearch",
						Categories = new List<string> { "NoSQL", "DocumentDB" },
						Tags = new List<string> {"Elasticsearch", "NoSQL", "JSON", ".NET"} ,
						Comments = new List<BlogPostComment>
						{
							new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,},
							new BlogPostComment { Content = "Second Comment!", CreatedDate = DateTime.UtcNow,},
						}
					},
					new BlogPost
					{
						Id = redisBlogPosts.GetNextSequence(),
						BlogId = godknowwho.Id,
						Title = "Redis",
						Categories = new List<string> { "NoSQL", "Cache" },
						Tags = new List<string> {"Redis", "NoSQL", "Scalability", "Performance"},
						Comments = new List<BlogPostComment>
						{
							new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
						}
					},
					new BlogPost
					{
						Id = redisBlogPosts.GetNextSequence(),
						BlogId = erhwenkuoBlog.Id,
						Title = "Cassandra",
						Categories = new List<string> { "NoSQL", "Cluster" },
						Tags = new List<string> {"Cassandra", "NoSQL", "Scalability", "Hashing"},
						Comments = new List<BlogPostComment>
						{
							new BlogPostComment { Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
						}
					},
					new BlogPost
					{
						Id = redisBlogPosts.GetNextSequence(),
						BlogId = godknowwho.Id,
						Title = "Couch Db",
						Categories = new List<string> { "NoSQL", "DocumentDB" },
						Tags = new List<string> {"CouchDb", "NoSQL", "JSON"},
						Comments = new List<BlogPostComment>
						{
							new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow,}
						}
					},
				};

            erhwenkuo.BlogIds.Add(erhwenkuoBlog.Id);
            godknowwho.BlogIds.Add(godknowwhoBlog.Id);

            foreach (var blogPost in blogPosts)
            {
                if (blogPost.BlogId == erhwenkuoBlog.Id)
                    erhwenkuoBlog.BlogPostIds.Add(blogPost.Id);

                if (blogPost.BlogId == godknowwhoBlog.Id)
                    godknowwhoBlog.BlogPostIds.Add(blogPost.Id);
            }

            redisUsers.Store(erhwenkuo);
            redisUsers.Store(godknowwho);
            redisBlogs.StoreAll(new[] { erhwenkuoBlog, godknowwhoBlog });
            redisBlogPosts.StoreAll(blogPosts);
        }

        [TestMethod]
        public void Show_a_list_of_blogs()
        {
            var redisBlogs = Redis.As<Blog>();
            var blogs = redisBlogs.GetAll();
            Debug.WriteLine(blogs.Dump());
        }
    }

    public class User
    {
        public User()
        {
            this.BlogIds = new List<long>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public List<long> BlogIds { get; set; }
    }

    public class Blog
    {
        public Blog()
        {
            this.Tags = new List<string>();
            this.BlogPostIds = new List<long>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Tags { get; set; }
        public List<long> BlogPostIds { get; set; }
    }

    public class BlogPost
    {
        public BlogPost()
        {
            this.Categories = new List<string>();
            this.Tags = new List<string>();
            this.Comments = new List<BlogPostComment>();
        }

        public long Id { get; set; }
        public long BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }
        public List<BlogPostComment> Comments { get; set; }
    }

    public class BlogPostComment
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
