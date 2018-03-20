using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ODataSampleServer.Models;
using System.Web.Http.OData.Query;

namespace ODataSampleServer.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ODataSampleServer.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Reservation>("Reservations");
    builder.EntitySet<Person>("People"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ReservationsController : ODataController
    {
        private ODataSampleServerContext db = new ODataSampleServerContext();

        // GET: odata/Reservations

        // Allow client paging but no other query options.
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.OrderBy,
                    AllowedOrderByProperties = "Name",
                    // Disable any() and all() functions
                    AllowedFunctions = AllowedFunctions.AllFunctions & ~AllowedFunctions.All & ~AllowedFunctions.Any, PageSize = 10)]
        public IQueryable<Reservation> GetReservations()
        {
        //    var reservations = db.Reservations.Include(r => r.Person).ToList();

        //    return reservations.ConvertAll(r =>  new { Name = r.Name, Guest = r.Person.Name, StartDate = r.StartDate, EndDate = r.EndDate });

            return db.Reservations;
        }

        // GET: odata/Reservations(5)
        [EnableQuery]
        public SingleResult<Reservation> GetReservation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reservations.Where(reservation => reservation.ReservatonID == key));
        }

        // PUT: odata/Reservations(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Reservation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Reservation reservation = await db.Reservations.FindAsync(key);
            if (reservation == null)
            {
                return NotFound();
            }

            patch.Put(reservation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reservation);
        }

        // POST: odata/Reservations
        public async Task<IHttpActionResult> Post(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();

            return Created(reservation);
        }

        // PATCH: odata/Reservations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Reservation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Reservation reservation = await db.Reservations.FindAsync(key);
            if (reservation == null)
            {
                return NotFound();
            }

            patch.Patch(reservation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(reservation);
        }

        // DELETE: odata/Reservations(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Reservation reservation = await db.Reservations.FindAsync(key);
            if (reservation == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Reservations(5)/Person
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Reservations.Where(m => m.ReservatonID == key).Select(m => m.Person));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservationExists(int key)
        {
            return db.Reservations.Count(e => e.ReservatonID == key) > 0;
        }
    }
}
