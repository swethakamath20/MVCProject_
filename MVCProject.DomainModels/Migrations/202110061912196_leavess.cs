namespace MVCProject.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leavess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "EmployeeName", c => c.String());
            AddColumn("dbo.Leaves", "Designation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "Designation");
            DropColumn("dbo.Leaves", "EmployeeName");
        }
    }
}
