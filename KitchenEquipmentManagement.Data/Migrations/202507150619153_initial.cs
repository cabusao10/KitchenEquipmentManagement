namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentEntities",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(),
                        Description = c.String(),
                        Condition = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentId)
                .ForeignKey("dbo.UserEntities", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RegisteredEquipmentEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        SiteEntity_SiteId = c.Int(),
                        EquipmentEntity_EquipmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentEntities", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.SiteEntities", t => t.SiteEntity_SiteId)
                .ForeignKey("dbo.SiteEntities", t => t.SiteId)
                .ForeignKey("dbo.EquipmentEntities", t => t.EquipmentEntity_EquipmentId)
                .Index(t => t.EquipmentId)
                .Index(t => t.SiteId)
                .Index(t => t.SiteEntity_SiteId)
                .Index(t => t.EquipmentEntity_EquipmentId);
            
            CreateTable(
                "dbo.SiteEntities",
                c => new
                    {
                        SiteId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SiteId)
                .ForeignKey("dbo.UserEntities", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserEntities",
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
            DropForeignKey("dbo.RegisteredEquipmentEntities", "EquipmentEntity_EquipmentId", "dbo.EquipmentEntities");
            DropForeignKey("dbo.RegisteredEquipmentEntities", "SiteId", "dbo.SiteEntities");
            DropForeignKey("dbo.SiteEntities", "UserId", "dbo.UserEntities");
            DropForeignKey("dbo.EquipmentEntities", "UserId", "dbo.UserEntities");
            DropForeignKey("dbo.RegisteredEquipmentEntities", "SiteEntity_SiteId", "dbo.SiteEntities");
            DropForeignKey("dbo.RegisteredEquipmentEntities", "EquipmentId", "dbo.EquipmentEntities");
            DropIndex("dbo.SiteEntities", new[] { "UserId" });
            DropIndex("dbo.RegisteredEquipmentEntities", new[] { "EquipmentEntity_EquipmentId" });
            DropIndex("dbo.RegisteredEquipmentEntities", new[] { "SiteEntity_SiteId" });
            DropIndex("dbo.RegisteredEquipmentEntities", new[] { "SiteId" });
            DropIndex("dbo.RegisteredEquipmentEntities", new[] { "EquipmentId" });
            DropIndex("dbo.EquipmentEntities", new[] { "UserId" });
            DropTable("dbo.UserEntities");
            DropTable("dbo.SiteEntities");
            DropTable("dbo.RegisteredEquipmentEntities");
            DropTable("dbo.EquipmentEntities");
        }
    }
}
