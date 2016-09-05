namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Facilitacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilitacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        MesId = c.Int(nullable: false),
                        AñoId = c.Int(nullable: false),
                        PaisId = c.Int(nullable: false),
                        CiudadId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(),
                        Cantidad = c.Int(nullable: false),
                        Estadia = c.Int(nullable: false),
                        Observaciones = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Año", t => t.AñoId, cascadeDelete: true)
                .ForeignKey("dbo.Ciudads", t => t.CiudadId, cascadeDelete: true)
                .ForeignKey("dbo.Meses", t => t.MesId, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.PaisId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MesId)
                .Index(t => t.AñoId)
                .Index(t => t.PaisId)
                .Index(t => t.CiudadId);
            
            AddColumn("dbo.Año", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Ciudads", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Meses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Pais", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Año", "ApplicationUser_Id");
            CreateIndex("dbo.Ciudads", "ApplicationUser_Id");
            CreateIndex("dbo.Meses", "ApplicationUser_Id");
            CreateIndex("dbo.Pais", "ApplicationUser_Id");
            AddForeignKey("dbo.Año", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ciudads", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Meses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Pais", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pais", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Meses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Facilitacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ciudads", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Año", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Facilitacions", "PaisId", "dbo.Pais");
            DropForeignKey("dbo.Facilitacions", "MesId", "dbo.Meses");
            DropForeignKey("dbo.Facilitacions", "CiudadId", "dbo.Ciudads");
            DropForeignKey("dbo.Facilitacions", "AñoId", "dbo.Año");
            DropIndex("dbo.Pais", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Meses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Facilitacions", new[] { "CiudadId" });
            DropIndex("dbo.Facilitacions", new[] { "PaisId" });
            DropIndex("dbo.Facilitacions", new[] { "AñoId" });
            DropIndex("dbo.Facilitacions", new[] { "MesId" });
            DropIndex("dbo.Facilitacions", new[] { "UserId" });
            DropIndex("dbo.Ciudads", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Año", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Pais", "ApplicationUser_Id");
            DropColumn("dbo.Meses", "ApplicationUser_Id");
            DropColumn("dbo.Ciudads", "ApplicationUser_Id");
            DropColumn("dbo.Año", "ApplicationUser_Id");
            DropTable("dbo.Facilitacions");
        }
    }
}
