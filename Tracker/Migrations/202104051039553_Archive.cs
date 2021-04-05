namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Archive : DbMigration
    {
        public override void Up()
        {
            AddColumn("COMP.Companies", "ArchivedTimeStamp", c => c.DateTime());
            AddColumn("COMP.Users", "ArchivedTimeStamp", c => c.DateTime());
            AddColumn("COMP.Notifications", "ArchivedTimeStamp", c => c.DateTime());
            AddColumn("COMP.Issues", "ArchivedTimeStamp", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("COMP.Issues", "ArchivedTimeStamp");
            DropColumn("COMP.Notifications", "ArchivedTimeStamp");
            DropColumn("COMP.Users", "ArchivedTimeStamp");
            DropColumn("COMP.Companies", "ArchivedTimeStamp");
        }
    }
}
