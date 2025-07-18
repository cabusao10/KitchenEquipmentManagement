namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeentityname : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EquipmentEntities", newName: "Equipment");
            RenameTable(name: "dbo.RegisteredEquipmentEntities", newName: "RegisteredEquipments");
            RenameTable(name: "dbo.SiteEntities", newName: "Site");
            RenameTable(name: "dbo.UserEntities", newName: "User");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.User", newName: "UserEntities");
            RenameTable(name: "dbo.Site", newName: "SiteEntities");
            RenameTable(name: "dbo.RegisteredEquipments", newName: "RegisteredEquipmentEntities");
            RenameTable(name: "dbo.Equipment", newName: "EquipmentEntities");
        }
    }
}
