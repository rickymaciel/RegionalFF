namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ciudads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Facilitacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Mes = c.String(nullable: false),
                        Año = c.Int(nullable: false),
                        PaisId = c.Int(nullable: false),
                        CiudadId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(),
                        Cantidad = c.Int(nullable: false),
                        Estadia = c.Int(nullable: false),
                        Observaciones = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciudads", t => t.CiudadId, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.PaisId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PaisId)
                .Index(t => t.CiudadId);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
            DropForeignKey("dbo.Pais", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "OficinaId", "dbo.Oficinas");
            DropForeignKey("dbo.Facilitacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ciudads", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Facilitacions", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.Facilitacions", "CiudadId", "dbo.Ciudads");
            DropIndex("dbo.AspNetUsers", new[] { "OficinaId" });
            DropIndex("dbo.Pais", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Facilitacions", new[] { "CiudadId" });
            DropIndex("dbo.Facilitacions", new[] { "PaisId" });
            DropIndex("dbo.Facilitacions", new[] { "UserId" });
            DropIndex("dbo.Ciudads", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropColumn("dbo.AspNetUsers", "Documento");
            DropColumn("dbo.AspNetUsers", "OficinaId");
            DropColumn("dbo.AspNetUsers", "Numero");
            DropTable("dbo.Oficinas");
            DropTable("dbo.Pais");
            DropTable("dbo.Facilitacions");
            DropTable("dbo.Ciudads");
        }
    }
}
