using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookReservationController : ControllerBase
    {
        private readonly ILogger<BookReservationController> _logger;
        private readonly IBookReservationService _bookReservationService;

        public BookReservationController(ILogger<BookReservationController> logger, IBookReservationService bookReservationService)
        {
            _logger = logger;
            _bookReservationService = bookReservationService;   
        }

        [HttpGet]
        [Route("GetAvailableReservationDate")]
        public string GetAvailableReservationDate(string isbn)
        {
            return _bookReservationService.GetAvailableReservationDate(isbn);
        }

        [HttpPost]
        [Route("ReserveBook")]
        public string ReserveBook(string emailAddress, string isbn)
        {
            return _bookReservationService.ReserveBook(emailAddress, isbn);
        }
    }
}