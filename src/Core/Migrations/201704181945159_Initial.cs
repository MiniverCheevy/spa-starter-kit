using System.Data.Entity.Migrations;

namespace Core.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ApplicationSettings",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Name = c.String(maxLength: 128, unicode: false),
                        Value = c.String(maxLength: 128, unicode: false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Exceptions",
                    c => new
                    {
                        Id = c.Long(false, true),
                        GUID = c.Guid(false),
                        ApplicationName = c.String(maxLength: 200, unicode: false),
                        MachineName = c.String(maxLength: 200, unicode: false),
                        CreationDate = c.DateTime(false),
                        Type = c.String(maxLength: 200, unicode: false),
                        IsProtected = c.Boolean(false),
                        Host = c.String(maxLength: 200, unicode: false),
                        Url = c.String(maxLength: 200, unicode: false),
                        HTTPMethod = c.String(maxLength: 200, unicode: false),
                        IPAddress = c.String(maxLength: 200, unicode: false),
                        Source = c.String(maxLength: 200, unicode: false),
                        Message = c.String(maxLength: 200, unicode: false),
                        Detail = c.String(unicode: false),
                        StatusCode = c.Int(),
                        SQL = c.String(unicode: false),
                        DeletionDate = c.DateTime(),
                        FullJson = c.String(unicode: false),
                        ErrorHash = c.Int(),
                        DuplicateCount = c.Int(false),
                        User = c.String(maxLength: 128, unicode: false)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.GUID);

            CreateTable(
                    "dbo.Roles",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Name = c.String(maxLength: 128, unicode: false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserName = c.String(maxLength: 128, unicode: false),
                        FirstName = c.String(maxLength: 128, unicode: false),
                        LastName = c.String(maxLength: 128, unicode: false),
                        Salt = c.String(maxLength: 128, unicode: false),
                        PasswordHash = c.String(maxLength: 128, unicode: false),
                        LockoutEnabled = c.Boolean(false),
                        LastUserAgent = c.String(maxLength: 128, unicode: false),
                        LastAuthentication = c.DateTime()
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true);

            CreateTable(
                    "dbo.UserRoles",
                    c => new
                    {
                        User_Id = c.Int(false),
                        Role_Id = c.Int(false)
                    })
                .PrimaryKey(t => new {t.User_Id, t.Role_Id})
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] {"Role_Id"});
            DropIndex("dbo.UserRoles", new[] {"User_Id"});
            DropIndex("dbo.Users", new[] {"UserName"});
            DropIndex("dbo.Exceptions", new[] {"GUID"});
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Exceptions");
            DropTable("dbo.ApplicationSettings");
        }
    }
}