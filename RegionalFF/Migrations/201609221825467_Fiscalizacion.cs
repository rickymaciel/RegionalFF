namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fiscalizacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conductors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Documento = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transportes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RazonSocial = c.String(nullable: false),
                        ConductorId = c.Int(nullable: false),
                        MarcaId = c.Int(nullable: false),
                        ChapaNro = c.String(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conductors", t => t.ConductorId, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .Index(t => t.ConductorId)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fiscalizacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Fecha = c.DateTime(nullable: false),
                        TransporteId = c.Int(nullable: false),
                        CiudadId = c.Int(nullable: false),
                        PaisId = c.Int(nullable: false),
                        CantidadPasajeros = c.Int(nullable: false),
                        Observaciones = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciudads", t => t.CiudadId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.PaisId, cascadeDelete: true)
                .ForeignKey("dbo.Transportes", t => t.TransporteId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TransporteId)
                .Index(t => t.CiudadId)
                .Index(t => t.PaisId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fiscalizacions", "TransporteId", "dbo.Transportes");
            DropForeignKey("dbo.Fiscalizacions", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.Fiscalizacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fiscalizacions", "CiudadId", "dbo.Ciudads");
            DropForeignKey("dbo.Transportes", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.Transportes", "ConductorId", "dbo.Conductors");
            DropIndex("dbo.Fiscalizacions", new[] { "PaisId" });
            DropIndex("dbo.Fiscalizacions", new[] { "CiudadId" });
            DropIndex("dbo.Fiscalizacions", new[] { "TransporteId" });
            DropIndex("dbo.Fiscalizacions", new[] { "UserId" });
            DropIndex("dbo.Transportes", new[] { "MarcaId" });
            DropIndex("dbo.Transportes", new[] { "ConductorId" });
            DropTable("dbo.Fiscalizacions");
            DropTable("dbo.Marcas");
            DropTable("dbo.Transportes");
            DropTable("dbo.Conductors");
        }
    }
}
