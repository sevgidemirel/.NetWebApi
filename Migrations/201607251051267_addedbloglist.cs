namespace blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbloglist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.blog", "token_id", c => c.Int());
            CreateIndex("dbo.blog", "token_id");
            AddForeignKey("dbo.blog", "token_id", "dbo.token", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.blog", "token_id", "dbo.token");
            DropIndex("dbo.blog", new[] { "token_id" });
            DropColumn("dbo.blog", "token_id");
        }
    }
}
