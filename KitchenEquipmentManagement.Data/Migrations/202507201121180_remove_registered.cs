namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_registered : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipment", "RegisteredEquipments_Id", "dbo.RegisteredEquipments");
            DropIndex("dbo.Equipment", new[] { "RegisteredEquipments_Id" });
            DropColumn("dbo.Equipment", "RegisteredEquipments_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipment", "RegisteredEquipments_Id", c => c.Int());
            CreateIndex("dbo.Equipment", "RegisteredEquipments_Id");
            AddForeignKey("dbo.Equipment", "RegisteredEquipments_Id", "dbo.RegisteredEquipments", "Id");
        }
    }
}
