namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ciudad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ciudads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ciudads");
        }
    }
}
