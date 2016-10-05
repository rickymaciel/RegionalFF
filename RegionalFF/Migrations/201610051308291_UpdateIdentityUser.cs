namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentityUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Direccion", c => c.String());
            AddColumn("dbo.AspNetUsers", "Imagen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Imagen");
            DropColumn("dbo.AspNetUsers", "Direccion");
        }
    }
}
