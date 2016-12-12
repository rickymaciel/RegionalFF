namespace RegionalFF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFacilitacion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Facilitacions", "Observaciones", c => c.String(maxLength: 140));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Facilitacions", "Observaciones", c => c.String());
        }
    }
}
