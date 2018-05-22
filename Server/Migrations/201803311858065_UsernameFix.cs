using System.Data.Entity.Migrations;

namespace Server.Migrations
{
    public partial class UsernameFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "Userame");
        }

        public override void Down()
        {
            AddColumn("dbo.Users", "Userame", c => c.String());
            DropColumn("dbo.Users", "Username");
        }
    }
}