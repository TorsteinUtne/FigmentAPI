using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerService.Data;
using PowerService.Data.Models;
using PowerService.Data.Models.Queries;
using PowerService.Data.Models.RequestResponseObjects;
using PowerService.Data.Models.RequestResponseObjects.Wrappers;
using PowerService.Services;
using PowerService.Util;

namespace PowerService.DAL.Context
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly PowerServiceContext _context;
        private readonly IUriService _uriService;
        private readonly ILogger _logger;
        public BookingController(PowerServiceContext context, IUriService uriService, ILogger<BookingController> logger)
        {
            _context = context;
            _uriService = uriService;
            _logger = logger;
        }

        /// <summary>
        /// Get Bookings henter ut timebestillinger, basert på et eller flere søkeparametre. Maks antall oppføringer per side er 250 (kan endres i config). For å søke på flere søkefelt samtidig, bruk semikolon for å skille søkefelt. Feilstavede søkefelt og sorteringsfelt blir ignorert
        /// </summary>
        /// <param name="searchParameters">Objekt som inneholder søkefelt, søkeverdi, paginering, maks antall records, sorteringsfelt, samt sorteringsrekkefølge </param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                    nameof(DefaultApiConventions.Get))]
        //TODO: FIltrer på Owner-feltet
        public async Task<ActionResult<PagedResponse<List<BookingResponse>>>> GetBoookings([FromQuery] Data.Models.Queries.SearchParameter searchParameters)
        {
            //TODO: Refactor, build service repository for methods accessing all entity types
            var route = Request.Path.Value;


            try
            {
                var bookings = await BuildAndExecuteQuery(searchParameters);
                var results = await BuildResponseObjects(bookings.Results);
                //Denne bør gi tall på antall treff, ikke antall oppføringer totalt. results-objektet er begrenset av skip-take, og vil ikke vise dette.
                var pagedResponse = CreatePagedResponses(searchParameters, results, bookings.TotalRecords, route);

                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "", 10001, Request.Path.Value);
                return Ok(pagedResponse);
            }
            catch (UnauthorizedAccessException uae)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Warning, _logger, uae, "", 30001, Request.Path.Value);
                return Unauthorized();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, "", 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        private PagedResponse<List<BookingResponse>> CreatePagedResponses(SearchParameter searchParameters, List<BookingResponse> results, int totalRecords,
            string route)
        {
            var pagedResponse = PaginationHelper.CreatePagedReponse<BookingResponse>(results,
                new PaginationFilter(searchParameters.PageNumber, searchParameters.PageSize), totalRecords, _uriService, route);
            return pagedResponse;
        }

        private async Task<List<BookingResponse>> BuildResponseObjects(List<Booking> bookings)
        {
            var results = new List<BookingResponse>();
            foreach (var billing in bookings)
            {
                var response = await new BookingResponse().GetResponse(billing.Id, _context);
                results.Add(response.Value);
            }

            return results;
        }






        /// <summary>
        /// Retrieves a single Booking
        /// </summary>
        /// <param name="id">GUID for the Booking</param>
        /// <returns></returns>
        // GET: api/Booking/5
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Get))]

        public async Task<ActionResult<Response<BookingResponse>>> GetBooking(Guid id)
        {
            try
            {
                var retrievedBooking = await new BookingResponse().GetResponse(id, _context);

                if (retrievedBooking == null)
                {
                    return NotFound();
                }
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, $"Successfully retrieved booking with Id {id}", 10001, Request.Path.Value);
                var response = new Response<BookingResponse>
                {
                    Data = retrievedBooking.Value,
                    Message = $"Successfully retrieved booking with Id {id}",
                    Succeeded = true,
                    Errors = null
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }
        //PATCHE eier - evt egen assign - funksjon
        //TODO: SJekk for endring i eier
        //Forsøk på endring av readonly - attributter må resultere i feilmelding - sånn at det ikke er mulig å patche id eller createdat, osv
        /// <summary>
        /// oppdaterer en Booking
        /// </summary>
        /// <param name="id">Guid to the booking that is to be patched</param>
        /// <param name="patch">JSON Patch document</param>
        /// <returns></returns>
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPatch("{id}")]
        public async Task<ActionResult<Response<BookingResponse>>> PatchBooking([FromUri] Guid id, [Microsoft.AspNetCore.Mvc.FromBody] JsonPatchDocument<BookingRequest> patch)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();
            var bookingRequest = await new BookingRequest().GetRequest(booking.Id, _context); //Henter frem en komplett request slik at hele objektet kan returnerens
            try
            {
                patch.Sanitize();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, null, patch.ToString(), 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
            patch.ApplyTo(bookingRequest.Value, ModelState);
            var updatedBooking = new Booking(bookingRequest.Value, booking.OwnerId.Value, _context); //Kopierer inn nye verdier
            //Merger dem over på eksisterende account
            MappingFunctions.CopyValues<Booking>(booking, updatedBooking);
            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, patch.ToString(), 10002, Request.Path.Value);
            var response = new BookingResponse().GeneratePatchResponse(patch, updatedBooking, Request.Path.Value, _context);
            return Ok(response);
        }




        /// <summary>
        /// Creates a new booking
        /// </summary>
        /// <param name="bookingrequest">Request-object of type BookingRequest to create a new booking</param>
        /// <returns></returns>
        // POST: api/Booking
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Response<BookingResponse>>> PostBilling([Microsoft.AspNetCore.Mvc.FromBody] BookingRequest bookingRequest)
        {
            try
            {
                Claim first = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (first != null)
                {
                    string userId = first.Value;
                    var owner = await _context.PortalUsers.FirstOrDefaultAsync(i => i.AuthOId == userId);
                    var booking = new Booking(bookingRequest, owner.Id, _context);

                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();
                    HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null, "Created booking with ID: " + booking.Id, 10003, Request.Path.Value);
                    var response = new Response<BookingResponse>
                    {
                        Data = new BookingResponse().GetResponse(booking.Id, _context).Result.Value,
                        Errors = null,
                        Message = $"Created booking with ID: {booking.Id}",
                        Succeeded = true
                    };
                    return Ok(response);

                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        //TODO: Check for rules and regulations
        /// <summary>
        /// Deletes a single booking -Softdelete only? Drafts should be deleted, sent should be credited, ects
        /// </summary>
        /// <param name="id">Guid to the booking that is to be deleted</param>
        /// <returns></returns>
        [Authorize]
        // DELETE: api/Booking/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<Response<string>>> DeleteBooking(Guid id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    return NotFound();
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                HelpFunctions.CreateLogEntry(LogLevel.Information, _logger, null,
                    $"Record with Id {id.ToString()} was deleted", 10004, Request.Path.Value);
                var response = new Response<string>
                {
                    Message = $"Booking record was deleted",
                    Data = $"ID: {id.ToString()}",
                    Succeeded = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                HelpFunctions.CreateLogEntry(LogLevel.Error, _logger, ex, ex.Message, 59001, Request.Path.Value);
                return BadRequest(ex.Message);
            }
        }

        #region Functions
        private async Task<QueryResults<List<Booking>>> BuildAndExecuteQuery(SearchParameter searchParameters)
        {
            var searchText = "";

            var columns = MappingFunctions.GetDisplayableColumns<BookingRequest>();
            var orderClause = MappingFunctions.BuildOrderClause(searchParameters, columns, "Name");
            var searchFields = searchParameters.SearchField.Split(';').ToList();


            //Splitter mulige flere søkefelt til en array
            if (columns.Contains(searchParameters.SearchField)) //Ellers søker vi på alle synlige kolonner
            {
                //Sjekker om searchField er i listen av displayable columns - da setter vi searchtext til den, hvis ikke bygger vi en søkestren av alle synlige kolonne
                searchText = MappingFunctions.BuildSearchClause(searchParameters, searchFields, _context);
            }
            else
            {
                searchText = MappingFunctions.BuildSearchClause(searchParameters, columns, _context);

            }

            var totalRecords = await _context.Bookings
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).CountAsync();
            var bookings = await _context.Bookings
                .Where(searchText, StringComparison.InvariantCultureIgnoreCase).OrderBy(orderClause)
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize).ToListAsync();
            return new QueryResults<List<Booking>>
            {
                Results = bookings,
                TotalRecords = totalRecords
            };

        }
        #endregion
    }
}