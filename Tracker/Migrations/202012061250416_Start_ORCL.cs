namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start_ORCL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "COMP.Companies",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "COMP.Users",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FirstName = c.String(),
                        Lastname = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        CompanyId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserTypeId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("COMP.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("COMP.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "COMP.Issues",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StatusId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        AgentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NotifierId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Companyid = c.Decimal(nullable: false, precision: 10, scale: 0),
                        User_Id = c.Decimal(precision: 10, scale: 0),
                        User_Id1 = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("COMP.Users", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("COMP.Companies", t => t.Companyid, cascadeDelete: true)
                .ForeignKey("COMP.Users", t => t.NotifierId, cascadeDelete: true)
                .ForeignKey("COMP.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("COMP.Users", t => t.User_Id)
                .ForeignKey("COMP.Users", t => t.User_Id1)
                .Index(t => t.StatusId)
                .Index(t => t.AgentId)
                .Index(t => t.NotifierId)
                .Index(t => t.Companyid)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "COMP.Notifications",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        WorkDone = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        IssueId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        WorkerId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("COMP.Issues", t => t.IssueId, cascadeDelete: true)
                .ForeignKey("COMP.Users", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.IssueId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "COMP.Status",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "COMP.UserTypes",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("COMP.Users", "UserTypeId", "COMP.UserTypes");
            DropForeignKey("COMP.Issues", "User_Id1", "COMP.Users");
            DropForeignKey("COMP.Issues", "User_Id", "COMP.Users");
            DropForeignKey("COMP.Issues", "StatusId", "COMP.Status");
            DropForeignKey("COMP.Issues", "NotifierId", "COMP.Users");
            DropForeignKey("COMP.Notifications", "WorkerId", "COMP.Users");
            DropForeignKey("COMP.Notifications", "IssueId", "COMP.Issues");
            DropForeignKey("COMP.Issues", "Companyid", "COMP.Companies");
            DropForeignKey("COMP.Issues", "AgentId", "COMP.Users");
            DropForeignKey("COMP.Users", "CompanyId", "COMP.Companies");
            DropIndex("COMP.Notifications", new[] { "WorkerId" });
            DropIndex("COMP.Notifications", new[] { "IssueId" });
            DropIndex("COMP.Issues", new[] { "User_Id1" });
            DropIndex("COMP.Issues", new[] { "User_Id" });
            DropIndex("COMP.Issues", new[] { "Companyid" });
            DropIndex("COMP.Issues", new[] { "NotifierId" });
            DropIndex("COMP.Issues", new[] { "AgentId" });
            DropIndex("COMP.Issues", new[] { "StatusId" });
            DropIndex("COMP.Users", new[] { "UserTypeId" });
            DropIndex("COMP.Users", new[] { "CompanyId" });
            DropTable("COMP.UserTypes");
            DropTable("COMP.Status");
            DropTable("COMP.Notifications");
            DropTable("COMP.Issues");
            DropTable("COMP.Users");
            DropTable("COMP.Companies");
        }
    }
}
