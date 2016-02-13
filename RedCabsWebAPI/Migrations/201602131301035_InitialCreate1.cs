namespace RedCabsWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PSK", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PSK");
        }
    }
}
