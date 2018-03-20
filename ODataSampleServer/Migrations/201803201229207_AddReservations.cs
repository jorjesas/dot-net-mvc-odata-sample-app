namespace ODataSampleServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservatonID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservatonID)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "PersonId", "dbo.People");
            DropIndex("dbo.Reservations", new[] { "PersonId" });
            DropTable("dbo.Reservations");
        }
    }
}
