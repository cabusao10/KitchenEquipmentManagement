namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Description = c.String(),
                        Condition = c.String(),
                        UserId = c.Int(),
                        DateCreated = c.DateTime(),
                        DateModifed = c.DateTime(),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.EquipmentId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.RegisteredEquipments", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RegisteredEquipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModifed = c.DateTime(),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipment", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Site", t => t.SiteId)
                .Index(t => t.EquipmentId)
                .Index(t => t.SiteId);
            
            CreateTable(
                "dbo.Site",
                c => new
                    {
                        SiteId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        UserId = c.Int(),
                        DateCreated = c.DateTime(),
                        DateModifed = c.DateTime(),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.SiteId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserType = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipment", "UserId", "dbo.RegisteredEquipments");
            DropForeignKey("dbo.RegisteredEquipments", "SiteId", "dbo.Site");
            DropForeignKey("dbo.Site", "UserId", "dbo.User");
            DropForeignKey("dbo.Equipment", "UserId", "dbo.User");
            DropForeignKey("dbo.RegisteredEquipments", "EquipmentId", "dbo.Equipment");
            DropIndex("dbo.Site", new[] { "UserId" });
            DropIndex("dbo.RegisteredEquipments", new[] { "SiteId" });
            DropIndex("dbo.RegisteredEquipments", new[] { "EquipmentId" });
            DropIndex("dbo.Equipment", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Site");
            DropTable("dbo.RegisteredEquipments");
            DropTable("dbo.Equipment");
        }
    }
}
