using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookStockController : ControllerBase
    {
        private readonly ILogger<BookStockController> _logger;
        private readonly IOnLoanService _onLoanService;

        public BookStockController(ILogger<BookStockController> logger, IOnLoanService onLoanService)
        {
            _logger = logger;
            _onLoanService = onLoanService;   
        }

        [HttpGet]
        [Route("OnLoan")]
        public IList<OnLoanBook> OnLoan()
        {
            return _onLoanService.GetBorrowersWithActiveLoans();
        }

    }
}