namespace KitchenEquipmentManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Equipment", "DateModifed", c => c.DateTime());
            AddColumn("dbo.Equipment", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.RegisteredEquipments", "DateCreated", c => c.DateTime());
            AddColumn("dbo.RegisteredEquipments", "DateModifed", c => c.DateTime());
            AddColumn("dbo.RegisteredEquipments", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.Site", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Site", "DateModifed", c => c.DateTime());
            AddColumn("dbo.Site", "DateDeleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Site", "DateDeleted");
            DropColumn("dbo.Site", "DateModifed");
            DropColumn("dbo.Site", "DateCreated");
            DropColumn("dbo.RegisteredEquipments", "DateDeleted");
            DropColumn("dbo.RegisteredEquipments", "DateModifed");
            DropColumn("dbo.RegisteredEquipments", "DateCreated");
            DropColumn("dbo.Equipment", "DateDeleted");
            DropColumn("dbo.Equipment", "DateModifed");
            DropColumn("dbo.Equipment", "DateCreated");
        }
    }
}
