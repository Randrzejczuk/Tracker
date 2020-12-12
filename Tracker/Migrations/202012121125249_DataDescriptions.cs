namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataDescriptions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("COMP.Companies", "Name", c => c.String(nullable: false));
            AlterColumn("COMP.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("COMP.Users", "Lastname", c => c.String(nullable: false));
            AlterColumn("COMP.Users", "Login", c => c.String(nullable: false));
            AlterColumn("COMP.Users", "Password", c => c.String(nullable: false));
            AlterColumn("COMP.Notifications", "WorkDone", c => c.String(nullable: false));
            AlterColumn("COMP.Issues", "Title", c => c.String(nullable: false));
            AlterColumn("COMP.Issues", "Description", c => c.String(nullable: false));
            AlterColumn("COMP.Status", "Name", c => c.String(nullable: false));
            AlterColumn("COMP.UserTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("COMP.UserTypes", "Name", c => c.String());
            AlterColumn("COMP.Status", "Name", c => c.String());
            AlterColumn("COMP.Issues", "Description", c => c.String());
            AlterColumn("COMP.Issues", "Title", c => c.String());
            AlterColumn("COMP.Notifications", "WorkDone", c => c.String());
            AlterColumn("COMP.Users", "Password", c => c.String());
            AlterColumn("COMP.Users", "Login", c => c.String());
            AlterColumn("COMP.Users", "Lastname", c => c.String());
            AlterColumn("COMP.Users", "FirstName", c => c.String());
            AlterColumn("COMP.Companies", "Name", c => c.String());
        }
    }
}
