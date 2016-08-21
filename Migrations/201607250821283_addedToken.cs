namespace blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.token", "tknaccess", c => c.String());
            AddColumn("dbo.token", "date", c => c.DateTime(nullable: false));
            DropColumn("dbo.token", "accestoken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.token", "accestoken", c => c.String());
            DropColumn("dbo.token", "date");
            DropColumn("dbo.token", "tknaccess");
        }
    }
}
