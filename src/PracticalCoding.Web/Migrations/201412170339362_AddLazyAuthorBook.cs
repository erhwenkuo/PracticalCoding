namespace PracticalCoding.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLazyAuthorBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LazyAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LazyBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Genre = c.String(),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LazyAuthors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LazyBooks", "AuthorId", "dbo.LazyAuthors");
            DropIndex("dbo.LazyBooks", new[] { "AuthorId" });
            DropTable("dbo.LazyBooks");
            DropTable("dbo.LazyAuthors");
        }
    }
}
