namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class e : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookStores", "BookPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookStores", "BookPath");
        }
    }
}
