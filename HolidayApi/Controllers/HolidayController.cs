using HolidayApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;

        public HolidayController(IHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeriadosPorAno([FromQuery] int? ano)
        {
            int anoConsulta = ano ?? DateTime.Today.Year;
            var feriados = await _service.GetFeriadosPorAno(anoConsulta);
            return Ok(feriados);
        }
    }
}
