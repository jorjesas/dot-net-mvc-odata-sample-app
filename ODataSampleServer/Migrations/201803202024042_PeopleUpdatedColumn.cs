namespace ODataSampleServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeopleUpdatedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Quote", c => c.String());
            DropColumn("dbo.People", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Description", c => c.String());
            DropColumn("dbo.People", "Quote");
        }
    }
}
