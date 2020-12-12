namespace Tracker.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DeleteWrongIndex : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("COMP.Issues", "User_Id", "COMP.Users");
            DropForeignKey("COMP.Issues", "User_Id1", "COMP.Users");
            DropIndex("COMP.Issues", new[] { "User_Id" });
            DropIndex("COMP.Issues", new[] { "User_Id1" });
            DropColumn("COMP.Issues", "User_Id");
            DropColumn("COMP.Issues", "User_Id1");
        }

        public override void Down()
        {
            AddColumn("COMP.Issues", "User_Id1", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("COMP.Issues", "User_Id", c => c.Decimal(precision: 10, scale: 0));
            CreateIndex("COMP.Issues", "User_Id1");
            CreateIndex("COMP.Issues", "User_Id");
            AddForeignKey("COMP.Issues", "User_Id1", "COMP.Users", "Id");
            AddForeignKey("COMP.Issues", "User_Id", "COMP.Users", "Id");
        }
    }
}
