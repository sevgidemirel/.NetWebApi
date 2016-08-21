namespace blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.rol",
                c => new
                    {
                        rolId = c.Int(nullable: false, identity: true),
                        roltype = c.String(),
                        kullanici_kullaniciId = c.Int(),
                    })
                .PrimaryKey(t => t.rolId)
                .ForeignKey("dbo.kullanici", t => t.kullanici_kullaniciId)
                .Index(t => t.kullanici_kullaniciId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.rol", "kullanici_kullaniciId", "dbo.kullanici");
            DropIndex("dbo.rol", new[] { "kullanici_kullaniciId" });
            DropTable("dbo.rol");
        }
    }
}
