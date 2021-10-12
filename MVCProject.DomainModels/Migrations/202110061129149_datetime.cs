namespace MVCProject.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leaves", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Leaves", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leaves", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Leaves", "StartDate", c => c.DateTime());
        }
    }
}
