﻿namespace MVCProject.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Designation = c.String(),
                        RoleID = c.Int(nullable: false),
                        Gender = c.String(),
                        ProjectManagerName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        TotalLeave = c.Int(nullable: false),
                        LeaveTaken = c.Int(nullable: false),
                        AvailableLeave = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        LeaveReason = c.String(),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "RoleID", "dbo.Roles");
            DropIndex("dbo.Leaves", new[] { "EmployeeID" });
            DropIndex("dbo.Employees", new[] { "RoleID" });
            DropTable("dbo.Leaves");
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
        }
    }
}
