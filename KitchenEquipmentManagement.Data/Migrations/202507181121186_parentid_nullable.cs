namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentid_nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipment", "UserId", "dbo.User");
            DropForeignKey("dbo.Site", "UserId", "dbo.User");
            DropIndex("dbo.Equipment", new[] { "UserId" });
            DropIndex("dbo.Site", new[] { "UserId" });
            AlterColumn("dbo.Equipment", "UserId", c => c.Int());
            AlterColumn("dbo.Site", "UserId", c => c.Int());
            CreateIndex("dbo.Equipment", "UserId");
            CreateIndex("dbo.Site", "UserId");
            AddForeignKey("dbo.Equipment", "UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.Site", "UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Site", "UserId", "dbo.User");
            DropForeignKey("dbo.Equipment", "UserId", "dbo.User");
            DropIndex("dbo.Site", new[] { "UserId" });
            DropIndex("dbo.Equipment", new[] { "UserId" });
            AlterColumn("dbo.Site", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Equipment", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Site", "UserId");
            CreateIndex("dbo.Equipment", "UserId");
            AddForeignKey("dbo.Site", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Equipment", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
