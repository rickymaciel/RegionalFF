namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ciudads", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pais", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ciudads", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Pais", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String(nullable: false));
            DropColumn("dbo.Ciudads", "ApplicationUser_Id");
            DropColumn("dbo.Pais", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pais", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Ciudads", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Nombre", c => c.String());
            CreateIndex("dbo.Pais", "ApplicationUser_Id");
            CreateIndex("dbo.Ciudads", "ApplicationUser_Id");
            AddForeignKey("dbo.Pais", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ciudads", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
