namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OficinaFuncionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        Apellido = c.String(nullable: false, maxLength: 30),
                        NumeroFuncionario = c.Int(nullable: false),
                        Documento = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        OficinaId = c.Int(nullable: false),
                        Creado = c.DateTime(nullable: false, storeType: "date"),
                        Modificado = c.DateTime(nullable: false, storeType: "date"),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Oficinas", t => t.OficinaId, cascadeDelete: true)
                .Index(t => t.OficinaId);
            
            CreateTable(
                "dbo.Oficinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        Departamento = c.String(nullable: false, maxLength: 30),
                        Ciudad = c.String(nullable: false, maxLength: 30),
                        Telefono = c.String(),
                        Ubicacion = c.String(),
                        Director = c.String(),
                        Creado = c.DateTime(nullable: false, storeType: "date"),
                        Modificado = c.DateTime(nullable: false, storeType: "date"),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionarios", "OficinaId", "dbo.Oficinas");
            DropIndex("dbo.Funcionarios", new[] { "OficinaId" });
            DropTable("dbo.Oficinas");
            DropTable("dbo.Funcionarios");
        }
    }
}
