namespace PracticalCoding.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChartdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chartdatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Period = c.String(),
                        Period_UTC = c.Double(nullable: false),
                        Taiex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MonitoringIndex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LeadingIndex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoincidentIndex = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LaggingIndex = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Chartdatas");
        }
    }
}
