using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FineController : ControllerBase
    {
        private readonly ILogger<FineController> _logger;
        private readonly IFineRepository _fineRepository;

        public FineController(ILogger<FineController> logger, IFineRepository fineRepository)
        {
            _logger = logger;
            _fineRepository = fineRepository;   
        }

        [HttpGet]
        [Route("GetFines")]
        public IList<Fine> Get()
        {
            return _fineRepository.GetFines();
        }

    }
}