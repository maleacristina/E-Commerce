namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        BookId = c.Int(nullable: false),
                        Cantitate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "BookId", "dbo.Books");
            DropIndex("dbo.Carts", new[] { "BookId" });
            DropTable("dbo.Carts");
        }
    }
}
