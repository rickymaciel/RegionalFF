namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Perfil", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Perfil");
        }
    }
}
