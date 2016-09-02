namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OficinaUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Oficinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Sigla = c.String(),
                        Departamento = c.String(),
                        Ciudad = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Numero", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "OficinaId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Documento", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String());
            AddColumn("dbo.AspNetUsers", "Apellido", c => c.String());
            CreateIndex("dbo.AspNetUsers", "OficinaId");
            AddForeignKey("dbo.AspNetUsers", "OficinaId", "dbo.Oficinas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "OficinaId", "dbo.Oficinas");
            DropIndex("dbo.AspNetUsers", new[] { "OficinaId" });
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropColumn("dbo.AspNetUsers", "Documento");
            DropColumn("dbo.AspNetUsers", "OficinaId");
            DropColumn("dbo.AspNetUsers", "Numero");
            DropTable("dbo.Oficinas");
        }
    }
}
