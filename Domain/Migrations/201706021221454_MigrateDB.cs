namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sweets",
                c => new
                    {
                        SweetId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Ingredients = c.String(nullable: false),
                        Packing = c.Int(nullable: false),
                        Expiration_date = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.SweetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sweets");
        }
    }
}
