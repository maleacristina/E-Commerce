namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityAndCounyModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        NameCity = c.String(nullable: false),
                        County_CountyId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Counties", t => t.County_CountyId)
                .Index(t => t.County_CountyId);
            
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        CountyId = c.Int(nullable: false, identity: true),
                        NameCounty = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "County_CountyId", "dbo.Counties");
            DropIndex("dbo.Cities", new[] { "County_CountyId" });
            DropTable("dbo.Counties");
            DropTable("dbo.Cities");
        }
    }
}
