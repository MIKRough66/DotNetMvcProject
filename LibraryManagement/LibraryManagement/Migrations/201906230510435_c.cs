namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookStores", "Title", c => c.String());
            AddColumn("dbo.BookStores", "SerialNumber", c => c.String());
            AddColumn("dbo.BookStores", "Author", c => c.String());
            AddColumn("dbo.BookStores", "Publisher", c => c.Int(nullable: false));
            DropColumn("dbo.BookStores", "Name");
            DropColumn("dbo.BookStores", "Position");
            DropColumn("dbo.BookStores", "Office");
            DropColumn("dbo.BookStores", "Salary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookStores", "Salary", c => c.Int(nullable: false));
            AddColumn("dbo.BookStores", "Office", c => c.String());
            AddColumn("dbo.BookStores", "Position", c => c.String());
            AddColumn("dbo.BookStores", "Name", c => c.String());
            DropColumn("dbo.BookStores", "Publisher");
            DropColumn("dbo.BookStores", "Author");
            DropColumn("dbo.BookStores", "SerialNumber");
            DropColumn("dbo.BookStores", "Title");
        }
    }
}
