namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookStores", "Publisher", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookStores", "Publisher", c => c.Int(nullable: false));
        }
    }
}
