namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAwesomeInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("security.Users", "AwesomeInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("security.Users", "AwesomeInformation");
        }
    }
}
