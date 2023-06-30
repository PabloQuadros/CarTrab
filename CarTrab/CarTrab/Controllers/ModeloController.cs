using CarTrab.Entities;
using CarTrab.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarTrab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeloController : Controller
    {
        public readonly ModeloService _modeloService;

        public ModeloController(ModeloService modeloService)
        {
            _modeloService = modeloService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Modelo modelo)
        {
            try
            {
                var result = await _modeloService.Create(modelo);
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
                var modelo = await _modeloService.GetById(id);
                return Ok(modelo);
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
                var modeloList = await _modeloService.GetAll();
                return Ok(modeloList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] Modelo modelo)
        {
            try
            {
                var result = await _modeloService.Update(modelo);
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
                var modelo = await _modeloService.Delete(id);
                return Ok(modelo);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }

    }
}
