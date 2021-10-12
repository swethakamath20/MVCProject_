namespace MVCProject.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leaveupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "LeaveStatus", c => c.String());
            DropColumn("dbo.Leaves", "TotalLeave");
            DropColumn("dbo.Leaves", "LeaveTaken");
            DropColumn("dbo.Leaves", "AvailableLeave");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "AvailableLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "LeaveTaken", c => c.Int(nullable: false));
            AddColumn("dbo.Leaves", "TotalLeave", c => c.Int(nullable: false));
            DropColumn("dbo.Leaves", "LeaveStatus");
        }
    }
}
