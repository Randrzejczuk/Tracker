namespace Tracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("COMP.Users", "Login", c => c.String());
            DropColumn("COMP.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("COMP.Users", "Password", c => c.String(nullable: false));
            AlterColumn("COMP.Users", "Login", c => c.String(nullable: false));
        }
    }
}
