namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Userame = c.String(),
                        Password = c.String(),
                        UserInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameLoseCount = c.Int(nullable: false),
                        GameWinCount = c.Int(nullable: false),
                        NorthDeck = c.String(),
                        SouthDeck = c.String(),
                        DarkDeck = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.Users", new[] { "UserInfo_Id" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Users");
        }
    }
}
