namespace blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parola : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.kullanici", "password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.kullanici", "password");
        }
    }
}
