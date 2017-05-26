namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.short_urls",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        long_url = c.String(nullable: false, maxLength: 1000),
                        segment = c.String(nullable: false, maxLength: 20),
                        added = c.DateTime(nullable: false),
                        ip = c.String(nullable: false, maxLength: 50),
                        num_of_clicks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.stats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        click_date = c.DateTime(nullable: false),
                        ip = c.String(nullable: false, maxLength: 50),
                        referer = c.String(maxLength: 500),
                        ShortUrl_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.short_urls", t => t.ShortUrl_Id)
                .Index(t => t.ShortUrl_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.stats", "ShortUrl_Id", "dbo.short_urls");
            DropIndex("dbo.stats", new[] { "ShortUrl_Id" });
            DropTable("dbo.stats");
            DropTable("dbo.short_urls");
        }
    }
}
