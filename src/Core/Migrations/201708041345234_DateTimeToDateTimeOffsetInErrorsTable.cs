namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeToDateTimeOffsetInErrorsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exceptions", "CreationDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Exceptions", "DeletionDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exceptions", "DeletionDate", c => c.DateTime());
            AlterColumn("dbo.Exceptions", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
