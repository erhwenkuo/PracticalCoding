namespace PracticalCoding.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCirRefAuthorBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CirRefAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CirRefBooks",
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
                .ForeignKey("dbo.CirRefAuthors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CirRefBooks", "AuthorId", "dbo.CirRefAuthors");
            DropIndex("dbo.CirRefBooks", new[] { "AuthorId" });
            DropTable("dbo.CirRefBooks");
            DropTable("dbo.CirRefAuthors");
        }
    }
}
