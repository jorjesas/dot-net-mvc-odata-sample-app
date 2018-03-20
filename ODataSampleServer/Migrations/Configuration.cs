namespace ODataSampleServer.Migrations
{
    using ODataSampleServer.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ODataSampleServer.Models.ODataSampleServerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ODataSampleServer.Models.ODataSampleServerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.People.AddOrUpdate(
                new Models.Person() {PersonID=1, Name="Dorutzu", IsUnicorn = false, Quote="Desi uneori iti doresti un unicorn viata iti ofera o capra.", LuckyNumber=2},
                new Models.Person() {PersonID=2, Name = "Mirelush", IsUnicorn = true, Quote = "2, fata mea!", LuckyNumber = 10 },
                new Models.Person() {PersonID=3, Name = "Aleja", IsUnicorn = true, Quote = "Uaaaaaaaaa!", LuckyNumber = 20 },
                new Models.Person() {PersonID=4, Name = "Ardei", IsUnicorn = false, Quote = "Dada nashpa", LuckyNumber = 7 },
                new Models.Person() {PersonID=5, Name = "Jorje", IsUnicorn = true, Quote = "So rude!", LuckyNumber = 11 }
                );

            IEnumerable<Reservation> reservations = Enumerable.Range(1, 100).AsParallel().ToList().Select(index => 
                new Reservation() {ReservatonID = index, Description = $"Description #{index}", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(index), Name = $"Reservation #{index}", Price = index, PersonId = index % 3 + 1 });

            context.Reservations.AddOrUpdate(
                reservations.ToArray()
                );
        }
    }
}
