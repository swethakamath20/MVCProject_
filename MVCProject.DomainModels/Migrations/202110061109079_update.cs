namespace MVCProject.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leaves", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Leaves", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leaves", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Leaves", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
