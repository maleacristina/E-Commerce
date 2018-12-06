namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'27538be2-4044-4b61-85fb-955eb66f0598', N'lucas@yahoo.com', 0, N'AGlPPm9fw24c35POKTibEcJvBGxM1+fqtuXRTNbYd9EUU+kX8irngbmXPIKFcFUajw==', N'dc846add-2bbc-4021-96f0-6c80bb5681f2', NULL, 0, 0, NULL, 1, 0, N'lucas@yahoo.com')
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8fd3a096-061a-46d3-aca7-d40e3e03a5f8', N'admin@yahoo.com', 0, N'AFaNuJr9gCQMVS2/m5t+CVq6tPKCIxJPQlrAG8At3qTIlnBH2/Itw7EhoSoLMhpvnw==', N'abd44fb5-4739-45a6-a4a7-f9e714b63507', NULL, 0, 0, NULL, 1, 0, N'admin@yahoo.com')
    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ca765a4f-8ce5-4902-9e7a-6766272d6738', N'ana@gmail.com', 0, N'AA7zqE1mBNLHABSvmH+WSRdBKG4fiOIGBrYhpX7mC8U724PnwMdLZVegOopc2QzAog==', N'1fceb4ad-5e60-46e0-ba90-b8c7ccb85629', NULL, 0, 0, NULL, 1, 0, N'ana@gmail.com')

    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd497b264-e5da-4f60-90f8-5d75e6462396', N'Admin')

    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8fd3a096-061a-46d3-aca7-d40e3e03a5f8', N'd497b264-e5da-4f60-90f8-5d75e6462396')



    ");
        }
        
        public override void Down()
        {
        }
    }
}
