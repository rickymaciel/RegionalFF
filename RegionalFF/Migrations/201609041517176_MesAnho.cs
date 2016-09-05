namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MesAnho : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Año",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Anho = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meses");
            DropTable("dbo.Año");
        }
    }
}
