using CarTrab.Entities;
using CarTrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarTrab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarroController : Controller
    {
        public readonly CarroService _carroService;

        public CarroController(CarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Carro carro)
        {
            try
            {
                var result = await _carroService.Create(carro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var carro = await _carroService.GetById(id);
                return Ok(carro);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var carroList = await _carroService.GetAll();
                return Ok(carroList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Carro carro)
        {
            try
            {
                var result = await _carroService.Update(carro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                var carro = await _carroService.Delete(id);
                return Ok(carro);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

    }
}